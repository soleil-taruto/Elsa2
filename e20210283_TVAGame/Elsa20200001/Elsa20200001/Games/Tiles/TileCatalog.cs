using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Commons;
using Charlotte.GameCommons;

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
			new TileInfo("芝", () => new Tile_Space(Ground.I.Picture2.Tile_A2[0, 0])),
			new TileInfo("水", () => new Tile_River(Ground.I.Picture2.Tile_A1[0, 0])),
			new TileInfo("箱", () => new Tile_Wall(Ground.I.Picture2.Tile_B[8, 2])),
			new TileInfo("水辺", () => new Tile_水辺(Ground.I.Picture2.MiniTile_A1, 16, 0, 3, 60)),
			new TileInfo("森林", () => new Tile_森林(Ground.I.Picture2.Tile_B, 6, 6, 6, 4, Ground.I.Picture2.Tile_A2[0, 0], Ground.I.Picture2.Tile_B[4, 6])),

			// 新しいタイルをここへ追加..
		};

		public static string[] GetNames()
		{
			return Tiles.Select(tile => tile.Name).ToArray();
		}

		public static string[] GetDisplayNames()
		{
			return Tiles.Select(tile => tile.DisplayName).ToArray();
		}

		public static Tile Create(string name)
		{
			return SCommon.FirstOrDie(Tiles, tile => tile.Name == name, () => new DDError(name)).Creator();
		}
	}
}
