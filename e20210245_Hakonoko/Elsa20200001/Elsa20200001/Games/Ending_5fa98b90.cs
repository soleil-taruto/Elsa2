﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DxLibDLL;
using Charlotte.Commons;
using Charlotte.GameCommons;

namespace Charlotte.Games
{
	public class Ending_復讐 : Ending
	{
		protected override IEnumerable<int> Script()
		{
			ClearAllSubScreen();

			Ground.I.Music.Ending_復讐.Play();

			DrawWall drawWall = new DrawWall();
			drawWall.DrawPictures.Add(new DrawWall.DrawPicture() { Picture = Ground.I.Picture.WhiteWall });
			DDGround.EL.Add(drawWall.Task);

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

			ClearAllSubScreen();
		}

		private static List<DDSubScreen> SubScreens = new List<DDSubScreen>();

		private static void ClearAllSubScreen()
		{
			while (1 <= SubScreens.Count)
				SCommon.UnaddElement(SubScreens).Dispose();
		}

		private IEnumerable<bool> DrawString(int x, int y, string text, int frameMax = 600)
		{
			DDSubScreen subScreenTmp = new DDSubScreen(DDConsts.Screen_W, DDConsts.Screen_H, true);
			DDSubScreen subScreen = new DDSubScreen(DDConsts.Screen_W, DDConsts.Screen_H, true);
			SubScreens.Add(subScreenTmp);
			SubScreens.Add(subScreen);

			using (subScreenTmp.Section())
			{
				DX.ClearDrawScreen();

				DDFontUtils.DrawString_XCenter(x, y, text, DDFontUtils.GetFont("K\u30b4\u30b7\u30c3\u30af", 30));

				ぼかし効果.Perform(0.01);
			}
			using (subScreen.Section())
			{
				DX.ClearDrawScreen();

				for (int c = 0; c < 100; c++)
				{
					DDDraw.SetBlendAdd(1.0);
					DDDraw.DrawSimple(subScreenTmp.ToPicture(), 0, 0);
					DDDraw.Reset();
				}
				ぼかし効果.Perform(0.01);
			}
			using (subScreenTmp.Section())
			{
				DX.ClearDrawScreen();

				for (int c = 0; c < 100; c++)
				{
					DDDraw.SetBlendAdd(1.0);
					DDDraw.DrawSimple(subScreen.ToPicture(), 0, 0);
					DDDraw.Reset();
				}
				ぼかし効果.Perform(0.01);
			}
			using (subScreen.Section())
			{
				DX.ClearDrawScreen();

				DDDraw.SetBright(0.0, 0.0, 0.0);
				DDDraw.DrawSimple(subScreenTmp.ToPicture(), 0, 0);
				DDDraw.Reset();

				DDFontUtils.DrawString_XCenter(x, y, text, DDFontUtils.GetFont("K\u30b4\u30b7\u30c3\u30af", 30));
			}

			double a = 0.0;
			double aTarg = 1.0;

			foreach (DDScene scene in DDSceneUtils.Create(frameMax))
			{
				if (scene.Numer == scene.Denom - 300)
					aTarg = 0.0;

				DDUtils.Approach(ref a, aTarg, 0.985);

				DDDraw.SetAlpha(a);
				DDDraw.DrawSimple(subScreen.ToPicture(), 0, 0);
				DDDraw.Reset();

				yield return true;
			}
		}

		private class DrawWall : DDTask
		{
			public List<DrawPicture> DrawPictures = new List<DrawPicture>();

			public override IEnumerable<bool> E_Task()
			{
				for (int frame = 0; ; frame++)
				{
					if (2 <= this.DrawPictures.Count && 1.0 - SCommon.MICRO < this.DrawPictures[1].A)
						this.DrawPictures.RemoveAt(0);

					foreach (DrawPicture task in this.DrawPictures)
						task.Execute();

					yield return true;
				}
			}

			public class DrawPicture : DDTask
			{
				public DDPicture Picture;
				public double XAdd = 0.0;
				public double YAdd = 0.0;

				// <---- prm

				private double X = 0.0;
				private double Y = 0.0;
				public double A = 0.0;

				public override IEnumerable<bool> E_Task()
				{
					for (int frame = 0; ; frame++)
					{
						DDDraw.SetAlpha(this.A);
						DDDraw.DrawSimple(this.Picture, this.X, this.Y);
						DDDraw.Reset();

						this.X += this.XAdd;
						this.Y += this.YAdd;
						DDUtils.Approach(ref this.A, 1.0, 0.993);

						yield return true;
					}
				}
			}
		}
	}
}
