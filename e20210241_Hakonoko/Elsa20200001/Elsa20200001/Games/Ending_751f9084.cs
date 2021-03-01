using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Commons;
using Charlotte.GameCommons;

namespace Charlotte.Games
{
	public class Ending_生還 : Ending
	{
		protected override IEnumerable<int> Script()
		{
			// TODO: 背景
			// TODO: BGM

			yield return 240;
			DDGround.EL.Add(SCommon.Supplier(DrawString(480, 200, "眩しい光が目に飛び込んでくる。", 800)));
			yield return 240;
			DDGround.EL.Add(SCommon.Supplier(DrawString(480, 250, "どうやら私は生死の境から生還したらしい。")));

			yield return 600;
			DDGround.EL.Add(SCommon.Supplier(DrawString(480, 250, "周りには安堵の表情を浮かべた医師と看護師が居る。", 1000)));
			yield return 240;
			DDGround.EL.Add(SCommon.Supplier(DrawString(480, 300, "……それ以外には誰も居ない。", 800)));
			yield return 240;
			DDGround.EL.Add(SCommon.Supplier(DrawString(480, 350, "それで良いし、とっくに覚悟はしていた。")));

			yield return 600;
			DDGround.EL.Add(SCommon.Supplier(DrawString(480, 150, "生きることは素晴らしく、それだけで幸せなこと。", 800)));
			yield return 240;
			DDGround.EL.Add(SCommon.Supplier(DrawString(480, 200, "そんな欺瞞に満ちた言葉には反吐が出る。")));

			yield return 600;
			DDGround.EL.Add(SCommon.Supplier(DrawString(480, 250, "でも、私はアイツと一緒に生きていくと決めた。", 800)));
			yield return 240;
			DDGround.EL.Add(SCommon.Supplier(DrawString(480, 300, "それが正しい選択かどうかは、生き抜いてみて初めて分かること。")));

			yield return 600;
			DDGround.EL.Add(SCommon.Supplier(DrawString(480, 200, "笑って死ぬ。", 1200)));
			yield return 240;
			DDGround.EL.Add(SCommon.Supplier(DrawString(480, 250, "明確な目標が出来たことで、心はスッキリしている。", 1000)));
			yield return 440;
			DDGround.EL.Add(SCommon.Supplier(DrawString(480, 330, "……とりあえず、墓参りと面会から始めてみようかな。")));

			yield return 840;
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

				DDFontUtils.DrawString_XCenter(x, y, text, DDFontUtils.GetFont("Kゴシック", 30), false, color);

				yield return true;
			}
		}
	}
}
