using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Commons;
using Charlotte.GameCommons;
using Charlotte.GameProgressMasters;

namespace Charlotte.Games
{
	/// <summary>
	/// アプリ固有のタイトルメニュー
	/// </summary>
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

		#region DrawWall

		private DrawWallTask DrawWall = new DrawWallTask();

		private class DrawWallTask : DDTask
		{
			public bool TopMenuLeaved = false;

			private DDTaskList Tiles = new DDTaskList();

			private const int TILES_L = -20;
			private const int TILES_T = -5;
			private const int TILES_X_NUM = 20;
			private const int TILES_Y_NUM = 11;
			private const int TILE_W = 50;
			private const int TILE_H = 50;

			public DrawWallTask()
			{
				for (int x = 0; x < TILES_X_NUM; x++)
				{
					for (int y = 0; y < TILES_Y_NUM; y++)
					{
						double x1;
						double y1;

						if (DDUtils.Random.GetInt(2) == 0) // rand 50 %
						{
							x1 = DDUtils.Random.Real() * DDConsts.Screen_W;
							y1 = DDUtils.Random.ChooseOne(new int[] { -TILE_H, DDConsts.Screen_H + TILE_H });
						}
						else // rand 50 %
						{
							x1 = DDUtils.Random.ChooseOne(new int[] { -TILE_W, DDConsts.Screen_W + TILE_W });
							y1 = DDUtils.Random.Real() * DDConsts.Screen_H;
						}

						double x2 = TILES_L + x * TILE_W + TILE_W / 2;
						double y2 = TILES_T + y * TILE_H + TILE_H / 2;

						this.Tiles.Add(new TileTask(x1, y1, x2, y2).Task);
					}
				}
			}

			private class TileTask : DDTask
			{
				private bool _lastTopMenuLeaved = false;

				private I3Color _color = new I3Color(
					DDUtils.Random.GetRange(15, 30),
					DDUtils.Random.GetRange(30, 60),
					DDUtils.Random.GetRange(60, 90)
					);

				private double _x1;
				private double _y1;
				private double _x2;
				private double _y2;
				private double _rot = (DDUtils.Random.Real() * 2.0 - 1.0) * 50.0;

				public TileTask(double x1, double y1, double x2, double y2)
				{
					_x1 = x1;
					_y1 = y1;
					_x2 = x2;
					_y2 = y2;
				}

				public override IEnumerable<bool> E_Task()
				{
					for (; ; )
					{
						if (_lastTopMenuLeaved != TitleMenu.I.DrawWall.TopMenuLeaved)
						{
							if (TitleMenu.I.DrawWall.TopMenuLeaved)
							{
								foreach (bool v in this.E_Move(_x1, _y1, _x2, _y2, _rot, 0.0))
									yield return v;
							}
							else
							{
								foreach (bool v in this.E_Move(_x2, _y2, _x1, _y1, 0.0, _rot))
									yield return v;
							}
							_lastTopMenuLeaved = TitleMenu.I.DrawWall.TopMenuLeaved;
						}
						if (TitleMenu.I.DrawWall.TopMenuLeaved)
						{
							DDDraw.SetBright(_color);
							DDDraw.DrawBegin(Ground.I.Picture.WhiteBox, _x2, _y2);
							DDDraw.DrawSetSize(TILE_W, TILE_H);
							DDDraw.DrawEnd();
							DDDraw.Reset();
						}
						yield return true;
					}
				}

				private IEnumerable<bool> E_Move(double sx, double sy, double dx, double dy, double sRot, double dRot)
				{
					foreach (DDScene scene in DDSceneUtils.Create(DDUtils.Random.GetRange(20, 60)))
					{
						DDDraw.SetBright(_color);
						DDDraw.DrawBegin(
							Ground.I.Picture.WhiteBox,
							DDUtils.AToBRate(sx, dx, DDUtils.SCurve(scene.Rate)),
							DDUtils.AToBRate(sy, dy, DDUtils.SCurve(scene.Rate))
							);
						DDDraw.DrawSetSize(TILE_W, TILE_H);
						DDDraw.DrawRotate(DDUtils.AToBRate(sRot, dRot, DDUtils.SCurve(scene.Rate)));
						DDDraw.DrawEnd();
						DDDraw.Reset();

						yield return true;
					}
				}
			}

