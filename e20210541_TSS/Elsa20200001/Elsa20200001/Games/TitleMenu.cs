﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Commons;
using Charlotte.GameCommons;

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

		#region DrawWall

		private DrawWallTask DrawWall = new DrawWallTask();

		private class DrawWallTask : DDTask
		{
			public bool TopMenuLeaved = false;

			public override IEnumerable<bool> E_Task()
			{
				DDPicture picture = Ground.I.Picture.Title;

				double slideRate = 0.0;
				double leaveRate = 0.0;
				double z = 1.0;

				for (; ; )
				{
					DDUtils.Approach(ref slideRate, 1.0, 0.9999);
					DDUtils.Approach(ref leaveRate, this.TopMenuLeaved ? 1.0 : 0.0, 0.95);
					DDUtils.Approach(ref z, this.TopMenuLeaved ? 1.02 : 1.0, 0.9);

					D4Rect drawRect = DDUtils.AdjustRectExterior(
						picture.GetSize().ToD2Size(),
						new D4Rect(0, 0, DDConsts.Screen_W, DDConsts.Screen_H),
						slideRate
						);

					DDDraw.DrawBeginRect(Ground.I.Picture.Title, drawRect.L, drawRect.T, drawRect.W, drawRect.H);
					DDDraw.DrawZoom(z);
					DDDraw.DrawEnd();

					if (0.01 < leaveRate)
					{
						ぼかし効果.Perform(leaveRate);
						DDCurtain.DrawCurtain(-0.6 * leaveRate);
					}
					yield return true;
				}
			}
		}

		#endregion

		#region TopMenu

		private TopMenuTask TopMenu = new TopMenuTask();

		private class TopMenuTask : DDTask
		{
			public const int ITEM_NUM = 4;
			public int SelectIndex = -1; // -1 == 未選択

			public List<ItemTask> Items = new List<ItemTask>();

			private const double ITEM_X = 220;
			private const double ITEM_Y = 720;
			private const double ITEM_Y_STEP = 90;
			private const double ITEM_A = 0.5;
			private const double ITEM_W = 440;
			private const double ITEM_H = 80;

			private const double ITEM_SEL_X = 240.0;
			private const double ITEM_SEL_A = 1.0;

			public TopMenuTask()
			{
				for (int index = 0; index < ITEM_NUM; index++)
				{
					this.Items.Add(new ItemTask(index));
				}
			}

			public class ItemTask : DDTask
			{
				public int SelfIndex;

				public ItemTask(int selfIndex)
				{
					this.SelfIndex = selfIndex;
				}

				public bool Selected = false;

				public override IEnumerable<bool> E_Task()
				{
					DDPicture picture = new DDPicture[]
					{
						Ground.I.Picture.TitleMenuItem_はじめから,
						Ground.I.Picture.TitleMenuItem_つづきから,
						Ground.I.Picture.TitleMenuItem_設定,
						Ground.I.Picture.TitleMenuItem_終了,
					}
					[this.SelfIndex];

					double targX = ITEM_X;
					double targA = ITEM_A;

					double x = targX;
					double y = ITEM_Y + this.SelfIndex * ITEM_Y_STEP;
					double a = targA;

					for (; ; )
					{
						targX = TitleMenu.I.TopMenu.SelectIndex == this.SelfIndex ? ITEM_SEL_X : ITEM_X;
						targA = TitleMenu.I.TopMenu.SelectIndex == this.SelfIndex ? ITEM_SEL_A : ITEM_A;

						DDUtils.Approach(ref x, targX, 0.93);
						DDUtils.Approach(ref a, targA, 0.93);

						DDDraw.SetAlpha(a);
						DDDraw.DrawCenter(picture, x, y);
						DDDraw.Reset();

						this.Selected = !DDUtils.IsOut(new D2Point(DDMouse.X, DDMouse.Y), new D4Rect(ITEM_X - ITEM_W / 2, y - ITEM_H / 2, ITEM_W, ITEM_H));

						yield return true;
					}
				}

				public void マウスカーソルをここへ移動()
				{
					const double MOUSE_XY_MGN = 3.0;

					DDMouse.X = (int)(ITEM_X + ITEM_W / 2 - MOUSE_XY_MGN);
					DDMouse.Y = (int)(ITEM_Y + ITEM_H / 2 - MOUSE_XY_MGN + this.SelfIndex * ITEM_Y_STEP);

					DDMouse.PosChanged();
				}
			}

			public override IEnumerable<bool> E_Task()
			{
				for (; ; )
				{
					foreach (ItemTask item in this.Items)
						item.Execute();

					this.SelectIndex = -1;

					foreach (ItemTask item in this.Items)
						if (item.Selected)
							this.SelectIndex = item.SelfIndex;

					yield return true;
				}
			}
		}

		#endregion

		public void Perform()
		{
			DDUtils.SetMouseDispMode(true); // 2bs -- 既にマウス有効であるはず

			DDCurtain.SetCurtain(0, -1.0);
			DDCurtain.SetCurtain();

			DDEngine.FreezeInput();

			Ground.I.Music.Title.Play();

			this.SimpleMenu = new DDSimpleMenu();
			this.SimpleMenu.Color = new I3Color(255, 255, 128);
			this.SimpleMenu.BorderColor = new I3Color(0, 0, 100);
			this.SimpleMenu.WallDrawer = this.DrawWall.Execute;

			for (; ; )
			{
				this.DrawWall.Execute();
				this.TopMenu.Execute();

				int moving = 0;

				if (DDInput.DIR_8.IsPound())
					moving = -1;

				if (DDInput.DIR_2.IsPound())
					moving = 1;

				if (moving != 0)
				{
					if (this.TopMenu.SelectIndex == -1)
					{
						this.TopMenu.SelectIndex = moving == 1 ? 0 : TopMenuTask.ITEM_NUM - 1;
					}
					else
					{
						this.TopMenu.SelectIndex += moving;
						this.TopMenu.SelectIndex += TopMenuTask.ITEM_NUM;
						this.TopMenu.SelectIndex %= TopMenuTask.ITEM_NUM;
					}
					this.TopMenu.Items[this.TopMenu.SelectIndex].マウスカーソルをここへ移動();
				}

				if (
					this.TopMenu.SelectIndex != -1 &&
					(
						DDMouse.L.GetInput() == -1 ||
						DDInput.A.GetInput() == 1
					)
					)
				{
					switch (this.TopMenu.SelectIndex)
					{
						case 0:
							if (DDConfig.LOG_ENABLED && 1 <= DDInput.DIR_6.GetInput())
							{
								this.CheatMainMenu();
							}
							else
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
#if true
								this.DrawWall.TopMenuLeaved = true;
								SaveDataSlot saveDataSlot;

								using (new SaveOrLoadMenu())
								{
									saveDataSlot = SaveOrLoadMenu.I.Load(this.DrawWall.Execute);
								}
								this.DrawWall.TopMenuLeaved = false;
#else // old
								this.DrawWall.TopMenuLeaved = true;
								SaveDataSlot saveDataSlot = this.LoadGame();
								this.DrawWall.TopMenuLeaved = false;
#endif

								if (saveDataSlot != null)
								{
									this.LeaveTitleMenu();

									using (new Game())
									{
										Game.I.Status = GameStatus.Deserialize(saveDataSlot.SerializedGameStatus);
										Game.I.Perform(true);
									}
									this.ReturnTitleMenu();
								}
							}
							break;

						case 2:
#if true
							this.DrawWall.TopMenuLeaved = true;

							using (new SettingMenu())
							{
								SettingMenu.I.Perform(this.DrawWall.Execute);
							}
							this.DrawWall.TopMenuLeaved = false;
#else // old
							this.DrawWall.TopMenuLeaved = true;
							this.Setting();
							this.DrawWall.TopMenuLeaved = false;
#endif
							break;

						case 3:
							goto endMenu;

						default:
							throw null; // never
					}
				}
				if (DDMouse.R.GetInput() == -1)
					goto endMenu;

				if (DDInput.B.GetInput() == 1)
				{
					if (this.TopMenu.SelectIndex == TopMenuTask.ITEM_NUM - 1)
						goto endMenu;

					this.TopMenu.Items[TopMenuTask.ITEM_NUM - 1].マウスカーソルをここへ移動();
				}
				DDEngine.EachFrame();
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

		private SaveDataSlot LoadGame()
		{
			SaveDataSlot saveDataSlot = null;

			DDCurtain.SetCurtain();
			DDEngine.FreezeInput();

			string[] items = Ground.I.SaveDataSlots.Select(v =>
				v.SavedTime.Year == 1 ?
				"----" :
				"[" + v.SavedTime.ToString() + "]").Concat(new string[] { "戻る" }).ToArray();

			int selectIndex = 0;

			for (; ; )
			{
				selectIndex = this.SimpleMenu.Perform("コンテニュー", items, selectIndex);

				if (selectIndex < Consts.SAVE_DATA_SLOT_NUM)
				{
					if (Ground.I.SaveDataSlots[selectIndex].SerializedGameStatus != null) // ロードする。
					{
						if (new Confirm().Perform("スロット " + (selectIndex + 1) + " のデータをロードします。", "はい", "いいえ") == 0)
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
			}
			DDEngine.FreezeInput();

			return saveDataSlot;
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

			DDSE[] seSamples = new DDSE[]
			{
				Ground.I.SE.Poka01,
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

		private void CheatMainMenu()
		{
			for (; ; )
			{
				int selectIndex = this.SimpleMenu.Perform("開発デバッグ用メニュー", new string[]
				{
					"スタート",
					"戻る",
				},
				0
				);

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
						goto endMenu;

					default:
						throw new DDError();
				}
			}
		endMenu:
			;
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
			DDTouch.Touch(); // 曲再生の前に -- .Play() で Touch した曲を解放してしまわないように
			Ground.I.Music.Title.Play();

			DDCurtain.SetCurtain();

			DDEngine.FreezeInput(GameConsts.LONG_INPUT_SLEEP);

			GC.Collect();
		}
	}
}
