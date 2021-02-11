using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Commons;
using Charlotte.GameCommons;
using Charlotte.GameProgressMasters;

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
				"ゲームスタート",
				"コンテニュー(未実装)",
				"設定",
				"終了",
			};

			int selectIndex = 0;

			this.SimpleMenu = new DDSimpleMenu();

			this.SimpleMenu.BorderColor = new I3Color(64, 0, 0);
			this.SimpleMenu.WallColor = new I3Color(96, 0, 0);
			//this.SimpleMenu.WallPicture = Ground.I.Picture.Title;
			//this.SimpleMenu.WallCurtain = -0.8;

			for (; ; )
			{
				selectIndex = this.SimpleMenu.Perform("ドレミー・ロックマン / タイトルメニュー(仮)", items, selectIndex);

				switch (selectIndex)
				{
					case 0:
#if true // 開発デバッグ用
						this.Debug_SelectWorld();
#else
						{
							this.LeaveTitleMenu();

							using (new GameProgressMaster())
							{
								GameProgressMaster.I.Perform();
							}
							this.ReturnTitleMenu();
						}
#endif
						break;

					case 1:
						// TODO
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

		private void Debug_SelectWorld()
		{
			Action<string, string> a_gameStart = (worldName, startMapName) =>
			{
				this.LeaveTitleMenu();

				using (new WorldGameMaster())
				{
					WorldGameMaster.I.World = new World(worldName, startMapName);
					WorldGameMaster.I.Status = new GameStatus();
					WorldGameMaster.I.Perform();
				}
				this.ReturnTitleMenu();
			};

			int selectIndex = this.SimpleMenu.Perform("開発デバッグ用_ワールドセレクト", new string[]
			{
				"Stage_Raimu_v001",
				"Stage_Sanae_v001",
				"w0001(テスト用)",
				"w1001(テスト用)",
			},
			0
			);

			switch (selectIndex)
			{
				case 0:
					a_gameStart("Stage_Raimu_v001", "Start");
					break;

				case 1:
					a_gameStart("Stage_Sanae_v001", "Start");
					break;

				case 2:
					a_gameStart("w0001", "t0001");
					break;

				case 3:
					a_gameStart("w1001", "t1001");
					break;

				case 4:
					break;

				default:
					throw new DDError();
			}
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
