using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Commons;
using Charlotte.GameCommons;
using Charlotte.GameProgressMasters;
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
				"開発デバッグ用_ワールドセレクト",
				"ゲームスタート",
				"コンテニュー(未実装)",
				"ノベルパート(テスト)",
				"設定",
				"終了",
			};

			int selectIndex = 0;

			this.SimpleMenu = new DDSimpleMenu()
			{
				BorderColor = new I3Color(64, 0, 0),
				WallDrawer = () =>
				{
					DDDraw.SetBright(new I3Color(32, 0, 0));
					DDDraw.DrawRect(Ground.I.Picture.WhiteBox, 0, 0, DDConsts.Screen_W, DDConsts.Screen_H);
					DDDraw.Reset();
				},
			};

			for (; ; )
			{
				selectIndex = this.SimpleMenu.Perform(40, 40, 40, 24, "ドレミー・ロックマン / タイトルメニュー(仮)", items, selectIndex);

				switch (selectIndex)
				{
					case 0:
						this.Debug_SelectWorld();
						break;

					case 1:
						{
							this.LeaveTitleMenu();

							using (new GameProgressMaster())
							{
								GameProgressMaster.I.Perform();
							}
							this.ReturnTitleMenu();
						}
						break;

					case 2:
						// TODO
						break;

					case 3:
						{
							this.LeaveTitleMenu();

							using (new Novel())
							{
								Novel.I.Status.Scenario = new Scenario("テスト0001");
								Novel.I.Perform();
							}
							this.ReturnTitleMenu();
						}
						break;

					case 4:
						using (new SettingMenu())
						{
							SettingMenu.I.SimpleMenu = this.SimpleMenu;
							SettingMenu.I.Perform();
						}
						break;

					case 5:
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
				this.SimpleMenu.WallDrawer();
				DDEngine.EachFrame();
			}

			DDEngine.FreezeInput();
		}

		private void Debug_SelectWorld()
		{
			Action<string> a_gameStart = startMapName =>
			{
				this.LeaveTitleMenu();

				using (new WorldGameMaster())
				{
					WorldGameMaster.I.World = new World(startMapName);
					WorldGameMaster.I.Status = new GameStatus();
					WorldGameMaster.I.Perform();
				}
				this.ReturnTitleMenu();
			};

			int selectIndex = this.SimpleMenu.Perform(40, 40, 40, 24, "開発デバッグ用_ワールドセレクト", new string[]
			{
				"Stage_0001_v001",
				"Stage_Reimu_v001",
				"Stage_Sanae_v001",
				"w0001(テスト用)",
				"w1001(テスト用)",
				"戻る",
			},
			0
			);

			switch (selectIndex)
			{
				case 0:
					a_gameStart("Stage_0001_v001\\t1001");
					break;

				case 1:
					a_gameStart("Stage_Reimu_v001\\Start");
					break;

				case 2:
					a_gameStart("Stage_Sanae_v001\\Start");
					break;

				case 3:
					a_gameStart("w0001\\t0001");
					break;

				case 4:
					a_gameStart("w1001\\t1001");
					break;

				case 5:
					break;

				default:
					throw new DDError();
			}
		}

		private void LeaveTitleMenu()
		{
			DDMusicUtils.Fade();
			DDCurtain.SetCurtain(30, -1.0);

			foreach (DDScene scene in DDSceneUtils.Create(40))
			{
				this.SimpleMenu.WallDrawer();
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
