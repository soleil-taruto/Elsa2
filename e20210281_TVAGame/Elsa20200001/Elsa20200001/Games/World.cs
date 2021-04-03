using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Charlotte.Commons;
using Charlotte.GameCommons;

namespace Charlotte.Games
{
	public class World
	{
		// prm の public string StartMapName; 廃止
		// -- new XXX() { F = v } によるフィールドの初期化よりコンストラクタの実行が先であるため

		private string[][] MapNameTableRows; // 添字：[y][x]
		private I2Point CurrPoint;

		public World(string startMapName)
		{
			using (WorkingDir wd = new WorkingDir())
			{
				string file = wd.MakePath();

				File.WriteAllBytes(file, DDResource.Load(@"res\World\World.csv"));

				using (CsvFileReader reader = new CsvFileReader(file))
				{
					this.MapNameTableRows = reader.ReadToEnd();
				}
			}
			this.CurrPoint = this.GetPoint(startMapName);
		}

		private I2Point GetPoint(string mapName)
		{
			for (int y = 0; y < this.MapNameTableRows.Length; y++)
				for (int x = 0; x < this.MapNameTableRows[y].Length; x++)
					if (this.MapNameTableRows[y][x] == mapName)
						return new I2Point(x, y);

			throw new DDError("\u305d\u3093\u306a\u30de\u30c3\u30d7\u3042\u308a\u307e\u305b\u3093\u3002" + mapName);
		}

		public string GetCurrMapName()
		{
			return this.MapNameTableRows[this.CurrPoint.Y][this.CurrPoint.X];
		}

		public void SetCurrMapName(string mapName)
		{
			this.CurrPoint = this.GetPoint(mapName);
		}

		public void Move(int xa, int ya)
		{
			int x = this.CurrPoint.X;
			int y = this.CurrPoint.Y;

			x += xa;
			y += ya;

			if (
				x < 0 ||
				y < 0 ||
				this.MapNameTableRows.Length <= y ||
				this.MapNameTableRows[y].Length <= x
				)
				throw new DDError("\u79fb\u52d5\u5148\u306b\u30de\u30c3\u30d7\u306f\u3042\u308a\u307e\u305b\u3093\u3002");

			this.CurrPoint.X = x;
			this.CurrPoint.Y = y;
		}
	}
}
