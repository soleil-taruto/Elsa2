using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Commons;
using Charlotte.GameCommons;
using Charlotte.Games.Tiles.Tests;

namespace Charlotte.Games.Tiles
{
	/// <summary>
	/// タイルのカタログ
	/// </summary>
	public static class TileCatalog
	{
		private class TileInfo
		{
			public string Name; // 敵の名前、マップ上の配置とか識別に使用する。(開発中、変更してはならない)
			public string DisplayName; // 表示名(開発中、変更しても良い)
			public Func<Tile> Creator;

			public TileInfo(string name, Func<Tile> creator)
			{
				int colonPos = name.IndexOf(':');

				if (colonPos == -1)
				{
					this.Name = name;
					this.DisplayName = name;
				}
				else
				{
					this.Name = name.Substring(0, colonPos);
					this.DisplayName = name.Substring(colonPos + 1);
				}
				this.Creator = creator;
			}
		}

		private static TileInfo[] Tiles = new TileInfo[]
		{
			new TileInfo(GameConsts.TILE_NONE, () => new Tile_None()),
			new TileInfo("\u30d6\u30ed\u30c3\u30af01", () => new Tile_B0001()),
			new TileInfo("\u30d6\u30ed\u30c3\u30af02", () => new Tile_B0002()),
			new TileInfo("\u30d6\u30ed\u30c3\u30af03", () => new Tile_B0003()),
			new TileInfo("\u30d6\u30ed\u30c3\u30af04", () => new Tile_B0004()),
			new TileInfo("\u80cc\u666f01", () => new Tile_Standard(Ground.I.Picture.Space_B0001, Tile.Kind_e.SPACE)),
			new TileInfo("\u68af\u5b50", () => new Tile_Ladder()),

			new TileInfo("Stage01_Chip_a01:\u5730\u9762", () => new Tile_Standard(Ground.I.Picture.Stage01_Chip_a01, Tile.Kind_e.WALL)),
			new TileInfo("Stage01_Chip_a02:\u5730\u4e2d", () => new Tile_Standard(Ground.I.Picture.Stage01_Chip_a02, Tile.Kind_e.WALL)),
			new TileInfo("Stage01_Chip_b01:\u5730\u9762(\u80cc\u666f)", () => new Tile_Standard(Ground.I.Picture.Stage01_Chip_b01, Tile.Kind_e.SPACE)),
			new TileInfo("Stage01_Chip_b02:\u5730\u4e2d(\u80cc\u666f)", () => new Tile_Standard(Ground.I.Picture.Stage01_Chip_b02, Tile.Kind_e.SPACE)),
			new TileInfo("Stage01_Chip_c01:\u6ad301", () => new Tile_Front(Ground.I.Picture.Stage01_Chip_c01, Tile.Kind_e.SPACE)),
			new TileInfo("Stage01_Chip_c02:\u6ad302", () => new Tile_Front(Ground.I.Picture.Stage01_Chip_c02, Tile.Kind_e.SPACE)),
			new TileInfo("Stage01_Chip_c03:\u6ad303", () => new Tile_Front(Ground.I.Picture.Stage01_Chip_c03, Tile.Kind_e.SPACE)),
			new TileInfo("Stage01_Chip_c04:\u6ad304", () => new Tile_Front(Ground.I.Picture.Stage01_Chip_c04, Tile.Kind_e.CLOUD)),
			new TileInfo("Stage01_Chip_c05:\u6ad305", () => new Tile_Front(Ground.I.Picture.Stage01_Chip_c05, Tile.Kind_e.SPACE)),
			new TileInfo("Stage01_Chip_c06:\u6ad306", () => new Tile_Front(Ground.I.Picture.Stage01_Chip_c06, Tile.Kind_e.SPACE)),
			new TileInfo("Stage01_Chip_c07:\u6ad307", () => new Tile_Front(Ground.I.Picture.Stage01_Chip_c07, Tile.Kind_e.CLOUD)),
			new TileInfo("Stage01_Chip_c08:\u6ad308", () => new Tile_Front(Ground.I.Picture.Stage01_Chip_c08, Tile.Kind_e.CLOUD)),
			new TileInfo("Stage01_Chip_c09:\u6ad309", () => new Tile_Front(Ground.I.Picture.Stage01_Chip_c09, Tile.Kind_e.CLOUD)),
			new TileInfo("Stage01_Chip_c10:\u6ad310", () => new Tile_Front(Ground.I.Picture.Stage01_Chip_c10, Tile.Kind_e.CLOUD)),
			new TileInfo("Stage01_Chip_c11:\u6ad311", () => new Tile_Front(Ground.I.Picture.Stage01_Chip_c11, Tile.Kind_e.CLOUD)),
			new TileInfo("Stage01_Chip_c12:\u6ad312", () => new Tile_Front(Ground.I.Picture.Stage01_Chip_c12, Tile.Kind_e.SPACE)),
			new TileInfo("Stage01_Chip_c13:\u6ad313", () => new Tile_Front(Ground.I.Picture.Stage01_Chip_c13, Tile.Kind_e.SPACE)),
			new TileInfo("Stage01_Chip_d01:\u30d6\u30ed\u30c3\u30af\u58c1_\u80cc\u666f(\u660e)", () => new Tile_Standard(Ground.I.Picture.Stage01_Chip_d01, Tile.Kind_e.SPACE)),
			new TileInfo("Stage01_Chip_d02:\u30d6\u30ed\u30c3\u30af\u58c1_\u80cc\u666f(\u30b0)", () => new Tile_Standard(Ground.I.Picture.Stage01_Chip_d02, Tile.Kind_e.SPACE)),
			new TileInfo("Stage01_Chip_d03:\u30d6\u30ed\u30c3\u30af\u58c1_\u80cc\u666f(\u6697)", () => new Tile_Standard(Ground.I.Picture.Stage01_Chip_d03, Tile.Kind_e.SPACE)),
			new TileInfo("Stage01_Chip_e01:\u30d6\u30ed\u30c3\u30af\u58c1(\u660e)", () => new Tile_Standard(Ground.I.Picture.Stage01_Chip_e01, Tile.Kind_e.WALL)),
			new TileInfo("Stage01_Chip_e02:\u30d6\u30ed\u30c3\u30af\u58c1(\u30b0)", () => new Tile_Standard(Ground.I.Picture.Stage01_Chip_e02, Tile.Kind_e.WALL)),
			new TileInfo("Stage01_Chip_e03:\u30d6\u30ed\u30c3\u30af\u58c1(\u6697)", () => new Tile_Standard(Ground.I.Picture.Stage01_Chip_e03, Tile.Kind_e.WALL)),

			//new TileInfo("Stage02_Bg_Chip_a01", () => new Tile_Standard(Ground.I.Picture.Stage02_Bg_Chip_a01, Tile.Kind_e.SPACE)), // レンガ_背景
			//new TileInfo("Stage02_Bg_Chip_a02", () => new Tile_Standard(Ground.I.Picture.Stage02_Bg_Chip_a02, Tile.Kind_e.SPACE)),
			//new TileInfo("Stage02_Bg_Chip_a03", () => new Tile_Standard(Ground.I.Picture.Stage02_Bg_Chip_a03, Tile.Kind_e.SPACE)),
			//new TileInfo("Stage02_Bg_Chip_b01_01", () => new Tile_Standard(Ground.I.Picture.Stage02_Bg_Chip_b01_01, Tile.Kind_e.SPACE)), // 滝_背面
			//new TileInfo("Stage02_Bg_Chip_b01_02", () => new Tile_Standard(Ground.I.Picture.Stage02_Bg_Chip_b01_02, Tile.Kind_e.SPACE)),
			//new TileInfo("Stage02_Bg_Chip_b01_03", () => new Tile_Standard(Ground.I.Picture.Stage02_Bg_Chip_b01_03, Tile.Kind_e.SPACE)),
			//new TileInfo("Stage02_Bg_Chip_b01_04", () => new Tile_Standard(Ground.I.Picture.Stage02_Bg_Chip_b01_04, Tile.Kind_e.SPACE)),
			//new TileInfo("Stage02_Bg_Chip_b02_01", () => new Tile_Standard(Ground.I.Picture.Stage02_Bg_Chip_b02_01, Tile.Kind_e.SPACE)),
			//new TileInfo("Stage02_Bg_Chip_b02_02", () => new Tile_Standard(Ground.I.Picture.Stage02_Bg_Chip_b02_02, Tile.Kind_e.SPACE)),
			//new TileInfo("Stage02_Bg_Chip_b02_03", () => new Tile_Standard(Ground.I.Picture.Stage02_Bg_Chip_b02_03, Tile.Kind_e.SPACE)),
			//new TileInfo("Stage02_Bg_Chip_b02_04", () => new Tile_Standard(Ground.I.Picture.Stage02_Bg_Chip_b02_04, Tile.Kind_e.SPACE)),
			//new TileInfo("Stage02_Bg_Chip_b03_01", () => new Tile_Standard(Ground.I.Picture.Stage02_Bg_Chip_b03_01, Tile.Kind_e.SPACE)),
			//new TileInfo("Stage02_Bg_Chip_b03_02", () => new Tile_Standard(Ground.I.Picture.Stage02_Bg_Chip_b03_02, Tile.Kind_e.SPACE)),
			//new TileInfo("Stage02_Bg_Chip_b03_03", () => new Tile_Standard(Ground.I.Picture.Stage02_Bg_Chip_b03_03, Tile.Kind_e.SPACE)),
			//new TileInfo("Stage02_Bg_Chip_b03_04", () => new Tile_Standard(Ground.I.Picture.Stage02_Bg_Chip_b03_04, Tile.Kind_e.SPACE)),
			//new TileInfo("Stage02_Bg_Chip_b04_01", () => new Tile_Standard(Ground.I.Picture.Stage02_Bg_Chip_b04_01, Tile.Kind_e.SPACE)),
			//new TileInfo("Stage02_Bg_Chip_b04_02", () => new Tile_Standard(Ground.I.Picture.Stage02_Bg_Chip_b04_02, Tile.Kind_e.SPACE)),
			//new TileInfo("Stage02_Bg_Chip_b04_03", () => new Tile_Standard(Ground.I.Picture.Stage02_Bg_Chip_b04_03, Tile.Kind_e.SPACE)),
			//new TileInfo("Stage02_Bg_Chip_b04_04", () => new Tile_Standard(Ground.I.Picture.Stage02_Bg_Chip_b04_04, Tile.Kind_e.SPACE)),
			new TileInfo("Stage02_Bg_Chip_c01:\u80cc\u666f\u30d1\u30a4\u30d7C01", () => new Tile_Standard(Ground.I.Picture.Stage02_Bg_Chip_c01, Tile.Kind_e.SPACE)),
			new TileInfo("Stage02_Bg_Chip_c02:\u80cc\u666f\u30d1\u30a4\u30d7C02", () => new Tile_Standard(Ground.I.Picture.Stage02_Bg_Chip_c02, Tile.Kind_e.SPACE)),
			new TileInfo("Stage02_Bg_Chip_c03:\u80cc\u666f\u30d1\u30a4\u30d7C03", () => new Tile_Standard(Ground.I.Picture.Stage02_Bg_Chip_c03, Tile.Kind_e.SPACE)),
			new TileInfo("Stage02_Bg_Chip_c04:\u80cc\u666f\u30d1\u30a4\u30d7C04", () => new Tile_Standard(Ground.I.Picture.Stage02_Bg_Chip_c04, Tile.Kind_e.SPACE)),
			new TileInfo("Stage02_Bg_Chip_c05:\u80cc\u666f\u30d1\u30a4\u30d7C05", () => new Tile_Standard(Ground.I.Picture.Stage02_Bg_Chip_c05, Tile.Kind_e.SPACE)),
			new TileInfo("Stage02_Bg_Chip_c06:\u80cc\u666f\u30d1\u30a4\u30d7C06", () => new Tile_Standard(Ground.I.Picture.Stage02_Bg_Chip_c06, Tile.Kind_e.SPACE)),
			new TileInfo("Stage02_Bg_Chip_c07:\u80cc\u666f\u30d1\u30a4\u30d7C07", () => new Tile_Standard(Ground.I.Picture.Stage02_Bg_Chip_c07, Tile.Kind_e.SPACE)),
			new TileInfo("Stage02_Bg_Chip_c08:\u80cc\u666f\u30d1\u30a4\u30d7C08", () => new Tile_Standard(Ground.I.Picture.Stage02_Bg_Chip_c08, Tile.Kind_e.SPACE)),
			new TileInfo("Stage02_Bg_Chip_c09:\u80cc\u666f\u30d1\u30a4\u30d7C09", () => new Tile_Standard(Ground.I.Picture.Stage02_Bg_Chip_c09, Tile.Kind_e.SPACE)),
			new TileInfo("Stage02_Bg_Chip_c10:\u80cc\u666f\u30d1\u30a4\u30d7C10", () => new Tile_Standard(Ground.I.Picture.Stage02_Bg_Chip_c10, Tile.Kind_e.SPACE)),
			new TileInfo("Stage02_Bg_Chip_c11:\u80cc\u666f\u30d1\u30a4\u30d7C11", () => new Tile_Standard(Ground.I.Picture.Stage02_Bg_Chip_c11, Tile.Kind_e.SPACE)),
			new TileInfo("Stage02_Bg_Chip_c12:\u80cc\u666f\u30d1\u30a4\u30d7C12", () => new Tile_Standard(Ground.I.Picture.Stage02_Bg_Chip_c12, Tile.Kind_e.SPACE)),
			new TileInfo("Stage02_Bg_Chip_c13:\u80cc\u666f\u30d1\u30a4\u30d7C13", () => new Tile_Standard(Ground.I.Picture.Stage02_Bg_Chip_c13, Tile.Kind_e.SPACE)),
			new TileInfo("Stage02_Bg_Chip_c14:\u80cc\u666f\u30d1\u30a4\u30d7C14", () => new Tile_Standard(Ground.I.Picture.Stage02_Bg_Chip_c14, Tile.Kind_e.SPACE)),
			new TileInfo("Stage02_Chip_a01:\u6a5f\u68b0\u58c101", () => new Tile_Standard(Ground.I.Picture.Stage02_Chip_a01, Tile.Kind_e.WALL)),
			new TileInfo("Stage02_Chip_a02:\u6a5f\u68b0\u58c102", () => new Tile_Standard(Ground.I.Picture.Stage02_Chip_a02, Tile.Kind_e.WALL)),
			new TileInfo("Stage02_Chip_a03:\u6a5f\u68b0\u58c103", () => new Tile_Standard(Ground.I.Picture.Stage02_Chip_a03, Tile.Kind_e.WALL)),
			new TileInfo("Stage02_Chip_a04:\u6a5f\u68b0\u58c104", () => new Tile_Standard(Ground.I.Picture.Stage02_Chip_a04, Tile.Kind_e.WALL)),
			new TileInfo("Stage02_Chip_a05:\u6a5f\u68b0\u58c105", () => new Tile_Standard(Ground.I.Picture.Stage02_Chip_a05, Tile.Kind_e.WALL)),
			new TileInfo("Stage02_Chip_a06:\u6a5f\u68b0\u58c106", () => new Tile_Standard(Ground.I.Picture.Stage02_Chip_a06, Tile.Kind_e.WALL)),
			new TileInfo("Stage02_Chip_a07:\u6a5f\u68b0\u58c107", () => new Tile_Standard(Ground.I.Picture.Stage02_Chip_a07, Tile.Kind_e.WALL)),
			new TileInfo("Stage02_Chip_a08:\u6a5f\u68b0\u58c108", () => new Tile_Standard(Ground.I.Picture.Stage02_Chip_a08, Tile.Kind_e.WALL)),
			new TileInfo("Stage02_Chip_a09:\u6a5f\u68b0\u58c109", () => new Tile_Standard(Ground.I.Picture.Stage02_Chip_a09, Tile.Kind_e.WALL)),
			new TileInfo("Stage02_Chip_a10:\u6a5f\u68b0\u58c110", () => new Tile_Standard(Ground.I.Picture.Stage02_Chip_a10, Tile.Kind_e.WALL)),
			new TileInfo("Stage02_Chip_a11:\u6a5f\u68b0\u58c111", () => new Tile_Standard(Ground.I.Picture.Stage02_Chip_a11, Tile.Kind_e.WALL)),
			new TileInfo("Stage02_Chip_a12:\u6a5f\u68b0\u58c112", () => new Tile_Standard(Ground.I.Picture.Stage02_Chip_a12, Tile.Kind_e.WALL)),
			new TileInfo("Stage02_Chip_a13:\u6a5f\u68b0\u58c113", () => new Tile_Standard(Ground.I.Picture.Stage02_Chip_a13, Tile.Kind_e.WALL)),
			new TileInfo("Stage02_Chip_a14:\u6a5f\u68b0\u58c114", () => new Tile_Standard(Ground.I.Picture.Stage02_Chip_a14, Tile.Kind_e.WALL)),
			new TileInfo("Stage02_Chip_a15:\u6a5f\u68b0\u58c115", () => new Tile_Standard(Ground.I.Picture.Stage02_Chip_a15, Tile.Kind_e.WALL)),
			new TileInfo("Stage02_Chip_a16:\u6a5f\u68b0\u58c116", () => new Tile_Standard(Ground.I.Picture.Stage02_Chip_a16, Tile.Kind_e.WALL)),
			new TileInfo("Stage02_Chip_a17:\u6a5f\u68b0\u58c117", () => new Tile_Standard(Ground.I.Picture.Stage02_Chip_a17, Tile.Kind_e.WALL)),
			new TileInfo("Stage02_Chip_a18:\u6a5f\u68b0\u58c118", () => new Tile_Standard(Ground.I.Picture.Stage02_Chip_a18, Tile.Kind_e.WALL)),
			new TileInfo("Stage02_Chip_a19:\u6a5f\u68b0\u58c119", () => new Tile_Standard(Ground.I.Picture.Stage02_Chip_a19, Tile.Kind_e.WALL)),
			new TileInfo("Stage02_Chip_a20:\u6a5f\u68b0\u58c120", () => new Tile_Standard(Ground.I.Picture.Stage02_Chip_a20, Tile.Kind_e.WALL)),
			new TileInfo("Stage02_Chip_a21:\u6a5f\u68b0\u58c121", () => new Tile_Standard(Ground.I.Picture.Stage02_Chip_a21, Tile.Kind_e.WALL)),
			new TileInfo("Stage02_Chip_a22:\u6a5f\u68b0\u58c122", () => new Tile_Standard(Ground.I.Picture.Stage02_Chip_a22, Tile.Kind_e.WALL)),
			new TileInfo("Stage02_Chip_a23:\u6a5f\u68b0\u58c123", () => new Tile_Standard(Ground.I.Picture.Stage02_Chip_a23, Tile.Kind_e.WALL)),
			new TileInfo("Stage02_Chip_a24:\u6a5f\u68b0\u58c124", () => new Tile_Standard(Ground.I.Picture.Stage02_Chip_a24, Tile.Kind_e.WALL)),
			new TileInfo("Stage02_Chip_a25:\u6a5f\u68b0\u58c125", () => new Tile_Standard(Ground.I.Picture.Stage02_Chip_a25, Tile.Kind_e.WALL)),
			new TileInfo("Stage02_Chip_a26:\u6a5f\u68b0\u58c126", () => new Tile_Standard(Ground.I.Picture.Stage02_Chip_a26, Tile.Kind_e.WALL)),
			new TileInfo("Stage02_Chip_a27:\u6a5f\u68b0\u58c127", () => new Tile_Standard(Ground.I.Picture.Stage02_Chip_a27, Tile.Kind_e.WALL)),
			new TileInfo("Stage02_Chip_a28:\u6a5f\u68b0\u58c128", () => new Tile_Standard(Ground.I.Picture.Stage02_Chip_a28, Tile.Kind_e.WALL)),
			new TileInfo("Stage02_Chip_b01:\u30cd\u30c3\u30c801", () => new Tile_Front(Ground.I.Picture.Stage02_Chip_b01, Tile.Kind_e.SPACE)),
			new TileInfo("Stage02_Chip_b02:\u30cd\u30c3\u30c802", () => new Tile_Front(Ground.I.Picture.Stage02_Chip_b02, Tile.Kind_e.SPACE)),
			new TileInfo("Stage02_Chip_b03:\u30cd\u30c3\u30c803", () => new Tile_Front(Ground.I.Picture.Stage02_Chip_b03, Tile.Kind_e.CLOUD)),
			new TileInfo("Stage02_Chip_b04:\u30cd\u30c3\u30c804", () => new Tile_Front(Ground.I.Picture.Stage02_Chip_b04, Tile.Kind_e.CLOUD)),
			new TileInfo("Stage02_Chip_b05:\u30cd\u30c3\u30c805", () => new Tile_Front(Ground.I.Picture.Stage02_Chip_b05, Tile.Kind_e.CLOUD)),
			new TileInfo("Stage02_Chip_b06:\u30cd\u30c3\u30c806", () => new Tile_Front(Ground.I.Picture.Stage02_Chip_b06, Tile.Kind_e.SPACE)),
			new TileInfo("Stage02_Chip_b07:\u30cd\u30c3\u30c807", () => new Tile_Front(Ground.I.Picture.Stage02_Chip_b07, Tile.Kind_e.SPACE)),
			new TileInfo("Stage02_Chip_b08:\u30cd\u30c3\u30c808", () => new Tile_Front(Ground.I.Picture.Stage02_Chip_b08, Tile.Kind_e.SPACE)),
			new TileInfo("Stage02_Chip_b09:\u30cd\u30c3\u30c809", () => new Tile_Front(Ground.I.Picture.Stage02_Chip_b09, Tile.Kind_e.SPACE)),
			//new TileInfo("Stage02_Chip_c01", () => new Tile_Standard(Ground.I.Picture.Stage02_Chip_c01, Tile.Kind_e.WALL)), // レンガ
			//new TileInfo("Stage02_Chip_c02", () => new Tile_Standard(Ground.I.Picture.Stage02_Chip_c02, Tile.Kind_e.WALL)),
			//new TileInfo("Stage02_Chip_c03", () => new Tile_Standard(Ground.I.Picture.Stage02_Chip_c03, Tile.Kind_e.WALL)),
			//new TileInfo("Stage02_Chip_d01", () => new Tile_Standard(Ground.I.Picture.Stage02_Chip_d01, Tile.Kind_e.LADDER)), // 梯子 -- 既に有る。
			//new TileInfo("Stage02_Chip_e01", () => new Tile_Standard(Ground.I.Picture.Stage02_Chip_e01, Tile.Kind_e.WALL)), // 針
			//new TileInfo("Stage02_Chip_e02", () => new Tile_Standard(Ground.I.Picture.Stage02_Chip_e02, Tile.Kind_e.WALL)),
			//new TileInfo("Stage02_Chip_e03", () => new Tile_Standard(Ground.I.Picture.Stage02_Chip_e03, Tile.Kind_e.WALL)),
			//new TileInfo("Stage02_Chip_e04", () => new Tile_Standard(Ground.I.Picture.Stage02_Chip_e04, Tile.Kind_e.WALL)),
			//new TileInfo("Stage02_Chip_f01_01", () => new Tile_Standard(Ground.I.Picture.Stage02_Chip_f01_01, Tile.Kind_e.WALL)), // 滝_前面
			//new TileInfo("Stage02_Chip_f01_02", () => new Tile_Standard(Ground.I.Picture.Stage02_Chip_f01_02, Tile.Kind_e.WALL)),
			//new TileInfo("Stage02_Chip_f01_03", () => new Tile_Standard(Ground.I.Picture.Stage02_Chip_f01_03, Tile.Kind_e.WALL)),
			//new TileInfo("Stage02_Chip_f01_04", () => new Tile_Standard(Ground.I.Picture.Stage02_Chip_f01_04, Tile.Kind_e.WALL)),
			//new TileInfo("Stage02_Chip_f02_01", () => new Tile_Standard(Ground.I.Picture.Stage02_Chip_f02_01, Tile.Kind_e.WALL)),
			//new TileInfo("Stage02_Chip_f02_02", () => new Tile_Standard(Ground.I.Picture.Stage02_Chip_f02_02, Tile.Kind_e.WALL)),
			//new TileInfo("Stage02_Chip_f02_03", () => new Tile_Standard(Ground.I.Picture.Stage02_Chip_f02_03, Tile.Kind_e.WALL)),
			//new TileInfo("Stage02_Chip_f02_04", () => new Tile_Standard(Ground.I.Picture.Stage02_Chip_f02_04, Tile.Kind_e.WALL)),
			//new TileInfo("Stage02_Chip_f03_01", () => new Tile_Standard(Ground.I.Picture.Stage02_Chip_f03_01, Tile.Kind_e.WALL)),
			//new TileInfo("Stage02_Chip_f03_02", () => new Tile_Standard(Ground.I.Picture.Stage02_Chip_f03_02, Tile.Kind_e.WALL)),
			//new TileInfo("Stage02_Chip_f03_03", () => new Tile_Standard(Ground.I.Picture.Stage02_Chip_f03_03, Tile.Kind_e.WALL)),
			//new TileInfo("Stage02_Chip_f03_04", () => new Tile_Standard(Ground.I.Picture.Stage02_Chip_f03_04, Tile.Kind_e.WALL)),
			//new TileInfo("Stage02_Chip_f04_01", () => new Tile_Standard(Ground.I.Picture.Stage02_Chip_f04_01, Tile.Kind_e.WALL)),
			//new TileInfo("Stage02_Chip_f04_02", () => new Tile_Standard(Ground.I.Picture.Stage02_Chip_f04_02, Tile.Kind_e.WALL)),
			//new TileInfo("Stage02_Chip_f04_03", () => new Tile_Standard(Ground.I.Picture.Stage02_Chip_f04_03, Tile.Kind_e.WALL)),
			//new TileInfo("Stage02_Chip_f04_04", () => new Tile_Standard(Ground.I.Picture.Stage02_Chip_f04_04, Tile.Kind_e.WALL)),
			//new TileInfo("Stage02_Chip_g04_01", () => new Tile_Standard(Ground.I.Picture.Stage02_Chip_g04_01, Tile.Kind_e.WALL)), // 針アニメ
			//new TileInfo("Stage02_Chip_g04_02", () => new Tile_Standard(Ground.I.Picture.Stage02_Chip_g04_02, Tile.Kind_e.WALL)),
			//new TileInfo("Stage02_Chip_g04_03", () => new Tile_Standard(Ground.I.Picture.Stage02_Chip_g04_03, Tile.Kind_e.WALL)),
			//new TileInfo("Stage02_Chip_g04_04", () => new Tile_Standard(Ground.I.Picture.Stage02_Chip_g04_04, Tile.Kind_e.WALL)),
			new TileInfo("Stage02_Chip_h01:\u524d\u9762\u30d1\u30a4\u30d7H01", () => new Tile_Front(Ground.I.Picture.Stage02_Chip_h01, Tile.Kind_e.SPACE)),
			new TileInfo("Stage02_Chip_h02:\u524d\u9762\u30d1\u30a4\u30d7H02", () => new Tile_Front(Ground.I.Picture.Stage02_Chip_h02, Tile.Kind_e.SPACE)),
			new TileInfo("Stage02_Chip_h03:\u524d\u9762\u30d1\u30a4\u30d7H03", () => new Tile_Front(Ground.I.Picture.Stage02_Chip_h03, Tile.Kind_e.SPACE)),
			new TileInfo("Stage02_Chip_h04:\u524d\u9762\u30d1\u30a4\u30d7H04", () => new Tile_Front(Ground.I.Picture.Stage02_Chip_h04, Tile.Kind_e.SPACE)),
			new TileInfo("Stage02_Chip_h05:\u524d\u9762\u30d1\u30a4\u30d7H05", () => new Tile_Front(Ground.I.Picture.Stage02_Chip_h05, Tile.Kind_e.SPACE)),
			new TileInfo("Stage02_Chip_h06:\u524d\u9762\u30d1\u30a4\u30d7H06", () => new Tile_Front(Ground.I.Picture.Stage02_Chip_h06, Tile.Kind_e.SPACE)),
			new TileInfo("Stage02_Chip_h07:\u524d\u9762\u30d1\u30a4\u30d7H07", () => new Tile_Front(Ground.I.Picture.Stage02_Chip_h07, Tile.Kind_e.SPACE)),
			new TileInfo("Stage02_Chip_h08:\u524d\u9762\u30d1\u30a4\u30d7H08", () => new Tile_Front(Ground.I.Picture.Stage02_Chip_h08, Tile.Kind_e.SPACE)),
			new TileInfo("Stage02_Chip_h09:\u524d\u9762\u30d1\u30a4\u30d7H09", () => new Tile_Front(Ground.I.Picture.Stage02_Chip_h09, Tile.Kind_e.SPACE)),
			new TileInfo("Stage02_Chip_h10:\u524d\u9762\u30d1\u30a4\u30d7H10", () => new Tile_Front(Ground.I.Picture.Stage02_Chip_h10, Tile.Kind_e.SPACE)),
			new TileInfo("Stage02_Chip_h11:\u524d\u9762\u30d1\u30a4\u30d7H11", () => new Tile_Front(Ground.I.Picture.Stage02_Chip_h11, Tile.Kind_e.SPACE)),
			new TileInfo("Stage02_Chip_h12:\u524d\u9762\u30d1\u30a4\u30d7H12", () => new Tile_Front(Ground.I.Picture.Stage02_Chip_h12, Tile.Kind_e.SPACE)),
			new TileInfo("Stage02_Chip_h13:\u524d\u9762\u30d1\u30a4\u30d7H13", () => new Tile_Front(Ground.I.Picture.Stage02_Chip_h13, Tile.Kind_e.SPACE)),
			new TileInfo("Stage02_Chip_h14:\u524d\u9762\u30d1\u30a4\u30d7H14", () => new Tile_Front(Ground.I.Picture.Stage02_Chip_h14, Tile.Kind_e.SPACE)),
			new TileInfo("Stage02_Chip_h01_Wall:\u58c1\u30d1\u30a4\u30d7H01", () => new Tile_Double(Ground.I.Picture.Stage01_Chip_e03, Ground.I.Picture.Stage02_Chip_h01, Tile.Kind_e.WALL)),
			new TileInfo("Stage02_Chip_h02_Wall:\u58c1\u30d1\u30a4\u30d7H02", () => new Tile_Double(Ground.I.Picture.Stage01_Chip_e03, Ground.I.Picture.Stage02_Chip_h02, Tile.Kind_e.WALL)),
			new TileInfo("Stage02_Chip_h03_Wall:\u58c1\u30d1\u30a4\u30d7H03", () => new Tile_Double(Ground.I.Picture.Stage01_Chip_e03, Ground.I.Picture.Stage02_Chip_h03, Tile.Kind_e.WALL)),
			new TileInfo("Stage02_Chip_h04_Wall:\u58c1\u30d1\u30a4\u30d7H04", () => new Tile_Double(Ground.I.Picture.Stage01_Chip_e03, Ground.I.Picture.Stage02_Chip_h04, Tile.Kind_e.WALL)),
			new TileInfo("Stage02_Chip_h05_Wall:\u58c1\u30d1\u30a4\u30d7H05", () => new Tile_Double(Ground.I.Picture.Stage01_Chip_e03, Ground.I.Picture.Stage02_Chip_h05, Tile.Kind_e.WALL)),
			new TileInfo("Stage02_Chip_h06_Wall:\u58c1\u30d1\u30a4\u30d7H06", () => new Tile_Double(Ground.I.Picture.Stage01_Chip_e03, Ground.I.Picture.Stage02_Chip_h06, Tile.Kind_e.WALL)),
			new TileInfo("Stage02_Chip_h07_Wall:\u58c1\u30d1\u30a4\u30d7H07", () => new Tile_Double(Ground.I.Picture.Stage01_Chip_e03, Ground.I.Picture.Stage02_Chip_h07, Tile.Kind_e.WALL)),
			new TileInfo("Stage02_Chip_h08_Wall:\u58c1\u30d1\u30a4\u30d7H08", () => new Tile_Double(Ground.I.Picture.Stage01_Chip_e03, Ground.I.Picture.Stage02_Chip_h08, Tile.Kind_e.WALL)),
			new TileInfo("Stage02_Chip_h09_Wall:\u58c1\u30d1\u30a4\u30d7H09", () => new Tile_Double(Ground.I.Picture.Stage01_Chip_e03, Ground.I.Picture.Stage02_Chip_h09, Tile.Kind_e.WALL)),
			new TileInfo("Stage02_Chip_h10_Wall:\u58c1\u30d1\u30a4\u30d7H10", () => new Tile_Double(Ground.I.Picture.Stage01_Chip_e03, Ground.I.Picture.Stage02_Chip_h10, Tile.Kind_e.WALL)),
			new TileInfo("Stage02_Chip_h11_Wall:\u58c1\u30d1\u30a4\u30d7H11", () => new Tile_Double(Ground.I.Picture.Stage01_Chip_e03, Ground.I.Picture.Stage02_Chip_h11, Tile.Kind_e.WALL)),
			new TileInfo("Stage02_Chip_h12_Wall:\u58c1\u30d1\u30a4\u30d7H12", () => new Tile_Double(Ground.I.Picture.Stage01_Chip_e03, Ground.I.Picture.Stage02_Chip_h12, Tile.Kind_e.WALL)),
			new TileInfo("Stage02_Chip_h13_Wall:\u58c1\u30d1\u30a4\u30d7H13", () => new Tile_Double(Ground.I.Picture.Stage01_Chip_e03, Ground.I.Picture.Stage02_Chip_h13, Tile.Kind_e.WALL)),
			new TileInfo("Stage02_Chip_h14_Wall:\u58c1\u30d1\u30a4\u30d7H14", () => new Tile_Double(Ground.I.Picture.Stage01_Chip_e03, Ground.I.Picture.Stage02_Chip_h14, Tile.Kind_e.WALL)),
			
			new TileInfo("\u30ec\u30f3\u30ac_\u80cc\u666f", () => new Tile_Brick_Back()),
			new TileInfo("\u30ec\u30f3\u30ac_\u58c1", () => new Tile_Brick_Front()),
			
			new TileInfo("\u6edd_\u80cc\u9762_01", () => new Tile_Waterfall_Back(Ground.I.Picture.Stage02_Bg_Chip_b0x[0])),
			new TileInfo("\u6edd_\u80cc\u9762_02", () => new Tile_Waterfall_Back(Ground.I.Picture.Stage02_Bg_Chip_b0x[1])),
			new TileInfo("\u6edd_\u80cc\u9762_03", () => new Tile_Waterfall_Back(Ground.I.Picture.Stage02_Bg_Chip_b0x[2])),
			new TileInfo("\u6edd_\u80cc\u9762_04", () => new Tile_Waterfall_Back(Ground.I.Picture.Stage02_Bg_Chip_b0x[3])),

			new TileInfo("\u6edd_\u524d\u9762_01", () => new Tile_Waterfall_Front(Ground.I.Picture.Stage02_Chip_f0x[0])),
			new TileInfo("\u6edd_\u524d\u9762_02", () => new Tile_Waterfall_Front(Ground.I.Picture.Stage02_Chip_f0x[1])),
			new TileInfo("\u6edd_\u524d\u9762_03", () => new Tile_Waterfall_Front(Ground.I.Picture.Stage02_Chip_f0x[2])),
			new TileInfo("\u6edd_\u524d\u9762_04", () => new Tile_Waterfall_Front(Ground.I.Picture.Stage02_Chip_f0x[3])),

			new TileInfo("\u6edd_\u524d\u9762-\u58c1_01", () => new Tile_Waterfall_Ground(Ground.I.Picture.Stage02_Chip_f0x[0], Ground.I.Picture.Stage01_Chip_e03)),
			new TileInfo("\u6edd_\u524d\u9762-\u58c1_02", () => new Tile_Waterfall_Ground(Ground.I.Picture.Stage02_Chip_f0x[1], Ground.I.Picture.Stage01_Chip_e03)),
			new TileInfo("\u6edd_\u524d\u9762-\u58c1_03", () => new Tile_Waterfall_Ground(Ground.I.Picture.Stage02_Chip_f0x[2], Ground.I.Picture.Stage01_Chip_e03)),
			new TileInfo("\u6edd_\u524d\u9762-\u58c1_04", () => new Tile_Waterfall_Ground(Ground.I.Picture.Stage02_Chip_f0x[3], Ground.I.Picture.Stage01_Chip_e03)),

			// 新しいタイルをここへ追加..
		};

		public static string[] GetNames()
		{
			return Tiles.Select(tile => tile.Name).ToArray();
		}

		private static string[] _displayNames = null;

		public static string[] GetDisplayNames()
		{
			// 生成が重いのでキャッシュする。
			//
			if (_displayNames == null)
				_displayNames = Tiles.Select(tile => tile.DisplayName + " (" + Tile.Kind_e_Names[(int)tile.Creator().GetKind()][0] + ")").ToArray();

			return _displayNames;
		}

		public static Tile Create(string name)
		{
			return SCommon.FirstOrDie(Tiles, tile => tile.Name == name, () => new DDError(name)).Creator();
		}
	}
}
