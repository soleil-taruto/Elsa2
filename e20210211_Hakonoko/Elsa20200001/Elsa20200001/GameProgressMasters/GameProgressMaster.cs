﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Charlotte.Commons;
using Charlotte.GameCommons;
using Charlotte.Games.Designs;
using Charlotte.Games;
using Charlotte.Novels;

namespace Charlotte.GameProgressMasters
{
	public class GameProgressMaster : IDisposable
	{
		/// <summary>
		/// ステージの番号(インデックス)
		/// 0 == テストステージ
		/// 1～9 == 実際のステージ
		/// </summary>
		public int StartStageIndex;

		// <---- prm

		public static GameProgressMaster I;

		public GameProgressMaster()
		{
			I = this;
		}

		public void Dispose()
		{
			I = null;
		}

		private I3Color[] ThemeColors = new I3Color[]
		{
			new I3Color(255, 255, 255), // テスト用ステージ
			new I3Color(0, 255, 255),
			new I3Color(255, 255, 0),
			new I3Color(255, 0, 255),
			new I3Color(200, 200, 255),
			new I3Color(128, 255, 0),
			new I3Color(255, 0, 0),
			new I3Color(255, 128, 255),
			new I3Color(255, 255, 128),
			new I3Color(255, 255, 255),
		};

		private Func<Map>[] MapLoaders = new Func<Map>[]
		{
			() => new Map(@"res\Map\0000.bmp", new Design_0000()),
			() => new Map(@"res\Map\0001.bmp", new Design_0001(
				Ground.I.Picture.WallPicture_0101,
				Ground.I.Picture.WallPicture_0102,
				Ground.I.Picture.WallPicture_0103,
				new I3Color(30, 30, 100),
				new I3Color(30, 60, 150),
				new I3Color(30, 90, 200),
				new I3Color(0, 120, 180),
				new I3Color(0, 180, 220),
				new I3Color(180, 70, 200),
				new I3Color(32, 192, 32),
				new I3Color(30, 0, 0),
				new I3Color(255, 0, 0),
				new I3Color(192, 32, 32),
				0.9,
				true,
				instance => { }
				)),
			() => new Map(@"res\Map\0002.bmp", new Design_0001(
				Ground.I.Picture.WallPicture_0201,
				Ground.I.Picture.WallPicture_0202,
				Ground.I.Picture.WallPicture_0203,
				new I3Color(100, 30, 0),
				new I3Color(90, 90, 0),
				new I3Color(60, 60, 0),
				new I3Color(200, 150, 0),
				new I3Color(60, 120, 90),
				new I3Color(200, 70, 170),
				new I3Color(32, 207, 32),
				new I3Color(30, 0, 0),
				new I3Color(255, 0, 0),
				new I3Color(222, 32, 32),
				0.9,
				true,
				instance => { }
				)),
			() => new Map(@"res\Map\0003.bmp", new Design_0001(
				Ground.I.Picture.WallPicture_0301,
				Ground.I.Picture.WallPicture_0302,
				Ground.I.Picture.WallPicture_0303,
				new I3Color(30, 0, 30),
				new I3Color(45, 30, 60),
				new I3Color(75, 30, 60),
				new I3Color(120, 30, 150),
				new I3Color(200, 150, 150),
				new I3Color(105, 70, 230),
				new I3Color(32, 192, 32),
				new I3Color(30, 0, 0),
				new I3Color(255, 0, 0),
				new I3Color(192, 32, 32),
				0.9,
				true,
				instance => { }
				)),
			() => new Map(@"res\Map\0004.bmp", new Design_0001(
				Ground.I.Picture.WallPicture_0401,
				Ground.I.Picture.WallPicture_0402,
				Ground.I.Picture.WallPicture_0403,
				new I3Color(30, 30, 30),
				new I3Color(30, 45, 60),
				new I3Color(45, 60, 75),
				new I3Color(75, 70, 75),
				new I3Color(130, 130, 130),
				new I3Color(180, 70, 200),
				new I3Color(32, 192, 32),
				new I3Color(30, 0, 0),
				new I3Color(255, 0, 0),
				new I3Color(192, 32, 32),
				0.9,
				true,
				instance => { }
				)),
			() => new Map(@"res\Map\0005.bmp", new Design_0001(
				Ground.I.Picture.WallPicture_0501,
				Ground.I.Picture.WallPicture_0502,
				Ground.I.Picture.WallPicture_0503,
				new I3Color(100, 15, 30),
				new I3Color(60, 30, 30),
				new I3Color(15, 15, 15),
				new I3Color(105, 105, 100),
				new I3Color(100, 220, 100),
				new I3Color(210, 70, 200),
				new I3Color(32, 252, 32),
				new I3Color(30, 0, 0),
				new I3Color(255, 0, 0),
				new I3Color(255, 25, 50),
				0.9,
				true,
				instance => { }
				)),
			() => new Map(@"res\Map\0006.bmp", new Design_0001(
				Ground.I.Picture.WallPicture_0601,
				Ground.I.Picture.WallPicture_0602,
				Ground.I.Picture.WallPicture_0603,
				new I3Color(15, 30, 45),
				new I3Color(45, 30, 15),
				new I3Color(130, 0, 0),
				new I3Color(120, 120, 120),
				new I3Color(60, 75, 75),
				new I3Color(180, 70, 200),
				new I3Color(32, 192, 32),
				new I3Color(30, 0, 0),
				new I3Color(255, 0, 0),
				new I3Color(207, 32, 17),
				0.9,
				true,
				instance => { }
				)),
			() => new Map(@"res\Map\0007.bmp", new Design_0001(
				Ground.I.Picture.WallPicture_0701,
				Ground.I.Picture.WallPicture_0702,
				Ground.I.Picture.WallPicture_0703,
				new I3Color(15, 30, 15),
				new I3Color(75, 60, 60),
				new I3Color(45, 15, 30),
				new I3Color(135, 40, 70),
				new I3Color(145, 175, 255),
				new I3Color(210, 60, 255), // EA
				new I3Color(77, 177, 47),
				new I3Color(255, 60, 0),
				new I3Color(60, 0, 0),
				new I3Color(255, 120, 120),
				1.0,
				false,
				instance => { }
				)),
			() => new Map(@"res\Map\0008.bmp", new Design_0001(
				Ground.I.Picture.WallPicture_0801,
				Ground.I.Picture.WallPicture_0802,
				Ground.I.Picture.WallPicture_0803,
				new I3Color(90, 90, 30),
				new I3Color(60, 60, 0),
				new I3Color(10, 20, 30),
				new I3Color(135, 135, 90),
				new I3Color(165, 165, 150),
				new I3Color(150, 100, 200),
				new I3Color(32, 192, 32),
				new I3Color(30, 0, 0),
				new I3Color(255, 0, 0),
				new I3Color(192, 32, 32),
				0.9,
				true,
				instance => { }
				)),
			() => new Map(@"res\Map\0009.bmp", new Design_0001(
				Ground.I.Picture.WallPicture_0901,
				Ground.I.Picture.WallPicture_0902,
				Ground.I.Picture.WallPicture_0903,
				new I3Color(255, 0, 75),
				new I3Color(210, 45, 75),
				new I3Color(255, 0, 0),
				new I3Color(45, 0, 0),
				new I3Color(30, 45, 30),
				new I3Color(0, 0, 30), // EA
				new I3Color(0, 5, 0),
				new I3Color(100, 0, 0),
				new I3Color(255, 0, 0),
				new I3Color(60, 0, 0),
				0.9,
				false,
				instance =>
				{
					instance.CookieAxisColor = new I3Color(0, 0, 0);
				}
				)),
		};

