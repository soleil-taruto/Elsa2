using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Commons;
using Charlotte.GameCommons;

namespace Charlotte.Games
{
	public class Ending_復讐 : Ending
	{
		protected override IEnumerable<int> Script()
		{
			yield return 180;
			DDCurtain.SetCurtain(0);
			DDGround.EL.Add(SCommon.Supplier(DrawString(420, 250, "さよなら")));

			yield return 1000;
			DDCurtain.SetCurtain(0, -1.0);
			DDCurtain.SetCurtain();

			yield return 240;
			DDGround.EL.Add(SCommon.Supplier(DrawString(50, 200, "生還したアタシの世界には色がなかった。", 1200)));
			yield return 440;
			DDGround.EL.Add(SCommon.Supplier(DrawString(50, 250, "でも、抑えようのない黒い炎が心の中で燃え盛り、", 800)));
			yield return 240;
			DDGround.EL.Add(SCommon.Supplier(DrawString(50, 300, "恐ろしいほどの原動力を生んでいた。")));

			yield return 600;
			DDGround.EL.Add(SCommon.Supplier(DrawString(20, 250, "その力に、心に従うことでアタシの世界に一色だけ色が戻った。")));

			yield return 600;
			DDGround.EL.Add(SCommon.Supplier(DrawString(320, 250, "「 アカ色 」 だけが。")));

			yield return 800;
			DDCurtain.SetCurtain(30, -1.0);
			DDMusicUtils.Fade();
			yield return 40;
		}

		private IEnumerable<bool> DrawString(int x, int y, string text, int frameMax = 600)
		{
			double b = 0.0;
			double bTarg = 1.0;

			foreach (DDScene scene in DDSceneUtils.Create(frameMax))
			{
				if (scene.Numer == scene.Denom - 300)
					bTarg = 0.0;

				DDUtils.Approach(ref b, bTarg, 0.99);

				I3Color color = new I3Color(
					SCommon.ToInt(b * 255),
					SCommon.ToInt(b * 255),
					SCommon.ToInt(b * 255)
					);

				DDFontUtils.DrawString(x, y, text, DDFontUtils.GetFont("03焚火-Regular", 30), false, color);

				yield return true;
			}
		}
	}
}
