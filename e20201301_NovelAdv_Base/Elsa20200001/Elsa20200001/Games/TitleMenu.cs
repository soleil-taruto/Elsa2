﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Commons;
using Charlotte.GameCommons;
using Charlotte.GameCommons.Options;

namespace Charlotte.Games
{
	public class TitleMenu : IDisposable
	{
		public static TitleMenu I;

		public TitleMenu()
		{
			I = this;
		}

		public void Dispose()
		{
			I = null;
		}

		private DDSimpleMenu SimpleMenu;

		public void Perform()
		{
			DDUtils.SetMouseDispMode(true); // 2bs -- 既にマウス有効であるはず

			DDCurtain.SetCurtain(0, -1.0);
			DDCurtain.SetCurtain();

			DDEngine.FreezeInput();

			Ground.I.Music.Title.Play();

			string[] items = new string[]
			{
				"ゲームスタート",
				"コンテニュー",
				"設定",
				"終了",
			};

			int selectIndex = 0;

			this.SimpleMenu = new DDSimpleMenu();
			this.SimpleMenu.Color = new I3Color(255, 255, 128);
			this.SimpleMenu.BorderColor = new I3Color(0, 0, 100);
			this.SimpleMenu.WallPicture = Ground.I.Picture.星屑物語11;
			this.SimpleMenu.WallCurtain = -0.5;

			for (; ; )
			{
				selectIndex = this.SimpleMenu.Perform("ノベルゲーテストコード / タイトルメニュー", items, selectIndex);

				switch (selectIndex)
				{
					case 0:
						{
							this.LeaveTitleMenu();

							using (new Game())
							{
								Game.I.Status.Scenario = new Scenario(GameConsts.FIRST_SCENARIO_NAME);
								Game.I.Perform();
							}
							this.ReturnTitleMenu();
						}
						break;

					case 1:
						{
							string savedData = this.LoadGame();

							if (savedData != null)
							{
								this.LeaveTitleMenu();

								using (new Game())
								{
									Game.I.Status = GameStatus.Deserialize(savedData);
									Game.I.Perform(true);
								}
								this.ReturnTitleMenu();
							}
						}
						break;

					case 2:
						this.Setting();
						break;

					case 3:
						goto endMenu;

					default:
						throw new DDError();
				}
			}
		endMenu:
			DDMusicUtils.Fade();
			DDCurtain.SetCurtain(30, -1.0);

			foreach (DDScene scene in DDSceneUtils.Create(40))
			{
				this.SimpleMenu.DrawWall();
				DDEngine.EachFrame();
			}

			DDEngine.FreezeInput();
		}

		private string LoadGame()
		{
			string savedData = null;

			DDCurtain.SetCurtain();
			DDEngine.FreezeInput();

			string[] items = Ground.I.GameSaveDataSlots.Select(v => v == null ? "[no-data]" : SCommon.Hex.ToString(SCommon.GetSHA512(Encoding.UTF8.GetBytes(v))).Substring(0, 20)).Concat(new string[] { "戻る" }).ToArray();

			int selectIndex = 0;

			for (; ; )
			{
				selectIndex = this.SimpleMenu.Perform("コンテニュー", items, selectIndex);

				if (selectIndex < Consts.GAME_SAVE_DATA_SLOT_NUM)
				{
					if (Ground.I.GameSaveDataSlots[selectIndex] != null)
					{
						savedData = Ground.I.GameSaveDataSlots[selectIndex];
						break;
					}
				}
				else // [戻る]
				{
					break;
				}
			}
			DDEngine.FreezeInput();

			return savedData;
		}

		private void Setting()
		{
			DDCurtain.SetCurtain();
			DDEngine.FreezeInput();

			string[] items = new string[]
			{
				"ゲームパッドのボタン設定",
				"キーボードのキー設定",
				"ウィンドウサイズ変更",
				"ＢＧＭ音量",
				"ＳＥ音量",
				"メッセージ表示速度",
				"メッセージウィンドウ不透明度",
				"戻る",
			};

			int selectIndex = 0;

			for (; ; )
			{
				selectIndex = this.SimpleMenu.Perform("設定メニュー", items, selectIndex);

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
						this.SimpleMenu.VolumeConfig("ＢＧＭ音量", DDGround.MusicVolume, 0, 100, 1, 10, volume =>
						{
							DDGround.MusicVolume = volume;
							DDMusicUtils.UpdateVolume();
						},
						() => { }
						);
						break;

					case 4:
						this.SimpleMenu.VolumeConfig("ＳＥ音量", DDGround.SEVolume, 0, 100, 1, 10, volume =>
						{
							DDGround.SEVolume = volume;
							DDSEUtils.UpdateVolume();
						},
						() =>
						{
							Ground.I.SE.Poka01.Play();
						}
						);
						break;

					case 5:
						this.SimpleMenu.IntVolumeConfig(
							"メッセージ表示速度",
							Ground.I.MessageSpeed,
							GameConsts.MESSAGE_SPEED_MIN,
							GameConsts.MESSAGE_SPEED_MAX,
							1,
							2,
							speed => Ground.I.MessageSpeed = speed,
							() => { }
							);
						break;

					case 6:
						this.SimpleMenu.IntVolumeConfig(
							"メッセージウィンドウ不透明度",
							Ground.I.MessageWindow_A_Pct,
							0,
							100,
							1,
							10,
							pct => Ground.I.MessageWindow_A_Pct = pct,
							() => { }
							);
						break;

					case 7:
						goto endMenu;

					default:
						throw new DDError();
				}
			}
		endMenu:
			DDEngine.FreezeInput();
		}

		private void LeaveTitleMenu()
		{
			DDMusicUtils.Fade();
			DDCurtain.SetCurtain(30, -1.0);

			foreach (DDScene scene in DDSceneUtils.Create(40))
			{
				this.SimpleMenu.DrawWall();
				DDEngine.EachFrame();
			}

			GC.Collect();
		}

		private void ReturnTitleMenu()
		{
			DDCurtain.SetCurtain();
			Ground.I.Music.Title.Play();

			DDEngine.FreezeInput(GameConsts.LONG_INPUT_SLEEP);

			GC.Collect();
		}
	}
}