		private Func<Scenario>[] ScenarioLoaders = new Func<Scenario>[]
		{
			() => new Scenario(NovelConsts.DUMMY_SCENARIO_NAME),
			() => new Scenario(@"ステージ0001"),
			() => new Scenario(@"ステージ0002"),
			() => new Scenario(@"ステージ0003"),
			() => new Scenario(@"ステージ0004"),
			() => new Scenario(@"ステージ0005"),
			() => new Scenario(@"ステージ0006"),
			() => new Scenario(@"ステージ0007"),
			() => new Scenario(@"ステージ0008"),
			() => new Scenario(@"ステージ0009"),
		};

		public void Perform()
		{
			for (int index = this.StartStageIndex; ; index++)
			{
				// TODO 開発中_抑止
				// TODO 開発中_抑止
				// TODO 開発中_抑止
				//レイヤ表示.Perform(index, this.ThemeColors[index]);

				Game.EndStatus_e endStatus;

				using (new Game())
				{
					Game.I.Map = this.MapLoaders[index]();
					Game.I.Perform();
					endStatus = Game.I.EndStatus;

					if (endStatus == Game.EndStatus_e.ReturnToTitleMenu)
						break;
				}

				DDUtils.Maxim(ref Ground.I.ReachedStageIndex, index + 1);

				try
				{
					if (endStatus == Game.EndStatus_e.NextStage)
					{
						箱から出る.Perform();

						using (new Novel())
						{
							Novel.I.Status.Scenario = ScenarioLoaders[index]();
							Novel.I.Perform();
						}
					}
					else if (endStatus == Game.EndStatus_e.死亡エンド)
					{
						Ending_死亡.Perform();
						break;
					}
					else if (endStatus == Game.EndStatus_e.生還エンド)
					{
						Ending_生還.Perform();
						break;
					}
					else if (endStatus == Game.EndStatus_e.復讐エンド)
					{
						Ending_復讐.Perform();
						break;
					}
					else
					{
						throw null; // never
					}
				}
				catch (箱から出る.Cancelled)
				{ }
				finally
				{
					DDUtils.SetMouseDispMode(false);
				}
			}
		}

		private const int FINAL_STAGE_INDEX = 9;

		public Scenario GetFinalScenario()
		{
			return this.ScenarioLoaders[FINAL_STAGE_INDEX]();
		}

		public Map GetFinalMap()
		{
			return this.MapLoaders[FINAL_STAGE_INDEX]();
		}
	}
}
