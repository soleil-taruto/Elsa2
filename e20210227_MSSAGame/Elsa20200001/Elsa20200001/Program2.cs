﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DxLibDLL;
using Charlotte.Commons;
using Charlotte.GameCommons;
using Charlotte.Games;
using Charlotte.Tests;
using Charlotte.Tests.Games;
using Charlotte.Tests.Novels;

namespace Charlotte
{
	public class Program2
	{
		public void Main2()
		{
			try
			{
				Main3();
			}
			catch (Exception e)
			{
				ProcMain.WriteLog(e);
			}
		}

		private void Main3()
		{
			DDMain2.Perform(Main4);
		}

		private void Main4()
		{
			// *.INIT
			{
				// アプリ固有 >

				//RippleEffect.INIT();
				//画面分割.INIT();
				//画面分割_Effect.INIT();

				// < アプリ固有
			}

			#region Charge To DDTouch

			// DDCCResource 等のための Touch
			//DDTouch.Add(TitleMenu.TouchWallDrawerResources);

			// 個別に設定
			//DDTouch.Add(Ground.I.Picture.XXX);
			//DDTouch.Add(Ground.I.Music.XXX);
			//DDTouch.Add(Ground.I.SE.XXX);

			// 全部設定
			//DDTouch.AddAllPicture();
			//DDTouch.AddAllMusic();
			//DDTouch.AddAllSE();

			#endregion

			//DDTouch.Touch(); // moved -> Logo

			if (DDConfig.LOG_ENABLED)
			{
				DDEngine.DispDebug = () =>
				{
					DDPrint.SetDebug();
					DDPrint.SetBorder(new I3Color(0, 0, 0));

					DDPrint.Print(string.Join(
						" ",
						Game.I == null ? "-" : "" + Game.I.Player.HP,

						// デバッグ表示する情報をここへ追加..

						DDEngine.FrameProcessingMillis,
						DDEngine.FrameProcessingMillis_Worst
						));

					DDPrint.Reset();
				};
			}

			if (ProcMain.DEBUG)
			{
				Main4_Debug();
			}
			else
			{
				Main4_Release();
			}
		}

		private void Main4_Debug()
		{
			// ---- choose one ----

			//Main4_Release();
			//new Test0001().Test01();
			//new DDRandomTest().Test01();
			new TitleMenuTest().Test01();
			//new GameTest().Test01();
			//new GameTest().Test02();
			//new GameTest().Test03(); // 開始マップ名を選択(当面不使用)
			//new WorldGameMasterTest().Test01();
			//new WorldGameMasterTest().Test02();
			//new WorldGameMasterTest().Test03(); // 開始マップ名を選択
			//new NovelTest().Test01();
			//new NovelTest().Test02(); // テスト0001

			// ----
		}

		private void Main4_Release()
		{
			using (new Logo())
			{
				Logo.I.Perform();
			}
			using (new TitleMenu())
			{
				TitleMenu.I.Perform();
			}
		}
	}
}
