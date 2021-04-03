using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.GameCommons;

namespace Charlotte.Games.Walls
{
	public static class WallCreator
	{
		public static Wall Create(string name)
		{
			if (name == GameConsts.NAME_DEFAULT)
				return new Wall_Dark();

			Wall wall;

			switch (name)
			{
				//case Consts.WALL_DEFAULT: wall = new Wall_Dark(); break; // 難読化のため、ここに書けない。
				//case "B0001": wall = new Wall_Simple(Ground.I.Picture.Wall_B0001); break;
				//case "B0002": wall = new Wall_Simple(Ground.I.Picture.Wall_B0002); break;
				//case "B0003": wall = new Wall_Simple(Ground.I.Picture.Wall_B0003); break;
				case "Dark": wall = new Wall_Dark(); break;
				case "None": wall = new Wall_None(); break;
				case "\u6771\u65b9\u98a8": wall = new Wall_\u6771\u65b9\u98a8(); break;
				case "\u30ed\u30c3\u30af\u30de\u30f3\u98a8": wall = new Wall_\u30ed\u30c3\u30af\u30de\u30f3\u98a8(); break;

				// 新しい壁紙をここへ追加..

				default:
					throw new DDError("name: " + name);
			}
			return wall;
		}
	}
}
