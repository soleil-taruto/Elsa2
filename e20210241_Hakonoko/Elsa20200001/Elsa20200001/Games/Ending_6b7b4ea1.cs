using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.GameCommons;
using Charlotte.Commons;

namespace Charlotte.Games
{
	public class Ending_死亡 : Ending
	{
		protected override IEnumerable<int> Script()
		{
			// TODO: 背景
			// TODO: BGM

			yield return 240;
			DDGround.EL.Add(SCommon.Supplier(DrawString(50, 200, "呑まれている感覚が分かる。", 800)));
			yield return 240;
			DDGround.EL.Add(SCommon.Supplier(DrawString(50, 250, "抗いようの無い、圧倒的に暗い闇の中へ。")));

			yield return 600;
			DDGround.EL.Add(SCommon.Supplier(DrawString(300, 300, "ただ、思っていた様な苦痛や恐怖はない。", 800)));
			yield return 240;
			DDGround.EL.Add(SCommon.Supplier(DrawString(300, 350, "アイツが外側で緩和してくれているんだろう。")));

			yield return 600;
			DDGround.EL.Add(SCommon.Supplier(DrawString(150, 150, "もう一度生きてみよう。", 1200)));
			yield return 480;
			DDGround.EL.Add(SCommon.Supplier(DrawString(150, 300, "そんな気持ちもなかったわけじゃない。", 800)));
			yield return 240;
			DDGround.EL.Add(SCommon.Supplier(DrawString(150, 350, "けど、これもアタシが望んでいた結果の１つ。")));

			yield return 600;
			DDGround.EL.Add(SCommon.Supplier(DrawString(400, 250, "……悪くない終わりかた。")));

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
