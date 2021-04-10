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
			// TODO: 背景
			// TODO: BGM

			// _#Include_Resource // for t20201023_GitHubRepositoriesSolve

			yield return 240;
			DDGround.EL.Add(SCommon.Supplier(DrawString(50, 200, "\u751f\u9084\u3057\u305f\u30a2\u30bf\u30b7\u306e\u4e16\u754c\u306b\u306f\u8272\u304c\u306a\u304b\u3063\u305f\u3002", 1200)));
			yield return 440;
			DDGround.EL.Add(SCommon.Supplier(DrawString(50, 250, "\u3067\u3082\u3001\u6291\u3048\u3088\u3046\u306e\u306a\u3044\u9ed2\u3044\u708e\u304c\u5fc3\u306e\u4e2d\u3067\u71c3\u3048\u76db\u308a\u3001", 800)));
			yield return 240;
			DDGround.EL.Add(SCommon.Supplier(DrawString(50, 300, "\u6050\u308d\u3057\u3044\u307b\u3069\u306e\u539f\u52d5\u529b\u3092\u751f\u3093\u3067\u3044\u305f\u3002")));

			yield return 600;
			DDGround.EL.Add(SCommon.Supplier(DrawString(20, 250, "\u305d\u306e\u529b\u306b\u3001\u5fc3\u306b\u5f93\u3046\u3053\u3068\u3067\u30a2\u30bf\u30b7\u306e\u4e16\u754c\u306b\u4e00\u8272\u3060\u3051\u8272\u304c\u623b\u3063\u305f\u3002")));

			yield return 600;
			DDGround.EL.Add(SCommon.Supplier(DrawString(320, 250, "\u300c \u30a2\u30ab\u8272 \u300d \u3060\u3051\u304c\u3002")));

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

				DDFontUtils.DrawString(x, y, text, DDFontUtils.GetFont("03\u711a\u706b-Regular", 30), false, color);

				yield return true;
			}
		}
	}
}
