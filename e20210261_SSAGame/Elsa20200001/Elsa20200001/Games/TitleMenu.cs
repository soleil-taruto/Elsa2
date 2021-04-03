using System;
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
				"\u30b2\u30fc\u30e0\u30b9\u30bf\u30fc\u30c8(\u307b\u3080\u3089)",
				"\u30b2\u30fc\u30e0\u30b9\u30bf\u30fc\u30c8(\u3055\u3084\u304b)",
				"\u30b3\u30f3\u30c6\u30cb\u30e5\u30fc",
				"\u8a2d\u5b9a",
				"\u7d42\u4e86",
			};

			int selectIndex = 0;

			this.SimpleMenu = new DDSimpleMenu()
			{
				BorderColor = new I3Color(64, 0, 0),
				WallDrawer = () =>
				{
					DDPicture picture = Ground.I.Picture.Title;

					DDDraw.DrawRect(
						picture,
						DDUtils.AdjustRectExterior(picture.GetSize().ToD2Size(), new D4Rect(0, 0, DDConsts.Screen_W, DDConsts.Screen_H))
						);

					DDCurtain.DrawCurtain(-0.8);
				},
			};

			for (; ; )
			{
				selectIndex = this.SimpleMenu.Perform(40, 40, 40, 24, "\u6a2a\u30b9\u30af\u30ed\u30fc\u30eb \u30a2\u30af\u30b7\u30e7\u30f3\u30b2\u30fc\u30e0 \u30bf\u30a4\u30d7-K \u30c6\u30b9\u30c8\u30b3\u30fc\u30c9 / \u30bf\u30a4\u30c8\u30eb\u30e1\u30cb\u30e5\u30fc", items, selectIndex);

				switch (selectIndex)
				{
					case 0:
						{
							this.LeaveTitleMenu();

							using (new WorldGameMaster())
							{
								WorldGameMaster.I.World = new World("Start");
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
								WorldGameMaster.I.World = new World("Start");
								WorldGameMaster.I.Status = new GameStatus()
								{
									StartChara = Player.Chara_e.CIRNO,
								};
								WorldGameMaster.I.Perform();
							}
							this.ReturnTitleMenu();
						}
						break;

					case 2:
						{
							Ground.P_SaveDataSlot saveDataSlot = LoadGame();

							if (saveDataSlot != null)
							{
								this.LeaveTitleMenu();

								using (new WorldGameMaster())
								{
									WorldGameMaster.I.World = new World(saveDataSlot.MapName);
									WorldGameMaster.I.Status = saveDataSlot.GameStatus.GetClone();
									WorldGameMaster.I.Status.StartPointDirection = 101; // スタート地点を「ロード地点」にする。
									WorldGameMaster.I.Perform();
								}
								this.ReturnTitleMenu();
							}
						}
						break;

					case 3:
						using (new SettingMenu()
						{
							SimpleMenu = this.SimpleMenu,
						})
						{
							SettingMenu.I.Perform();
						}
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
				this.SimpleMenu.WallDrawer();
				DDEngine.EachFrame();
			}

			DDEngine.FreezeInput();
		}

		private static Ground.P_SaveDataSlot LoadGame() // ret: null == キャンセル, ret.GameStatus を使用する際は GetClone を忘れずに！
		{
			Ground.P_SaveDataSlot saveDataSlot = null;

			DDEngine.FreezeInput();

			DDCurtain.SetCurtain();
			DDEngine.FreezeInput();

			DDSimpleMenu simpleMenu = new DDSimpleMenu()
			{
				BorderColor = new I3Color(0, 128, 0),
				WallDrawer = () =>
				{
					DDDraw.SetBright(new I3Color(64, 64, 128));
					DDDraw.DrawRect(Ground.I.Picture.WhiteBox, 0, 0, DDConsts.Screen_W, DDConsts.Screen_H);
					DDDraw.Reset();
				},
			};

			string[] items = Ground.I.SaveDataSlots.Select(v => v.GameStatus == null ?
				"----" :
				"[" + v.TimeStamp + "]\u3000" + v.Description).Concat(new string[] { "\u623b\u308b" }).ToArray();

			int selectIndex = 0;

			for (; ; )
			{
				selectIndex = simpleMenu.Perform(18, 18, 32, 24, "\u30ed\u30fc\u30c9", items, selectIndex);

				if (selectIndex < Consts.SAVE_DATA_SLOT_NUM)
				{
					if (Ground.I.SaveDataSlots[selectIndex].GameStatus != null)
					{
						if (new Confirm() { BorderColor = new I3Color(50, 100, 200) }
							.Perform("\u30b9\u30ed\u30c3\u30c8 " + (selectIndex + 1) + " \u306e\u30c7\u30fc\u30bf\u3092\u30ed\u30fc\u30c9\u3057\u307e\u3059\u3002", "\u306f\u3044", "\u3044\u3044\u3048") == 0)
						{
							saveDataSlot = Ground.I.SaveDataSlots[selectIndex];
							break;
						}
					}
				}
				else // [戻る]
				{
					break;
				}
				//DDEngine.EachFrame(); // 不要
			}
			DDEngine.FreezeInput();

			return saveDataSlot;
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
