using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Commons;
using Charlotte.GameCommons;

namespace Charlotte.Games.Shots
{
	public class Shot_跳ねる陰陽玉 : Shot
	{
		public Shot_跳ねる陰陽玉(double x, double y, bool facingLeft, int level)
			: base(x, y, facingLeft, 10, true, false) // 自力で壁から跳ねるので、壁貫通にしておく。
		{ }

		protected override IEnumerable<bool> E_Draw()
		{
			const double R = 20.0; // 自弾半径
			const double X_ADD = 8.0; // 横移動速度
			const double GRAVITY = 0.8; // 重力加速度
			const double Y_ADD_MAX = 19.0; // 落下最高速度
			const double K = 0.98; // 跳ね返り係数

			double yAdd = 0.0;
			int bouncedCount = 0;

			for (int frame = 0; ; frame++)
			{
				this.X += X_ADD * (this.FacingLeft ? -1 : 1);
				this.Y += yAdd;

				yAdd += GRAVITY;

				DDUtils.Minim(ref yAdd, Y_ADD_MAX);

				// 跳ね返り
				{
					bool bounced = false;

					if (Game.I.Map.GetCell(GameCommon.ToTablePoint(this.X - R, this.Y)).Tile.IsWall())
					{
						this.FacingLeft = false;
						bounced = true;
					}
					if (Game.I.Map.GetCell(GameCommon.ToTablePoint(this.X + R, this.Y)).Tile.IsWall())
					{
						this.FacingLeft = true;
						bounced = true;
					}
					if (Game.I.Map.GetCell(GameCommon.ToTablePoint(this.X, this.Y - R)).Tile.IsWall() && yAdd < 0.0)
					{
						yAdd *= -K;
						bounced = true;
					}
					if (Game.I.Map.GetCell(GameCommon.ToTablePoint(this.X, this.Y + R)).Tile.IsWall() && 0.0 < yAdd)
					{
						yAdd *= -K;
						bounced = true;

						while (
							!Game.I.Map.GetCell(GameCommon.ToTablePoint(this.X, this.Y)).Tile.IsWall() &&
							Game.I.Map.GetCell(GameCommon.ToTablePoint(this.X, this.Y + R)).Tile.IsWall()
							)
							this.Y--;
					}

					if (bounced)
					{
						bouncedCount++;

						if (20 <= bouncedCount) // ? 跳ね返り回数オーバー
						{
							//DDGround.EL.Add(SCommon.Supplier(Effects.FireBall爆発(this.X, this.Y)));
							break;
						}
					}
				}

				DDDraw.DrawBegin(Ground.I.Picture2.陰陽玉, this.X, this.Y);
				DDDraw.DrawRotate(frame / 10.0);
				DDDraw.DrawEnd();

				this.Crash = DDCrashUtils.Circle(new D2Point(this.X, this.Y), R);

				yield return !DDUtils.IsOutOfCamera(new D2Point(this.X, this.Y)); // カメラから出たら消滅する。
			}
		}
	}
}
