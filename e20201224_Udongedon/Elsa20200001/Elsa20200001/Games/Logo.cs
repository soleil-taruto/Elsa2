﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.GameCommons;

namespace Charlotte.Games
{
	public class Logo : IDisposable
	{
		// <---- prm

		public static Logo I;

		public Logo()
		{
			I = this;
		}

		public void Dispose()
		{
			I = null;
		}

		public void Perform()
		{
			if (DDConfig.LOG_ENABLED) // 開発・デバッグ_モードであることを表示
			{
#if true
				DDGround.EL.Keep(300, () =>
				{
					DDPrint.SetPrint(20, DDConsts.Screen_H - 40, 20);
					DDPrint.PrintLine("デバッグモードが有効になりました。");
					DDPrint.PrintLine("★これはクローズドテスト版です。仮リソース・未実装・不完全な機能を含みます。(このメッセージは数秒で消えます)");
				});
#else // old nearly same
				int endFrame = DDEngine.ProcFrame + 300;

				DDGround.EL.Add(() =>
				{
					int remFrame = endFrame - DDEngine.ProcFrame;

					DDPrint.SetPrint(60, DDConsts.Screen_H - 40, 20);
					DDPrint.PrintLine("デバッグモードが有効になりました。");
					DDPrint.PrintLine("★これはクローズドテスト版です。仮リソース・未実装・不完全な機能を含みます。(あと " + (remFrame / 60.0).ToString("F1") + " 秒で消えます)");

					return 0 < remFrame;
				});
#endif
			}

			foreach (DDScene scene in DDSceneUtils.Create(60)) // LiteStatusDlg を閉じるまでの遅延の分(30フレーム)延長
			//foreach (DDScene scene in DDSceneUtils.Create(30))
			{
				DDCurtain.DrawCurtain();
				DDEngine.EachFrame();
			}

			double z1 = 0.3;
			double z2 = 2.0;
			double z3 = 3.7;

			foreach (DDScene scene in DDSceneUtils.Create(60))
			{
				DDCurtain.DrawCurtain();

				DDDraw.SetAlpha(scene.Rate);
				DDDraw.DrawBegin(Ground.I.Picture.Copyright, DDConsts.Screen_W / 2, DDConsts.Screen_H / 2);
				DDDraw.DrawZoom(z1);
				DDDraw.DrawEnd();
				DDDraw.Reset();

				DDDraw.SetAlpha((1.0 - scene.Rate) * 0.7);
				DDDraw.DrawBegin(Ground.I.Picture.Copyright, DDConsts.Screen_W / 2, DDConsts.Screen_H / 2);
				DDDraw.DrawZoom(0.8 + 0.5 * scene.Rate);
				DDDraw.DrawEnd();
				DDDraw.Reset();

				DDDraw.SetAlpha((1.0 - scene.Rate) * 0.5);
				DDDraw.DrawBegin(Ground.I.Picture.Copyright, DDConsts.Screen_W / 2, DDConsts.Screen_H / 2);
				DDDraw.DrawZoom(z2);
				DDDraw.DrawEnd();
				DDDraw.Reset();

				DDDraw.SetAlpha((1.0 - scene.Rate) * 0.3);
				DDDraw.DrawBegin(Ground.I.Picture.Copyright, DDConsts.Screen_W / 2, DDConsts.Screen_H / 2);
				DDDraw.DrawZoom(z3);
				DDDraw.DrawEnd();
				DDDraw.Reset();

				DDUtils.Approach(ref z1, 1.0, 0.9);
				DDUtils.Approach(ref z2, 1.0, 0.98);
				DDUtils.Approach(ref z3, 1.0, 0.95);

				DDEngine.EachFrame();
			}

			{
				long endLoopTime = long.MaxValue;

				for (int frame = 0; ; frame++)
				{
					if (endLoopTime < DDEngine.FrameStartTime)
						break;

					if (frame == 1)
					{
						endLoopTime = DDEngine.FrameStartTime + 1500;
						DDTouch.Touch();
					}
					DDCurtain.DrawCurtain();
					DDDraw.DrawCenter(Ground.I.Picture.Copyright, DDConsts.Screen_W / 2, DDConsts.Screen_H / 2);
					DDEngine.EachFrame();
				}
			}

			foreach (DDScene scene in DDSceneUtils.Create(60))
			{
				DDCurtain.DrawCurtain();

				DDDraw.SetAlpha((1.0 - scene.Rate) * 0.5);
				DDDraw.DrawBegin(Ground.I.Picture.Copyright, DDConsts.Screen_W / 2, DDConsts.Screen_H / 2);
				DDDraw.DrawZoom(1.0 - 0.3 * scene.Rate);
				DDDraw.DrawRotate(scene.Rate * -0.1);
				DDDraw.DrawEnd();
				DDDraw.Reset();

				DDDraw.SetAlpha((1.0 - scene.Rate) * 0.5);
				DDDraw.DrawBegin(Ground.I.Picture.Copyright, DDConsts.Screen_W / 2, DDConsts.Screen_H / 2);
				DDDraw.DrawZoom(1.0 + 0.8 * scene.Rate);
				DDDraw.DrawRotate(scene.Rate * 0.1);
				DDDraw.DrawEnd();
				DDDraw.Reset();

				DDDraw.SetAlpha((1.0 - scene.Rate) * 0.3);
				DDDraw.DrawCenter(Ground.I.Picture.Copyright, DDConsts.Screen_W / 2 + scene.Rate * 100.0, DDConsts.Screen_H / 2);
				DDDraw.Reset();

				DDDraw.SetAlpha((1.0 - scene.Rate) * 0.3);
				DDDraw.DrawCenter(Ground.I.Picture.Copyright, DDConsts.Screen_W / 2, DDConsts.Screen_H / 2 + scene.Rate * 50.0);
				DDDraw.Reset();

				DDEngine.EachFrame();
			}
		}
	}
}
