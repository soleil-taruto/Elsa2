using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Commons;
using Charlotte.GameCommons;

namespace Charlotte.Games.Shots
{
	public class Shot_ハンマー陰陽玉 : Shot
	{
		private int Level;

		public Shot_ハンマー陰陽玉(double x, double y, bool facingLeft, int level)
			: base(x, y, facingLeft, LevelToAttackPoint(level), true, 敵を貫通する_e.する)
		{
			this.Level = level;
		}

		private static int LevelToAttackPoint(int level)
		{
			switch (level)
			{
				case 1: return 1;
				case 2: return 2;
				case 3: return 4;
				case 4: return 8;

				default:
					throw null; // never
			}
		}

		private static int LevelToR(int level)
		{
			switch (level)
			{
				case 1: return 24;
				case 2: return 32;
				case 3: return 48;
				case 4: return 64;

				default:
					throw null; // never
			}
		}

		private static double LevelToScale(int level)
		{
			switch (level)
			{
				case 1: return 1.0;
				case 2: return 1.25;
				case 3: return 1.5;
				case 4: return 1.75;

				default:
					throw null; // never
			}
		}

		protected override IEnumerable<bool> E_Draw()
		{
			double R = LevelToR(this.Level);
			double SCALE = LevelToScale(this.Level);

			double xAdd = this.FacingLeft ? -1.0 : 1.0;
			double yAdd = Game.I.Player.YSpeed * 0.2;

			DDUtils.MakeXYSpeed(0.0, 0.0, xAdd, yAdd, 20.0, out xAdd, out yAdd);

			for (int frame = 0; ; frame++)
			{
				if (Game.I.Status.Equipment != GameStatus.Equipment_e.ハンマー陰陽玉) // 武器を切り替えたら消滅
					break;

				double xaa;
				double yaa;

				// バネの加速度
				{
					xaa = (Game.I.Player.X - this.X) * 0.01 * SCALE;
					yaa = (Game.I.Player.Y - this.Y) * 0.01 * SCALE;
				}

				yaa += 1.0 * SCALE; // 重力加速度

				xAdd += xaa;
				yAdd += yaa;

				// 空気抵抗
				{
					xAdd *= 0.97;
					yAdd *= 0.97;
				}

				this.X += xAdd;
				this.Y += yAdd;

				DDDraw.DrawBegin(Ground.I.Picture2.陰陽玉, this.X - DDGround.ICamera.X, this.Y - DDGround.ICamera.Y);
				DDDraw.DrawSetSize(R * 2, R * 2);
				DDDraw.DrawRotate(frame / 10.0);
				DDDraw.DrawEnd();

				this.Crash = DDCrashUtils.Circle(new D2Point(this.X, this.Y), R);

				yield return true;
			}
		}
	}
}
