using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.GameCommons;
using Charlotte.Games.Shots;

namespace Charlotte.Games.Attacks
{
	public class Attack_ほむらシールド : Attack
	{
		public override bool IsInvincibleMode()
		{
			return false;
		}

		protected override IEnumerable<bool> E_Draw()
		{
			for (int frame = 0; ; frame++)
			{
				int koma = frame / 6;

				if (Ground.I.Picture2.ほむらシールド.Length <= koma)
					break;

				if (frame == 4 * 6)
					this.カットイン();

				double xZoom = Game.I.Player.FacingLeft ? -1.0 : 1.0;

				DDDraw.SetTaskList(Game.I.Player.Draw_EL);
				DDDraw.DrawBegin(
					Ground.I.Picture2.ほむらシールド[koma],
					Game.I.Player.X - DDGround.ICamera.X - 8.0 * xZoom,
					Game.I.Player.Y - DDGround.ICamera.Y + 2.0
					);
				DDDraw.DrawZoom_X(xZoom);
				DDDraw.DrawEnd();
				DDDraw.Reset();

				yield return true;
			}

			Game.I.Shots.Add(new Shot_ほむらシールド());
		}

		private void カットイン()
		{
			DDMain.KeepMainScreen();

			foreach (DDScene scene in DDSceneUtils.Create(40))
			{
				DDDraw.DrawSimple(DDGround.KeptMainScreen.ToPicture(), 0, 0);

				DDDraw.SetBright(0, 0, 0);
				DDDraw.SetAlpha(1.0 - scene.Rate);
				DDDraw.DrawBegin(
					Ground.I.Picture.WhiteCircle,
					Game.I.Player.X - DDGround.ICamera.X,
					Game.I.Player.Y - DDGround.ICamera.Y
					);
				DDDraw.DrawZoom(0.3 + 20.0 * scene.Rate);
				DDDraw.DrawEnd();
				DDDraw.Reset();

				DDEngine.EachFrame();
			}
		}
	}
}
