﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.GameCommons;

namespace Charlotte.Games
{
	public class SettingMenu : IDisposable
	{
		public DDSimpleMenu SimpleMenu;

		// <---- prm

		public static SettingMenu I;

		public SettingMenu()
		{
			I = this;
		}

		public void Dispose()
		{
			I = null;
		}

		public void Perform()
		{
			DDCurtain.SetCurtain();
			DDEngine.FreezeInput();

			DDSE[] seSamples = Ground.I.SE.テスト用s;

			int selectIndex = 0;

			for (; ; )
			{
				string[] items = new string[]
				{
					"\u30b2\u30fc\u30e0\u30d1\u30c3\u30c9\u306e\u30dc\u30bf\u30f3\u8a2d\u5b9a",
					"\u30ad\u30fc\u30dc\u30fc\u30c9\u306e\u30ad\u30fc\u8a2d\u5b9a",
					"\u30a6\u30a3\u30f3\u30c9\u30a6\u30b5\u30a4\u30ba\u5909\u66f4",
					"\uff22\uff27\uff2d\u97f3\u91cf",
					"\uff33\uff25\u97f3\u91cf",
					"\u30b7\u30e7\u30c3\u30c8\u306e\u30bf\u30a4\u30df\u30f3\u30b0 [ \u30b7\u30e7\u30c3\u30c8\u30dc\u30bf\u30f3\u3092" +
						(Ground.I.\u30b7\u30e7\u30c3\u30c8\u306e\u30bf\u30a4\u30df\u30f3\u30b0 == Ground.\u30b7\u30e7\u30c3\u30c8\u306e\u30bf\u30a4\u30df\u30f3\u30b0_e.\u30b7\u30e7\u30c3\u30c8\u30dc\u30bf\u30f3\u3092\u62bc\u3057\u4e0b\u3052\u305f\u6642 ? "\u62bc\u3057\u4e0b\u3052\u305f" : "\u96e2\u3057\u305f") +
						"\u6642 ]",
					"\u623b\u308b",
				};

				selectIndex = this.SimpleMenu.Perform(40, 40, 40, 24, "\u8a2d\u5b9a", items, selectIndex);

				switch (selectIndex)
				{
					case 0:
						this.SimpleMenu.PadConfig();
						break;

					case 1:
						this.SimpleMenu.PadConfig(true);
						break;

					case 2:
						this.SimpleMenu.WindowSizeConfig();
						break;

					case 3:
						this.SimpleMenu.VolumeConfig("\uff22\uff27\uff2d\u97f3\u91cf", DDGround.MusicVolume, 0, 100, 1, 10, volume =>
						{
							DDGround.MusicVolume = volume;
							DDMusicUtils.UpdateVolume();
						},
						() => { }
						);
						break;

					case 4:
						this.SimpleMenu.VolumeConfig("\uff33\uff25\u97f3\u91cf", DDGround.SEVolume, 0, 100, 1, 10, volume =>
						{
							DDGround.SEVolume = volume;
							//DDSEUtils.UpdateVolume(); // old

							foreach (DDSE se in seSamples) // サンプルのみ音量更新
								se.UpdateVolume();
						},
						() =>
						{
							DDUtils.Random.ChooseOne(seSamples).Play();
						}
						);
						DDSEUtils.UpdateVolume(); // 全音量更新
						break;

					case 5:
						Ground.I.ショットのタイミング =
							Ground.I.ショットのタイミング == Ground.ショットのタイミング_e.ショットボタンを押し下げた時 ?
							Ground.ショットのタイミング_e.ショットボタンを離した時 :
							Ground.ショットのタイミング_e.ショットボタンを押し下げた時;
						break;

					case 6:
						goto endMenu;

					default:
						throw new DDError();
				}
			}
		endMenu:
			DDEngine.FreezeInput();
		}
	}
}
