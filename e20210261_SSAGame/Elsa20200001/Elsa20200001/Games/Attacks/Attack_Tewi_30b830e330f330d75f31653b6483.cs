using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.GameCommons;
using Charlotte.Games.Shots;

namespace Charlotte.Games.Attacks
{
	public class Attack_Tewi_ジャンプ弱攻撃 : Attack
	{
		public override bool IsInvincibleMode()
		{
			return false;
		}

		protected override IEnumerable<bool> E_Draw()
		{
			for (int frame = 0; ; frame++)
			{
				if (1 <= frame && DDInput.B.GetInput() == 1) // ? 再発砲
					frame = 0;

				double x = Game.I.Player.X;
				double y = Game.I.Player.Y;
				double xZoom = Game.I.Player.FacingLeft ? -1.0 : 1.0;
				bool facingLeft = Game.I.Player.FacingLeft;

				if (frame == 0)
				{
					////Game.I.Shots.Add(new Shot_ほむら滞空攻撃(
					////    x + 50.0 * xZoom,
					////    y - 18.0,
					////    facingLeft
					////    ));
				}

				int koma = Math.Min((frame + 15) / 4, Ground.I.Picture2.Tewi_ジャンプ弱攻撃.Length - 1);

				AttackCommon.ProcPlayer_移動();
				AttackCommon.ProcPlayer_Fall();

				AttackCommon.ProcPlayer_側面();
				AttackCommon.ProcPlayer_脳天();

				if (AttackCommon.ProcPlayer_接地())
					break;

				DDDraw.SetTaskList(Game.I.Player.Draw_EL);
				DDDraw.DrawBegin(
					Ground.I.Picture2.Tewi_ジャンプ弱攻撃[koma],
					x - DDGround.ICamera.X,
					y - DDGround.ICamera.Y
					);
				DDDraw.DrawZoom_X(xZoom);
				DDDraw.DrawEnd();
				DDDraw.Reset();

				yield return true;
			}
		}
	}
}
