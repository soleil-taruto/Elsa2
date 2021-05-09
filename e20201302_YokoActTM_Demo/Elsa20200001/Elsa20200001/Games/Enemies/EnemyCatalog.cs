using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.GameCommons;
using Charlotte.Commons;
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
			public string Name;
			public Func<Enemy> Creator;

			public EnemyInfo(string name, Func<Enemy> creator)
			{
				this.Name = name;
				this.Creator = creator;
			}
		}

		// Creator 用
		// -- 初期値は適当な値
		private static double X = 300.0;
		private static double Y = 300.0;

		private static EnemyInfo[] Enemies = new EnemyInfo[]
		{
			new EnemyInfo(GameConsts.ENEMY_NONE, () => { throw new DDError("敵「無」を生成しようとしました。"); }),
			new EnemyInfo("スタート地点", () => new Enemy_スタート地点(X, Y, 5)),
			new EnemyInfo("上から入場", () => new Enemy_スタート地点(X, Y, 8)),
			new EnemyInfo("下から入場", () => new Enemy_スタート地点(X, Y, 2)),
			new EnemyInfo("左から入場", () => new Enemy_スタート地点(X, Y, 4)),
			new EnemyInfo("右から入場", () => new Enemy_スタート地点(X, Y, 6)),
			new EnemyInfo("ロード地点", () => new Enemy_スタート地点(X, Y, 101)),
			new EnemyInfo("セーブ地点", () => new Enemy_Bセーブ地点(X, Y)),
			new EnemyInfo("アイテム・ファイアボール", () => new Enemy_Bアイテム(X, Y, Enemy_Bアイテム.効用_e.WEAPON_FIRE_BALL)),
			new EnemyInfo("アイテム・レーザー", () => new Enemy_Bアイテム(X, Y, Enemy_Bアイテム.効用_e.WEAPON_LASER)),
			new EnemyInfo("アイテム・波動ビーム", () => new Enemy_Bアイテム(X, Y, Enemy_Bアイテム.効用_e.WEAPON_WAVE_BEAM)),
			new EnemyInfo("敵01", () => new Enemy_B0001(X, Y)),
			new EnemyInfo("敵02", () => new Enemy_B0002(X, Y)),
			new EnemyInfo("神奈子", () => new Enemy_B神奈子(X, Y)),
			new EnemyInfo("イベント0001", () => new Enemy_Bイベント0001(X, Y)),
			new EnemyInfo("ハック0001", () => new Enemy_Bハック0001(X, Y)),

			// 新しい敵をここへ追加..
		};

		public static string[] GetNames()
		{
			return Enemies.Select(enemy => enemy.Name).ToArray();
		}

		public static Enemy Create(string name, double x, double y)
		{
			X = x;
			Y = y;

			return SCommon.FirstOrDie(Enemies, enemy => enemy.Name == name, () => new DDError(name)).Creator();
		}
	}
}
