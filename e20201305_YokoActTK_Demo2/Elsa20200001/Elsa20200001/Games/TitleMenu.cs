﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Commons;
using Charlotte.GameCommons;
using Charlotte.Novels;

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
			DDCurtain.SetCurtain(0, -1.0);
			DDCurtain.SetCurtain();

			DDEngine.FreezeInput();

			Ground.I.Music.Title.Play();

			string[] items = new string[]
			{
				"ゲームスタート(ほむら)",
				"ゲームスタート(さやか)",
				"コンテニュー",
				"設定",
				"終了",
			};

			int selectIndex = 0;

			this.SimpleMenu = new DDSimpleMenu();

			this.SimpleMenu.BorderColor = new I3Color(64, 0, 0);
			//this.SimpleMenu.WallColor = new I3Color(96, 0, 0);
			this.SimpleMenu.WallPicture = Ground.I.Picture.Title;
			this.SimpleMenu.WallCurtain = -0.8;

			for (; ; )
			{
				selectIndex = this.SimpleMenu.Perform("横スクロール アクションゲーム タイプ-K テストコード / タイトルメニュー", items, selectIndex);

				switch (selectIndex)
				{
					case 0:
						{
							this.LeaveTitleMenu();

							using (new WorldGameMaster())
							{
								WorldGameMaster.I.World = new World("t0001"); // 仮？
								WorldGameMaster.I.Status = new GameStatus();
								WorldGameMaster.I.Perform();
							}
							this.ReturnTitleMenu();
						}
						break;

					case 1:
						{
							this.LeaveTitleMenu();

							using (new WorldGameMaster())
							{
								WorldGameMaster.I.World = new World("t0001"); // 仮？
								WorldGameMaster.I.Status = new GameStatus()
								{
									StartChara = Player.Chara_e.SAYAKA,
								};
								WorldGameMaster.I.Perform();
							}
							this.ReturnTitleMenu();
						}
						break;

					case 2:
						{
							Ground.GameSaveDataInfo gameSaveData = LoadGame();

							if (gameSaveData != null)
							{
								this.LeaveTitleMenu();

								using (new WorldGameMaster())
								{
									WorldGameMaster.I.World = new World(gameSaveData.MapName);
									WorldGameMaster.I.Status = gameSaveData.GameStatus.GetClone();
									WorldGameMaster.I.Status.StartPointDirection = 101; // スタート地点を「ロード地点」にする。
									WorldGameMaster.I.Perform();
								}
								this.ReturnTitleMenu();
							}
						}
						break;

					case 3:
						this.Setting();
						break;

					case 4:
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

		private static Ground.GameSaveDataInfo LoadGame()
		{
			Ground.GameSaveDataInfo gameSaveData = null;

			DDEngine.FreezeInput();

			DDCurtain.SetCurtain();
			DDEngine.FreezeInput();

			DDSimpleMenu simpleMenu = new DDSimpleMenu();

			simpleMenu.BorderColor = new I3Color(0, 128, 0);
			simpleMenu.WallColor = new I3Color(64, 64, 128);

			string[] items = Ground.I.GameSaveDataSlots.Select(v => v == null ? "[データ無し]" : v.TimeStamp).Concat(new string[] { "戻る" }).ToArray();

			int selectIndex = 0;

			for (; ; )
			{
				selectIndex = simpleMenu.Perform("ロード画面", items, selectIndex);

				if (selectIndex < Consts.GAME_SAVE_DATA_SLOT_NUM)
				{
					if (Ground.I.GameSaveDataSlots[selectIndex] != null)
					{
						gameSaveData = Ground.I.GameSaveDataSlots[selectIndex];
						break;
					}
				}
				else // [戻る]
				{
					break;
				}
				//DDEngine.EachFrame(); // 不要
			}
			DDEngine.FreezeInput();

			return gameSaveData;
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
				"ノベルパートのメッセージ表示速度",
				"戻る",
			};

			int selectIndex = 0;

			for (; ; )
			{
				selectIndex = this.SimpleMenu.Perform("設定", items, selectIndex);

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
							DDUtils.Random.ChooseOne(Ground.I.SE.テスト用s).Play();
						}
						);
						break;

					case 5:
						this.SimpleMenu.IntVolumeConfig(
							"ノベルパートのメッセージ表示速度",
							Ground.I.NovelMessageSpeed,
							NovelConsts.MESSAGE_SPEED_MIN,
							NovelConsts.MESSAGE_SPEED_MAX,
							1,
							2,
							speed => Ground.I.NovelMessageSpeed = speed,
							() => { }
							);
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
			Ground.I.Music.Title.Play();

			GC.Collect();
		}
	}
}