			public override IEnumerable<bool> E_Task()
			{
				for (; ; )
				{
					DDDraw.DrawSimple(Ground.I.Picture.Title, 0, 0);

					this.Tiles.ExecuteAllTask();

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
			public int SelectIndex = 0;

			private DDTaskList Items = new DDTaskList();

			private const double ITEM_UNSEL_X = 150.0;
			private const double ITEM_UNSEL_A = 0.5;
			private const double ITEM_SEL_X = 180.0;
			private const double ITEM_SEL_A = 1.0;
			private const double ITEM_Y = 250.0;
			private const double ITEM_Y_STEP = 70.0;

			public TopMenuTask()
			{
				for (int index = 0; index < ITEM_NUM; index++)
				{
					this.Items.Add(new ItemTask(index).Task);
				}
			}

			private class ItemTask : DDTask
			{
				private int SelfIndex;

				public ItemTask(int selfIndex)
				{
					this.SelfIndex = selfIndex;
				}

				public override IEnumerable<bool> E_Task()
				{
					DDPicture picture = Ground.I.Picture.TitleMenuItems[this.SelfIndex];

					double targX = ITEM_UNSEL_X;
					double targA = ITEM_UNSEL_A;

					double x = targX;
					double y = ITEM_Y + this.SelfIndex * ITEM_Y_STEP;
					double a = targA;

					for (; ; )
					{
						targX = TitleMenu.I.TopMenu.SelectIndex == this.SelfIndex ? ITEM_SEL_X : ITEM_UNSEL_X;
						targA = TitleMenu.I.TopMenu.SelectIndex == this.SelfIndex ? ITEM_SEL_A : ITEM_UNSEL_A;

						DDUtils.Approach(ref x, targX, 0.93);
						DDUtils.Approach(ref a, targA, 0.93);

						DDDraw.SetAlpha(a);
						DDDraw.DrawCenter(picture, x, y);
						DDDraw.Reset();

						yield return true;
					}
				}
			}

			public override IEnumerable<bool> E_Task()
			{
				for (; ; )
				{
					this.Items.ExecuteAllTask();

					yield return true;
				}
			}
		}

		#endregion

		private DDSimpleMenu SimpleMenu;

		public void Perform()
		{
			DDCurtain.SetCurtain(0, -1.0);
			DDCurtain.SetCurtain();

			DDEngine.FreezeInput();

			//Ground.I.Music.Title.Play();

			this.SimpleMenu = new DDSimpleMenu();

			this.SimpleMenu.BorderColor = new I3Color(0, 96, 192);
			//this.SimpleMenu.WallColor = new I3Color(0, 64, 96);
			//this.SimpleMenu.WallPicture = Ground.I.Picture.Title;
			//this.SimpleMenu.WallCurtain = -0.4;
			this.SimpleMenu.WallDrawer = this.DrawWall.Execute;

			for (; ; )
			{
				if (DDInput.DIR_8.IsPound())
					this.TopMenu.SelectIndex--;

				if (DDInput.DIR_2.IsPound())
					this.TopMenu.SelectIndex++;

				this.TopMenu.SelectIndex += TopMenuTask.ITEM_NUM;
				this.TopMenu.SelectIndex %= TopMenuTask.ITEM_NUM;

				if (DDInput.A.GetInput() == 1) // ? 決定ボタン押下
				{
					switch (this.TopMenu.SelectIndex)
					{
						case 0:
							{
								this.LeaveTitleMenu();

								using (new GameProgressMaster())
								{
									GameProgressMaster.I.StartStageIndex = 1;
									GameProgressMaster.I.Perform();
								}
								this.ReturnTitleMenu();
							}
							break;

						case 1:
							this.DrawWall.TopMenuLeaved = true;
							this.SelectStage();
							this.DrawWall.TopMenuLeaved = false;
							break;

						case 2:
							this.DrawWall.TopMenuLeaved = true;
							this.Setting();
							this.DrawWall.TopMenuLeaved = false;
							break;

						case 3:
							goto endMenu;

						default:
							throw new DDError();
					}
				}
				if (DDInput.B.GetInput() == 1) // ? キャンセルボタン押下
				{
					if (this.TopMenu.SelectIndex == TopMenuTask.ITEM_NUM - 1)
						break;

					this.TopMenu.SelectIndex = TopMenuTask.ITEM_NUM - 1;
				}

				this.DrawWall.Execute();
				this.TopMenu.Execute();

				DDEngine.EachFrame();
			}
		endMenu:
			DDMusicUtils.Fade();
			DDCurtain.SetCurtain(30, -1.0);

			foreach (DDScene scene in DDSceneUtils.Create(40))
			{
				//this.SimpleMenu.DrawWall();
				this.DrawWall.Execute();

				DDEngine.EachFrame();
			}

			DDEngine.FreezeInput();
		}

		private void SelectStage()
		{
			DDCurtain.SetCurtain();
			DDEngine.FreezeInput();

			// memo:
			// selectIndex                 -->  0 ～  8 == LAYER 9 ～ 1
			// *stageIndex                 -->  1 ～  9 == LAYER 9 ～ 1         , 0 == テスト用ステージ
			// Ground.I.ReachedStageIndex  -->  2 ～ 10 == LAYER 9 ～ 1 Cleared , 0 == クリアステージ無し

			int selectIndex = Math.Min(Math.Max(0, Ground.I.ReachedStageIndex - 1), 8);

			for (; ; )
			{
				string[] items = Enumerable.Range(0, 9)
					.Select(stageIndex => "LAYER " + (9 - stageIndex) + (Math.Max(0, Ground.I.ReachedStageIndex - 1) < stageIndex ? " (未到達)" : ""))
					.Concat(new string[] { "戻る" })
					.ToArray();

				selectIndex = this.SimpleMenu.Perform("コンテニュー", items, selectIndex);

				if (selectIndex == items.Length - 1) // ? 戻る
					break;

				int selectedStageIndex = selectIndex + 1;

				if (selectedStageIndex <= Ground.I.ReachedStageIndex || DDConfig.LOG_ENABLED)
				{
					this.LeaveTitleMenu();

					using (new GameProgressMaster())
					{
						GameProgressMaster.I.StartStageIndex = selectedStageIndex;
						GameProgressMaster.I.Perform();
					}
					this.ReturnTitleMenu();

					//break; // タイトルメニューへ戻っておく。
				}
			}
			DDEngine.FreezeInput();
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
				"スナップショット・ストック数(リスポーン地点設置回数)",
				"戻る",
			};

			DDSE[] seSamples = new DDSE[]
			{
				Ground.I.SE.Dummy, // TODO
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
						this.Setting_SnapshotCountMax();
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

		private void Setting_SnapshotCountMax()
		{
			DDCurtain.SetCurtain();
			DDEngine.FreezeInput();

			int selectIndex = Ground.I.StartSnapshotCount;

			for (; ; )
			{
				string[] items = Enumerable.Range(Consts.START_SNAPSHOT_COUNT_MIN, Consts.START_SNAPSHOT_COUNT_MAX - Consts.START_SNAPSHOT_COUNT_MIN + 1)
					.Select(v => (v == Ground.I.StartSnapshotCount ? "*" : " ") + " " + v + (v == Consts.START_SNAPSHOT_COUNT_DEF ? " (デフォルト)" : ""))
					.Concat(new string[] { "戻る" })
					.ToArray();

				selectIndex = this.SimpleMenu.Perform("スナップショット・ストック数(リスポーン地点設置回数)の設定", items, selectIndex);

				if (selectIndex == items.Length - 1) // ? 戻る
					break;

				Ground.I.StartSnapshotCount = selectIndex;
			}
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
			//Ground.I.Music.Title.Play();

			DDCurtain.SetCurtain();

			GC.Collect();
		}
	}
}
