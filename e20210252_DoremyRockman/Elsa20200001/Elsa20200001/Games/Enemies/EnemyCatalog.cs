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
			new EnemyInfo(GameConsts.ENEMY_NONE, () => { throw new DDError("敵「無」を生成しようとしました。"); }),
			new EnemyInfo("スタート地点", () => new Enemy_スタート地点(X, Y, 5)),
			new EnemyInfo("上から入場", () => new Enemy_スタート地点(X, Y, 8)),
			new EnemyInfo("下から入場", () => new Enemy_スタート地点(X, Y, 2)),
			new EnemyInfo("左から入場", () => new Enemy_スタート地点(X, Y, 4)),
			new EnemyInfo("右から入場", () => new Enemy_スタート地点(X, Y, 6)),
			new EnemyInfo("敵01:テスト用01", () => new Enemy_B0001(X, Y)),
			new EnemyInfo("敵02:テスト用02", () => new Enemy_B0002(X, Y)),
			new EnemyInfo("敵03:テスト用03", () => new Enemy_B0003(X, Y)),
			new EnemyInfo("赤ノコノコ(左)", () => new Enemy_ノコノコ(X, Y, true, true)),
			new EnemyInfo("赤ノコノコ(右)", () => new Enemy_ノコノコ(X, Y, true, false)),
			new EnemyInfo("青ノコノコ(左)", () => new Enemy_ノコノコ(X, Y, false, true)),
			new EnemyInfo("青ノコノコ(右)", () => new Enemy_ノコノコ(X, Y, false, false)),
			new EnemyInfo("Helmet", () => new Enemy_Helmet(X, Y)),
			new EnemyInfo("Chaser", () => new Enemy_Chaser(X, Y)),
			new EnemyInfo("即死トラップ-杭", () => new Enemy_即死トラップThe針(X, Y, Ground.I.Picture.Stage01_Chip_f01)),
			new EnemyInfo("地蔵(背景)", () => new Enemy_地蔵(X, Y)),
			new EnemyInfo("Dog", () => new Enemy_Dog(X, Y)),
			new EnemyInfo("Frog", () => new Enemy_Frog(X, Y)),
			new EnemyInfo("Bird", () => new Enemy_Bird(X, Y)),
			new EnemyInfo("天井針", () => new Enemy_天井針(X, Y)),
			new EnemyInfo("即死トラップ-針(右)", () => new Enemy_即死トラップThe針(X, Y, Ground.I.Picture.Stage02_Chip_e01)),
			new EnemyInfo("即死トラップ-針(下)", () => new Enemy_即死トラップThe針(X, Y, Ground.I.Picture.Stage02_Chip_e02)),
			new EnemyInfo("即死トラップ-針(左)", () => new Enemy_即死トラップThe針(X, Y, Ground.I.Picture.Stage02_Chip_e03)),
			new EnemyInfo("即死トラップ-針(上)", () => new Enemy_即死トラップThe針(X, Y, Ground.I.Picture.Stage02_Chip_e04)),

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
