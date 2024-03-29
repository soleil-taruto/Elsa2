﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;
using DxLibDLL;
using Charlotte.Commons;
using Charlotte.Games;

namespace Charlotte.GameCommons
{
	public static class DDEngine
	{
		public static long FrameStartTime;
		public static long HzChaserTime;
		public static int SlowdownLevel;
		public static int FrameProcessingMillis;
		public static int FrameProcessingMillis_Worst;
		public static int FrameProcessingMillis_WorstFrame;
		public static int ProcFrame;
		public static int FreezeInputFrame;
		public static bool WindowIsActive;

		private static void CheckHz()
		{
			long currTime = DDUtils.GetCurrTime();

			if (1 <= SlowdownLevel)
				HzChaserTime += (long)(16.666 * SlowdownLevel); // テスト用
			else
				HzChaserTime += 16L; // 16.666 == 60Hz

			//HzChaserTime += (long)(16.666 * 8); // test
			//HzChaserTime += (long)(16.666 * 4); // test
			//HzChaserTime += (long)(16.666 * 2); // test
			//HzChaserTime += 16L; // 16.666 == 60Hz
			HzChaserTime = SCommon.ToRange(HzChaserTime, currTime - 100L, currTime + 100L);

			while (currTime < HzChaserTime)
			{
				Thread.Sleep(1);
				currTime = DDUtils.GetCurrTime();
			}
			FrameStartTime = currTime;
		}

		public static Action DispDebug = () => { };

		public static void EachFrame()
		{
			//Ground.EL.ExecuteAllTask();

			//DDGround.EL_先行.ExecuteAllTask(); // アプリ固有 -- old
			DDGround.EL.ExecuteAllTask();
			DispDebug();
			DDMouse.PosChanged_Delay();
			DDCurtain.EachFrame();

			if (!DDSEUtils.EachFrame())
			{
				DDMusicUtils.EachFrame();
			}

			DDSubScreenUtils.ChangeDrawScreen(DX.DX_SCREEN_BACK);

			if (DDGround.RealScreenDraw_W == -1)
			{
				if (DX.DrawExtendGraph(0, 0, DDGround.RealScreen_W, DDGround.RealScreen_H, DDGround.MainScreen.GetHandle(), 0) != 0) // ? 失敗
					throw new DDError();
			}
			else
			{
				if (DX.DrawBox(0, 0, DDGround.RealScreen_W, DDGround.RealScreen_H, DX.GetColor(0, 0, 0), 1) != 0) // ? 失敗
					throw new DDError();

				bool mosaicFlag =
					DDConfig.DrawScreen_MosaicFlag &&
					DDGround.RealScreenDraw_W == DDConsts.Screen_W * 2 &&
					DDGround.RealScreenDraw_H == DDConsts.Screen_H * 2;

				if (mosaicFlag)
					DX.SetDrawMode(DX.DX_DRAWMODE_NEAREST);

				if (DX.DrawExtendGraph(
					DDGround.RealScreenDraw_L,
					DDGround.RealScreenDraw_T,
					DDGround.RealScreenDraw_L + DDGround.RealScreenDraw_W,
					DDGround.RealScreenDraw_T + DDGround.RealScreenDraw_H, DDGround.MainScreen.GetHandle(), 0) != 0) // ? 失敗
					throw new DDError();

				if (mosaicFlag)
					DX.SetDrawMode(DDConsts.DEFAULT_DX_DRAWMODE); // restore
			}

			GC.Collect(0);

			FrameProcessingMillis = (int)(DDUtils.GetCurrTime() - FrameStartTime);

			if (FrameProcessingMillis_Worst < FrameProcessingMillis || !DDUtils.CountDown(ref FrameProcessingMillis_WorstFrame))
			{
				FrameProcessingMillis_Worst = FrameProcessingMillis;
				FrameProcessingMillis_WorstFrame = 120;
			}

			// DxLib >

			DX.ScreenFlip();

			if (DX.CheckHitKey(DX.KEY_INPUT_ESCAPE) == 1 || DX.ProcessMessage() == -1)
			{
				throw new DDCoffeeBreak();
			}

			// < DxLib

			CheckHz();

			ProcFrame++;
			DDUtils.CountDown(ref FreezeInputFrame);
			WindowIsActive = DDUtils.IsWindowActive();

			if (SCommon.IMAX < ProcFrame) // 192.9日程度でカンスト
			{
				ProcFrame = SCommon.IMAX; // 2bs
				throw new DDError();
			}

			DDPad.EachFrame();
			DDKey.EachFrame();
			DDInput.EachFrame();
			DDMouse.EachFrame();

			// Swap MainScreen
			{
				DDSubScreen tmp = DDGround.MainScreen;
				DDGround.MainScreen = DDGround.LastMainScreen;
				DDGround.LastMainScreen = tmp;
			}

			DDGround.MainScreen.ChangeDrawScreen();

			// ? ALT + ENTER -> フルスクリーン切り替え
			if ((1 <= DDKey.GetInput(DX.KEY_INPUT_LALT) || 1 <= DDKey.GetInput(DX.KEY_INPUT_RALT)) && DDKey.GetInput(DX.KEY_INPUT_RETURN) == 1)
			{
				// ? 現在フルスクリーン -> フルスクリーン解除
				if (
					DDGround.RealScreen_W == DDGround.MonitorRect.W &&
					DDGround.RealScreen_H == DDGround.MonitorRect.H
					)
				{
					DDMain.SetScreenSize(DDGround.UnfullScreen_W, DDGround.UnfullScreen_H);
				}
				else // ? 現在フルスクリーンではない -> フルスクリーンにする
				{
					DDGround.UnfullScreen_W = DDGround.RealScreen_W;
					DDGround.UnfullScreen_H = DDGround.RealScreen_H;

					DDMain.SetFullScreen();
				}
				DDEngine.FreezeInput(30); // エンターキー押下がゲームに影響しないように
			}
		}

		public static void FreezeInput(int frame = 1) // frame: 1 == このフレームのみ, 2 == このフレームと次のフレーム ...
		{
			if (frame < 1 || SCommon.IMAX < frame)
				throw new DDError("frame: " + frame);

			FreezeInputFrame = Math.Max(FreezeInputFrame, frame); // frame より長いフレーム数が既に設定されていたら、そちらを優先する。
		}
	}
}
