using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.GameCommons;
using Charlotte.Commons;
using Charlotte.Games.Enemies;
using Charlotte.Games.Shots;

namespace Charlotte.Games
{
	/// <summary>
	/// プレイヤーに関する情報と機能
	/// 唯一のインスタンスを Game.I.Player に保持する。
	/// </summary>
	public class Player
	{
		public enum 武器_e
		{
			NORMAL,
			WAVE,
			SPREAD,
			BOUNCE,
		}

		public static int 武器_e_Length = Enum.GetValues(typeof(武器_e)).Length;

		public double X;
		public double Y;
		public int FaceDirection; // プレイヤーが向いている方向 { 2, 4, 6, 8 } == { 下, 左, 右, 上 }
		public int MoveFrame;
		public int AttackFrame;
		public int DeadFrame = 0; // 0 == 無効, 1～ == 死亡中
		public int DamageFrame = 0; // 0 == 無効, 1～ == ダメージ中
		public int InvincibleFrame = 0; // 0 == 無効, 1～ == 無敵時間中
		public int HP = 1; // -1 == 死亡, 1～ == 生存
		public 武器_e 選択武器 = 武器_e.NORMAL;

		public void Draw()
		{
			int koma = 1;

			if (1 <= this.MoveFrame)
			{
				koma = (Game.I.Frame / 5) % 4;

				if (koma == 3)
					koma = 1;
			}

			DDPicture picture;

			var infos = new[]
			{
				null,
				new { Pic = Ground.I.Picture2.Player_05, Y = 0 }, // 1 (左下向き)
				new { Pic = Ground.I.Picture2.Player_00, Y = 0 }, // 2 (下向き)
				new { Pic = Ground.I.Picture2.Player_05, Y = 2 }, // 3 (右下向き)
				new { Pic = Ground.I.Picture2.Player_00, Y = 1 }, // 4 (左向き)
				null,
				new { Pic = Ground.I.Picture2.Player_00, Y = 2 }, // 6 (右向き)
				new { Pic = Ground.I.Picture2.Player_05, Y = 1 }, // 7 (左上向き)
				new { Pic = Ground.I.Picture2.Player_00, Y = 3 }, // 8 (上向き)
				new { Pic = Ground.I.Picture2.Player_05, Y = 3 }, // 9 (右上向き)
			};

			var info = infos[this.FaceDirection];

			picture = info.Pic[koma, info.Y];

			// ---- ダメージ中等差し替え ----

			if (1 <= this.DeadFrame)
			{
				picture = Ground.I.Picture2.Player_02[0, 3];
			}
			else if (1 <= this.DamageFrame || 1 <= this.InvincibleFrame)
			{
				DDDraw.SetTaskList(DDGround.EL);
				DDDraw.SetAlpha(0.5);
			}

			// ----

			DDDraw.DrawCenter(
				picture,
				(int)this.X - DDGround.ICamera.X,
				(int)this.Y - DDGround.ICamera.Y
				);
			DDDraw.Reset();
		}

		private bool Attack_Wave_左回転Sw = false;

		public void Attack()
		{
			// 将来的に武器毎にコードが実装され、メソッドがでかくなると思われる。

			switch (this.選択武器)
			{
				case 武器_e.NORMAL:
					if (this.AttackFrame % 10 == 1)
					{
						Game.I.Shots.Add(new Shot_Normal(this.X, this.Y, this.FaceDirection));
					}
					break;

				case 武器_e.WAVE:
					if (this.AttackFrame % 20 == 1)
					{
						Game.I.Shots.Add(new Shot_Wave(this.X, this.Y, this.FaceDirection, this.Attack_Wave_左回転Sw));
						this.Attack_Wave_左回転Sw = !this.Attack_Wave_左回転Sw;
					}
					break;

				case 武器_e.SPREAD:
					if (this.AttackFrame % 10 == 1)
					{
						for (int c = -2; c <= 2; c++)
						{
							Game.I.Shots.Add(new Shot_Spread(this.X, this.Y, this.FaceDirection, 0.3 * c));
						}
					}
					break;

				case 武器_e.BOUNCE:
					if (this.AttackFrame % 25 == 1)
					{
						for (int c = -1; c <= 1; c++)
						{
							Game.I.Shots.Add(new Shot_Bounce(this.X, this.Y, GameCommon.Rotate(this.FaceDirection, c)));
						}
					}
					break;

				default:
					throw null; // never
			}
		}
	}
}
