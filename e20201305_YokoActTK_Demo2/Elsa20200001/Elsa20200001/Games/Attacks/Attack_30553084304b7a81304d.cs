using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.GameCommons;
using Charlotte.Games.Shots;

namespace Charlotte.Games.Attacks
{
	public class Attack_さやか突き : Attack
	{
		public override bool IsInvincibleMode()
		{
			return true;
		}

		protected override IEnumerable<bool> E_Draw()
		{
			for (int frame = 0; ; frame++)
			{
				int koma = frame / 4;

				if (3 <= koma)
					break;

				// 移動
				{
					const double SPEED = 3.0;

					if (Game.I.Player.FacingLeft)
						Game.I.Player.X -= SPEED;
					else
						Game.I.Player.X += SPEED;
				}

				AttackCommon.ProcPlayer_側面();

				double xZoom = Game.I.Player.FacingLeft ? -1.0 : 1.0;

				DDDraw.SetTaskList(Game.I.Player.Draw_EL);
				DDDraw.DrawBegin(
					Ground.I.Picture2.さやか突き[koma],
					Game.I.Player.X - DDGround.ICamera.X + 30.0 * xZoom,
					Game.I.Player.Y - DDGround.ICamera.Y
					);
				DDDraw.DrawZoom_X(xZoom);
				DDDraw.DrawEnd();
				DDDraw.Reset();

				yield return true;
			}
			for (int frame = 0; frame < 60; frame++)
			{
				if (
					10 < frame &&
					DDInput.C.GetInput() <= 0 // ? 特殊攻撃ボタンを離した。
					)
					break;

				int koma = 3 + (frame / 3) % 3;

				// 移動
				{
					const double SPEED_X = 12.0;
					const double SPEED_Y = 2.0;

					if (Game.I.Player.FacingLeft)
						Game.I.Player.X -= SPEED_X;
					else
						Game.I.Player.X += SPEED_X;

					if (1 <= DDInput.DIR_8.GetInput())
						Game.I.Player.Y -= SPEED_Y;

					if (1 <= DDInput.DIR_2.GetInput())
						Game.I.Player.Y += SPEED_Y;
				}

				AttackCommon.ProcPlayer_脳天();
				AttackCommon.ProcPlayer_接地();

				if (AttackCommon.ProcPlayer_側面())
					break;

				double xZoom = Game.I.Player.FacingLeft ? -1.0 : 1.0;

				Game.I.Shots.Add(new Shot_さやか突き(
					Game.I.Player.X + 50.0 * xZoom,
					Game.I.Player.Y,
					Game.I.Player.FacingLeft
					));

				DDDraw.SetTaskList(Game.I.Player.Draw_EL);
				DDDraw.DrawBegin(
					Ground.I.Picture2.さやか突き[koma],
					Game.I.Player.X - DDGround.ICamera.X,
					Game.I.Player.Y - DDGround.ICamera.Y + 8.0
					);
				DDDraw.DrawZoom_X(xZoom);
				DDDraw.DrawEnd();
				DDDraw.Reset();

				yield return true;
			}
			for (int frame = 0; ; frame++)
			{
				int koma = 6 + frame / 2;

				if (10 <= koma)
					break;

				double xZoom = Game.I.Player.FacingLeft ? -1.0 : 1.0;

				DDDraw.SetTaskList(Game.I.Player.Draw_EL);
				DDDraw.DrawBegin(
					Ground.I.Picture2.さやか突き[koma],
					Game.I.Player.X - DDGround.ICamera.X + 30.0 * xZoom,
					Game.I.Player.Y - DDGround.ICamera.Y
					);
				DDDraw.DrawZoom_X(xZoom);
				DDDraw.DrawEnd();
				DDDraw.Reset();

				yield return true;
			}

			// 後処理
			{
				Game.I.Player.YSpeed = 0.0;
			}
		}
	}
}
