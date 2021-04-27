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

			Ground.I.Music.Ending_死亡.Play();

			// _#Include_Resource // for t20201023_GitHubRepositoriesSolve

			yield return 240;
			DDGround.EL.Add(SCommon.Supplier(DrawString(50, 200, "\u5451\u307e\u308c\u3066\u3044\u308b\u611f\u899a\u304c\u5206\u304b\u308b\u3002", 800)));
			yield return 240;
			DDGround.EL.Add(SCommon.Supplier(DrawString(50, 250, "\u6297\u3044\u3088\u3046\u306e\u7121\u3044\u3001\u5727\u5012\u7684\u306b\u6697\u3044\u95c7\u306e\u4e2d\u3078\u3002")));

			yield return 600;
			DDGround.EL.Add(SCommon.Supplier(DrawString(300, 300, "\u305f\u3060\u3001\u601d\u3063\u3066\u3044\u305f\u69d8\u306a\u82e6\u75db\u3084\u6050\u6016\u306f\u306a\u3044\u3002", 800)));
			yield return 240;
			DDGround.EL.Add(SCommon.Supplier(DrawString(300, 350, "\u30a2\u30a4\u30c4\u304c\u5916\u5074\u3067\u7de9\u548c\u3057\u3066\u304f\u308c\u3066\u3044\u308b\u3093\u3060\u308d\u3046\u3002")));

			yield return 600;
			DDGround.EL.Add(SCommon.Supplier(DrawString(150, 150, "\u3082\u3046\u4e00\u5ea6\u751f\u304d\u3066\u307f\u3088\u3046\u3002", 1200)));
			yield return 480;
			DDGround.EL.Add(SCommon.Supplier(DrawString(150, 300, "\u305d\u3093\u306a\u6c17\u6301\u3061\u3082\u306a\u304b\u3063\u305f\u308f\u3051\u3058\u3083\u306a\u3044\u3002", 800)));
			yield return 240;
			DDGround.EL.Add(SCommon.Supplier(DrawString(150, 350, "\u3051\u3069\u3001\u3053\u308c\u3082\u30a2\u30bf\u30b7\u304c\u671b\u3093\u3067\u3044\u305f\u7d50\u679c\u306e\uff11\u3064\u3002")));

			yield return 600;
			DDGround.EL.Add(SCommon.Supplier(DrawString(400, 250, "\u2026\u2026\u60aa\u304f\u306a\u3044\u7d42\u308f\u308a\u304b\u305f\u3002")));

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
