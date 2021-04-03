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

			// _#Include_Resource // for t20201023_GitHubRepositoriesSolve

			yield return 240;
			DDGround.EL.Add(SCommon.Supplier(DrawString(480, 200, "\u7729\u3057\u3044\u5149\u304c\u76ee\u306b\u98db\u3073\u8fbc\u3093\u3067\u304f\u308b\u3002", 800)));
			yield return 240;
			DDGround.EL.Add(SCommon.Supplier(DrawString(480, 250, "\u3069\u3046\u3084\u3089\u79c1\u306f\u751f\u6b7b\u306e\u5883\u304b\u3089\u751f\u9084\u3057\u305f\u3089\u3057\u3044\u3002")));

			yield return 600;
			DDGround.EL.Add(SCommon.Supplier(DrawString(480, 250, "\u5468\u308a\u306b\u306f\u5b89\u5835\u306e\u8868\u60c5\u3092\u6d6e\u304b\u3079\u305f\u533b\u5e2b\u3068\u770b\u8b77\u5e2b\u304c\u5c45\u308b\u3002", 1000)));
			yield return 240;
			DDGround.EL.Add(SCommon.Supplier(DrawString(480, 300, "\u2026\u2026\u305d\u308c\u4ee5\u5916\u306b\u306f\u8ab0\u3082\u5c45\u306a\u3044\u3002", 800)));
			yield return 240;
			DDGround.EL.Add(SCommon.Supplier(DrawString(480, 350, "\u305d\u308c\u3067\u826f\u3044\u3057\u3001\u3068\u3063\u304f\u306b\u899a\u609f\u306f\u3057\u3066\u3044\u305f\u3002")));

			yield return 600;
			DDGround.EL.Add(SCommon.Supplier(DrawString(480, 150, "\u751f\u304d\u308b\u3053\u3068\u306f\u7d20\u6674\u3089\u3057\u304f\u3001\u305d\u308c\u3060\u3051\u3067\u5e78\u305b\u306a\u3053\u3068\u3002", 800)));
			yield return 240;
			DDGround.EL.Add(SCommon.Supplier(DrawString(480, 200, "\u305d\u3093\u306a\u6b3a\u779e\u306b\u6e80\u3061\u305f\u8a00\u8449\u306b\u306f\u53cd\u5410\u304c\u51fa\u308b\u3002")));

			yield return 600;
			DDGround.EL.Add(SCommon.Supplier(DrawString(480, 250, "\u3067\u3082\u3001\u79c1\u306f\u30a2\u30a4\u30c4\u3068\u4e00\u7dd2\u306b\u751f\u304d\u3066\u3044\u304f\u3068\u6c7a\u3081\u305f\u3002", 800)));
			yield return 240;
			DDGround.EL.Add(SCommon.Supplier(DrawString(480, 300, "\u305d\u308c\u304c\u6b63\u3057\u3044\u9078\u629e\u304b\u3069\u3046\u304b\u306f\u3001\u751f\u304d\u629c\u3044\u3066\u307f\u3066\u521d\u3081\u3066\u5206\u304b\u308b\u3053\u3068\u3002")));

			yield return 600;
			DDGround.EL.Add(SCommon.Supplier(DrawString(480, 200, "\u7b11\u3063\u3066\u6b7b\u306c\u3002", 1200)));
			yield return 240;
			DDGround.EL.Add(SCommon.Supplier(DrawString(480, 250, "\u660e\u78ba\u306a\u76ee\u6a19\u304c\u51fa\u6765\u305f\u3053\u3068\u3067\u3001\u5fc3\u306f\u30b9\u30c3\u30ad\u30ea\u3057\u3066\u3044\u308b\u3002", 1000)));
			yield return 440;
			DDGround.EL.Add(SCommon.Supplier(DrawString(480, 330, "\u2026\u2026\u3068\u308a\u3042\u3048\u305a\u3001\u5893\u53c2\u308a\u3068\u9762\u4f1a\u304b\u3089\u59cb\u3081\u3066\u307f\u3088\u3046\u304b\u306a\u3002")));

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

				DDFontUtils.DrawString_XCenter(x, y, text, DDFontUtils.GetFont("K\u30b4\u30b7\u30c3\u30af", 30), false, color);

				yield return true;
			}
		}
	}
}
