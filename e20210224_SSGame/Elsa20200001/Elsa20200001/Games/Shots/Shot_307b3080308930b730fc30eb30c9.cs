using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Games.Enemies;
using Charlotte.GameCommons;
using Charlotte.Commons;

namespace Charlotte.Games.Shots
{
	public class Shot_ほむらシールド : Shot
	{
		public Shot_ほむらシールド()
			: base(0, 0, false, 0, true, true)
		{ }

		protected override IEnumerable<bool> E_Draw()
		{
			// 無効化した。

			for (int frame = 0; frame < 180; frame++)
				yield return true;

			if (Game.I.Player.HP == -1) // ? プレイヤーが死亡している。
				goto endFunc;

			this.終了カットイン();

		endFunc:
			;
		}

		private void 終了カットイン()
		{
			DDMain.KeepMainScreen();

			foreach (DDScene scene in DDSceneUtils.Create(40))
			{
				DDDraw.DrawSimple(DDGround.KeptMainScreen.ToPicture(), 0, 0);

				DDDraw.SetBright(0, 0, 0);
				DDDraw.SetAlpha(scene.Rate);
				DDDraw.DrawBegin(
					Ground.I.Picture.WhiteCircle,
					Game.I.Player.X - DDGround.ICamera.X,
					Game.I.Player.Y - DDGround.ICamera.Y
					);
				DDDraw.DrawZoom(0.3 + 20.0 * (1.0 - scene.Rate));
				DDDraw.DrawEnd();
				DDDraw.Reset();

				DDEngine.EachFrame();
			}
		}
	}
}
