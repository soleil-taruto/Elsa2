using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.GameCommons;
using Charlotte.Games.Shots;

namespace Charlotte.Games.Attacks
{
	public class Attack_さやか接地攻撃 : Attack
	{
		public override bool IsInvincibleMode()
		{
			return false;
		}

		protected override IEnumerable<bool> E_Draw()
		{
			for (int frame = 0; ; frame++)
			{
				int koma = frame / 3;

				////if (Ground.I.Picture2.さやか接地攻撃.Length <= koma)
				////break;

				// 移動
				{
					const double SPEED = 1.0;

					if (1 <= DDInput.DIR_4.GetInput())
						Game.I.Player.X -= SPEED;

					if (1 <= DDInput.DIR_6.GetInput())
						Game.I.Player.X += SPEED;
				}

				AttackCommon.ProcPlayer_側面();

				double xZoom = Game.I.Player.FacingLeft ? -1.0 : 1.0;

				////if (frame == 6 * 3)
				////    Game.I.Shots.Add(new Shot_さやか接地攻撃(
				////        Game.I.Player.X + 80.0 * xZoom,
				////        Game.I.Player.Y,
				////        Game.I.Player.FacingLeft
				////        ));

				DDDraw.SetTaskList(Game.I.Player.Draw_EL);
				////DDDraw.DrawBegin(
				////Ground.I.Picture2.さやか接地攻撃[koma],
				////Game.I.Player.X - DDGround.ICamera.X,
				////Game.I.Player.Y - DDGround.ICamera.Y - 4.0
				////);
				DDDraw.DrawZoom_X(xZoom);
				DDDraw.DrawEnd();
				DDDraw.Reset();

				yield return true;
			}
		}
	}
}
