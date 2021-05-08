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
			new TileInfo("ブロック01", () => new Tile_B0001()),
			new TileInfo("ブロック02", () => new Tile_B0002()),
			new TileInfo("ブロック03", () => new Tile_B0003()),
			new TileInfo("ブロック04", () => new Tile_B0004()),

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
