﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Commons;
using Charlotte.GameCommons;
using Charlotte.Games.Tiles;

namespace Charlotte.Games
{
	public class Map
	{
		public string MapFile;

		public Map(string mapFile)
		{
			this.MapFile = mapFile;
			this.Load();
		}

		public MapCell[,] Table; // 添字：[x,y]
		public int W;
		public int H;
		public string WallName;
		public string MusicName;

		public void Load()
		{
			string[] lines = SCommon.TextToLines(SCommon.ENCODING_SJIS.GetString(DDResource.Load(this.MapFile)));
			int c = 0;

			lines = lines.Where(line => line != "" && line[0] != ';').ToArray(); // 空行とコメント行を除去

			int w = int.Parse(lines[c++]);
			int h = int.Parse(lines[c++]);

			if (w < 1 || SCommon.IMAX < w) throw new DDError();
			if (h < 1 || SCommon.IMAX < h) throw new DDError();

			this.Table = new MapCell[w, h];

			for (int x = 0; x < w; x++)
			{
				for (int y = 0; y < h; y++)
				{
					MapCell cell = new MapCell();

					if (c < lines.Length)
					{
						string[] tokens = SCommon.Tokenize(lines[c++], "\t");
						int d = 0;

						d++; // Skip

						cell.TileName = Common.GetElement(tokens, d++, GameConsts.TILE_NONE);
						cell.Tile = TileCatalog.Create(cell.TileName);
						cell.EnemyName = Common.GetElement(tokens, d++, GameConsts.ENEMY_NONE);

						// 新しい項目をここへ追加..

						this.Table[x, y] = cell;
					}
					else
					{
						cell.TileName = GameConsts.TILE_NONE;
						cell.Tile = new Tile_None();
						cell.EnemyName = GameConsts.ENEMY_NONE;
					}
					this.Table[x, y] = cell;
				}
			}
			this.W = w;
			this.H = h;
			this.WallName = Common.GetElement(lines, c++, GameConsts.NAME_DEFAULT);
			this.MusicName = Common.GetElement(lines, c++, GameConsts.NAME_DEFAULT);
		}

		public void Save()
		{
			List<string> lines = new List<string>();
			int w = this.W;
			int h = this.H;

			lines.Add(w.ToString());
			lines.Add(h.ToString());

			for (int x = 0; x < w; x++)
			{
				for (int y = 0; y < h; y++)
				{
					MapCell cell = this.Table[x, y];

					lines.Add(string.Join("\t", cell.TileName == GameConsts.TILE_NONE ? 0 : 1, cell.TileName, cell.EnemyName));
				}
			}
			lines.Add("");
			lines.Add("; WallName");
			lines.Add(this.WallName);
			lines.Add("");
			lines.Add("; MusicName");
			lines.Add(this.MusicName);

			DDResource.Save(this.MapFile, SCommon.ENCODING_SJIS.GetBytes(SCommon.LinesToText(lines.ToArray())));
		}

		public MapCell GetCell(I2Point pt)
		{
			return this.GetCell(pt.X, pt.Y);
		}

		public MapCell GetCell(int x, int y)
		{
			if (
				x < 0 || this.W <= x ||
				y < 0 || this.H <= y
				)
				return GameCommon.DefaultMapCell;

			return this.Table[x, y];
		}
	}
}
