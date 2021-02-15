using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Commons;
using Charlotte.GameCommons;
using Charlotte.Games.Scripts;
using Charlotte.Games.Scripts.Tests;
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

		#region 背景

		private DDTask 背景 = new 背景Task();

		private class 背景Task : DDTask
		{
			private DDTask SnowPanel_01 = new SnowPanelTask() { RMin = 1.0, RMax = 2.0 };
			private DDTask SnowPanel_02 = new SnowPanelTask() { RMin = 2.0, RMax = 3.0 };
			private DDTask SnowPanel_03 = new SnowPanelTask() { RMin = 3.0, RMax = 4.0 };
			private DDTask SnowPanel_04 = new SnowPanelTask() { RMin = 4.0, RMax = 5.0 };

			private DDTask Panel_01 = new PanelTask_01();
			private DDTask Panel_02 = new PanelTask_02();
			private DDTask Panel_03 = new PanelTask_03();

			public override IEnumerable<bool> E_Task()
			{
				for (; ; )
				{
					DDCurtain.DrawCurtain();

					this.SnowPanel_01.Execute();
					this.Panel_01.Execute();
					this.SnowPanel_02.Execute();
					this.Panel_02.Execute();
					this.SnowPanel_03.Execute();
					this.Panel_03.Execute();
					this.SnowPanel_04.Execute();

					yield return true;
				}
			}

			private class SnowPanelTask : DDTask
			{
				public double RMin;
				public double RMax;

				// <---- prm

				private DDTaskList Snows = new DDTaskList();
				private double PutRate = 0.0;

				public override IEnumerable<bool> E_Task()
				{
					for (; ; )
					{
						DDUtils.Approach(ref this.PutRate, 0.6, 0.999);

						if (DDUtils.Random.Real() < this.PutRate)
						{
							double r = DDUtils.AToBRate(this.RMin, this.RMax, DDUtils.Random.Real());

							this.Snows.Add(new SnowInfo()
							{
								X = DDUtils.Random.Real() * DDConsts.Screen_W,
								Y = -10.0,
								R = r,
								XAdd = DDUtils.Random.Real() * -3.0,
								YAdd = r * 1.5,
							}
							.Task
							);
						}

						this.Snows.ExecuteAllTask();

						yield return true;
					}
				}

				private class SnowInfo : DDTask
				{
					public double X;
					public double Y;
					public double R;
					public double XAdd;
					public double YAdd;

					// <---- prm

					public override IEnumerable<bool> E_Task()
					{
						for (; ; )
						{
							this.X += this.XAdd;
							this.Y += this.YAdd;

							if (this.X < 0.0)
								this.X += DDConsts.Screen_W;

							DDDraw.SetAlpha(0.2);
							DDDraw.DrawBegin(Ground.I.Picture.WhiteCircle, this.X, this.Y);
							DDDraw.DrawZoom(this.R / (Ground.I.Picture.WhiteCircle.Get_W() / 2.0));
							DDDraw.DrawEnd();
							DDDraw.Reset();

							yield return this.Y < DDConsts.Screen_H + 10.0;
						}
					}
				}
			}

			private class PanelTask_01 : DDTask
			{
				private DDPicture PIC = Ground.I.Picture.Wall0001;
				private const int SPEED = 3;
				private const double TARGET_PUT_RATE = 0.666;
				private const double PR_APPROACHING_RATE = 0.91;

				private DDTaskList Tiles = new DDTaskList();
				private double PutRate = 0.0;

				public override IEnumerable<bool> E_Task()
				{
					for (int slide = 0; ; slide += SPEED)
					{
						if (PIC.Get_W() < slide)
						{
							slide -= PIC.Get_W();
							DDUtils.Approach(ref this.PutRate, TARGET_PUT_RATE, PR_APPROACHING_RATE);

							for (int y = 0; y < DDConsts.Screen_H; y += PIC.Get_H())
								if (DDUtils.Random.Real() < this.PutRate)
									this.Tiles.Add(new TileTask() { PIC = PIC, X = DDConsts.Screen_W - slide, Y = y }.Task);
						}
						this.Tiles.ExecuteAllTask();
						yield return true;
					}
				}

				private class TileTask : DDTask
				{
					public DDPicture PIC;
					public int X;
					public int Y;

					// <---- prm

					public override IEnumerable<bool> E_Task()
					{
						while (-PIC.Get_W() < this.X)
						{
							DDDraw.DrawSimple(PIC, this.X, this.Y);
							this.X -= SPEED;
							yield return true;
						}
					}
				}
			}

			private class PanelTask_02 : DDTask
			{
				private DDPicture PIC = Ground.I.Picture.Wall0002;
				private const int SPEED = 5;
				private const double TARGET_PUT_RATE = 0.666;
				private const double PR_APPROACHING_RATE = 0.99;

				private DDTaskList Tiles = new DDTaskList();
				private double PutRate = 0.0;

				public override IEnumerable<bool> E_Task()
				{
					for (int slide = 0; ; slide += SPEED)
					{
						if (PIC.Get_W() < slide)
						{
							slide -= PIC.Get_W();
							DDUtils.Approach(ref this.PutRate, TARGET_PUT_RATE, PR_APPROACHING_RATE);

							for (int y = 0; y < DDConsts.Screen_H; y += PIC.Get_H())
								if (DDUtils.Random.Real() < this.PutRate)
									this.Tiles.Add(new TileTask() { PIC = PIC, X = DDConsts.Screen_W - slide, Y = y }.Task);
						}
						this.Tiles.ExecuteAllTask();
						yield return true;
					}
				}

				private class TileTask : DDTask
				{
					public DDPicture PIC;
					public int X;
					public int Y;

					// <---- prm

					public override IEnumerable<bool> E_Task()
					{
						while (-PIC.Get_W() < this.X)
						{
							DDDraw.DrawSimple(PIC, this.X, this.Y);
							this.X -= SPEED;
							yield return true;
						}
					}
				}
			}

			private class PanelTask_03 : DDTask
			{
				private DDPicture PIC = Ground.I.Picture.Wall0003;
				private const int SPEED = 7;
				private const double TARGET_PUT_RATE = 0.333;
				private const double PR_APPROACHING_RATE = 0.99;

				private DDTaskList Tiles = new DDTaskList();
				private double PutRate = 0.0;

				public override IEnumerable<bool> E_Task()
				{
					for (int slide = 0; ; slide += SPEED)
					{
						if (PIC.Get_W() < slide)
						{
							slide -= PIC.Get_W();
							DDUtils.Approach(ref this.PutRate, TARGET_PUT_RATE, PR_APPROACHING_RATE);

							for (int y = 0; y < DDConsts.Screen_H; y += PIC.Get_H())
								if (DDUtils.Random.Real() < this.PutRate)
									this.Tiles.Add(new TileTask() { PIC = PIC, X = DDConsts.Screen_W - slide, Y = y }.Task);
						}
						this.Tiles.ExecuteAllTask();
						yield return true;
					}
				}

				private class TileTask : DDTask
				{
					public DDPicture PIC;
					public int X;
					public int Y;

					// <---- prm

					public override IEnumerable<bool> E_Task()
					{
						while (-PIC.Get_W() < this.X)
						{
							DDDraw.DrawSimple(PIC, this.X, this.Y);
							this.X -= SPEED;
							yield return true;
						}
					}
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

			this.SimpleMenu.BorderColor = new I3Color(0, 64, 128);
			//this.SimpleMenu.WallColor = new I3Color(60, 120, 130);
			//this.SimpleMenu.WallPicture = Ground.I.Picture.P_TITLE_WALL;
			//this.SimpleMenu.WallCurtain = -0.8;
			this.SimpleMenu.WallDrawer = this.背景.Execute;

			for (; ; )
			{
				selectIndex = this.SimpleMenu.Perform("横シュー・テストコード / タイトルメニュー", items, selectIndex);

				switch (selectIndex)
				{
					case 0:
						{
							this.LeaveTitleMenu();

							using (new Game())
							{
								Game.I.Script = new Script_Bステージ0001(); // 仮？
								Game.I.Perform();
							}
							this.ReturnTitleMenu();
						}
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
