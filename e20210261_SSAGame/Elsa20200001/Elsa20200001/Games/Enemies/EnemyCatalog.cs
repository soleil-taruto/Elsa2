using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Commons;
using Charlotte.GameCommons;
using Charlotte.Games.Enemies.Tests;
using Charlotte.Games.Enemies.Tests.神奈子s;

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
			new EnemyInfo("\u30ed\u30fc\u30c9\u5730\u70b9", () => new Enemy_\u30b9\u30bf\u30fc\u30c8\u5730\u70b9(X, Y, 101)),
			new EnemyInfo("\u30bb\u30fc\u30d6\u5730\u70b9", () => new Enemy_B\u30bb\u30fc\u30d6\u5730\u70b9(X, Y)),
			new EnemyInfo("\u30a2\u30a4\u30c6\u30e0_0001", () => new Enemy_B\u30a2\u30a4\u30c6\u30e0(X, Y, Enemy_B\u30a2\u30a4\u30c6\u30e0.\u52b9\u7528_e.TEST_0001)),
			new EnemyInfo("\u30a2\u30a4\u30c6\u30e0_0002", () => new Enemy_B\u30a2\u30a4\u30c6\u30e0(X, Y, Enemy_B\u30a2\u30a4\u30c6\u30e0.\u52b9\u7528_e.TEST_0002)),
			new EnemyInfo("\u30a2\u30a4\u30c6\u30e0_0003", () => new Enemy_B\u30a2\u30a4\u30c6\u30e0(X, Y, Enemy_B\u30a2\u30a4\u30c6\u30e0.\u52b9\u7528_e.TEST_0003)),
			new EnemyInfo("\u657501", () => new Enemy_B0001(X, Y)),
			new EnemyInfo("\u657502", () => new Enemy_B0002(X, Y)),
			new EnemyInfo("\u657503", () => new Enemy_B0003(X, Y)),
			new EnemyInfo("\u795e\u5948\u5b50", () => new Enemy_B\u795e\u5948\u5b50(X, Y)),
			new EnemyInfo("\u30a4\u30d9\u30f3\u30c80001", () => new Enemy_B\u30a4\u30d9\u30f3\u30c80001(X, Y)),

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
