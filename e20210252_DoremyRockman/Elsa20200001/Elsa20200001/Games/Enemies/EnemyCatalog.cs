using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Commons;
using Charlotte.GameCommons;
using Charlotte.Games.Enemies.Tests;

namespace Charlotte.Games.Enemies
{
	/// <summary>
	/// 敵のカタログ
	/// </summary>
	public static class EnemyCatalog
	{
		private class EnemyInfo
		{
			public string Name; // 敵の名前、マップ上の配置とか識別に使用する。(開発中、変更してはならない)
			public string DisplayName; // 表示名(開発中、変更しても良い)
			public Func<Enemy> Creator;

			public EnemyInfo(string name, Func<Enemy> creator)
			{
				int colonIndex = name.IndexOf(':');

				if (colonIndex == -1)
				{
					this.Name = name;
					this.DisplayName = name;
				}
				else
				{
					this.Name = name.Substring(0, colonIndex);
					this.DisplayName = name.Substring(colonIndex + 1);
				}
				this.Creator = creator;
			}
		}

		// Creator 用
		// -- 初期値は適当な値
		private static double X = 300.0;
		private static double Y = 300.0;

		private static EnemyInfo[] Tiles = new EnemyInfo[]
		{
			new EnemyInfo(GameConsts.ENEMY_NONE, () => { throw new DDError("\u6575\u300c\u7121\u300d\u3092\u751f\u6210\u3057\u3088\u3046\u3068\u3057\u307e\u3057\u305f\u3002"); }),
			new EnemyInfo("\u30b9\u30bf\u30fc\u30c8\u5730\u70b9", () => new Enemy_\u30b9\u30bf\u30fc\u30c8\u5730\u70b9(X, Y, 5)),
			new EnemyInfo("\u4e0a\u304b\u3089\u5165\u5834", () => new Enemy_\u30b9\u30bf\u30fc\u30c8\u5730\u70b9(X, Y, 8)),
			new EnemyInfo("\u4e0b\u304b\u3089\u5165\u5834", () => new Enemy_\u30b9\u30bf\u30fc\u30c8\u5730\u70b9(X, Y, 2)),
			new EnemyInfo("\u5de6\u304b\u3089\u5165\u5834", () => new Enemy_\u30b9\u30bf\u30fc\u30c8\u5730\u70b9(X, Y, 4)),
			new EnemyInfo("\u53f3\u304b\u3089\u5165\u5834", () => new Enemy_\u30b9\u30bf\u30fc\u30c8\u5730\u70b9(X, Y, 6)),
			new EnemyInfo("\u657501:\u30c6\u30b9\u30c8\u752801", () => new Enemy_B0001(X, Y)),
			new EnemyInfo("\u657502:\u30c6\u30b9\u30c8\u752802", () => new Enemy_B0002(X, Y)),
			new EnemyInfo("\u657503:\u30c6\u30b9\u30c8\u752803", () => new Enemy_B0003(X, Y)),
			new EnemyInfo("\u8d64\u30ce\u30b3\u30ce\u30b3(\u5de6)", () => new Enemy_\u30ce\u30b3\u30ce\u30b3(X, Y, true, true)),
			new EnemyInfo("\u8d64\u30ce\u30b3\u30ce\u30b3(\u53f3)", () => new Enemy_\u30ce\u30b3\u30ce\u30b3(X, Y, true, false)),
			new EnemyInfo("\u9752\u30ce\u30b3\u30ce\u30b3(\u5de6)", () => new Enemy_\u30ce\u30b3\u30ce\u30b3(X, Y, false, true)),
			new EnemyInfo("\u9752\u30ce\u30b3\u30ce\u30b3(\u53f3)", () => new Enemy_\u30ce\u30b3\u30ce\u30b3(X, Y, false, false)),
			new EnemyInfo("Helmet", () => new Enemy_Helmet(X, Y)),
			new EnemyInfo("Chaser", () => new Enemy_Chaser(X, Y)),
			new EnemyInfo("\u5373\u6b7b\u30c8\u30e9\u30c3\u30d7-\u676d", () => new Enemy_\u5373\u6b7b\u30c8\u30e9\u30c3\u30d7The\u91dd(X, Y, Ground.I.Picture.Stage01_Chip_f01)),
			new EnemyInfo("\u5730\u8535(\u80cc\u666f)", () => new Enemy_\u5730\u8535(X, Y)),
			new EnemyInfo("Dog", () => new Enemy_Dog(X, Y)),
			new EnemyInfo("Frog", () => new Enemy_Frog(X, Y)),
			new EnemyInfo("Bird", () => new Enemy_Bird(X, Y)),
			new EnemyInfo("\u5929\u4e95\u91dd", () => new Enemy_\u5929\u4e95\u91dd(X, Y)),
			new EnemyInfo("\u5373\u6b7b\u30c8\u30e9\u30c3\u30d7-\u91dd(\u53f3)", () => new Enemy_\u5373\u6b7b\u30c8\u30e9\u30c3\u30d7The\u91dd(X, Y, Ground.I.Picture.Stage02_Chip_e01)),
			new EnemyInfo("\u5373\u6b7b\u30c8\u30e9\u30c3\u30d7-\u91dd(\u4e0b)", () => new Enemy_\u5373\u6b7b\u30c8\u30e9\u30c3\u30d7The\u91dd(X, Y, Ground.I.Picture.Stage02_Chip_e02)),
			new EnemyInfo("\u5373\u6b7b\u30c8\u30e9\u30c3\u30d7-\u91dd(\u5de6)", () => new Enemy_\u5373\u6b7b\u30c8\u30e9\u30c3\u30d7The\u91dd(X, Y, Ground.I.Picture.Stage02_Chip_e03)),
			new EnemyInfo("\u5373\u6b7b\u30c8\u30e9\u30c3\u30d7-\u91dd(\u4e0a)", () => new Enemy_\u5373\u6b7b\u30c8\u30e9\u30c3\u30d7The\u91dd(X, Y, Ground.I.Picture.Stage02_Chip_e04)),

			// 新しい敵をここへ追加..
		};

		public static string[] GetNames()
		{
			return Tiles.Select(enemy => enemy.Name).ToArray();
		}

		public static string[] GetDisplayNames()
		{
			return Tiles.Select(enemy => enemy.DisplayName).ToArray();
		}

		public static Enemy Create(string name, double x, double y)
		{
			X = x;
			Y = y;

			return SCommon.FirstOrDie(Tiles, enemy => enemy.Name == name, () => new DDError(name)).Creator();
		}
	}
}
