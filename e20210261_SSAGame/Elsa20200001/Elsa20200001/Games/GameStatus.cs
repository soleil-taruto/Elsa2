﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Commons;

namespace Charlotte.Games
{
	/// <summary>
	/// ゲームの状態を保持する。
	/// プレイヤーのレベルとか保有アイテムといった概念が入ってくることを想定して、独立したクラスとする。
	/// </summary>
	public class GameStatus
	{
		// (テスト時など)特にフィールドを設定せずにインスタンスを生成する使い方を想定して、
		// 全てのパラメータはデフォルト値で初期化すること。

		public Player.Chara_e StartChara = Player.Chara_e.TEWI;

		/// <summary>
		/// プレイヤーの最大HP
		/// </summary>
		public int MaxHP = 10;

		/// <summary>
		/// プレイヤーのHP
		/// </summary>
		public int StartHP = 10;

		/// <summary>
		/// スタート地点の Direction 値
		/// 5 == 中央(デフォルト) == ゲームスタート時
		/// 2 == 下から入場
		/// 4 == 左から入場
		/// 6 == 右から入場
		/// 8 == 上から入場
		/// 101 == ロード地点
		/// </summary>
		public int StartPointDirection = 5;

		/// <summary>
		/// プレイヤーが左を向いているか
		/// </summary>
		public bool StartFacingLeft = false;

		/// <summary>
		/// 最後のマップを退場した方向
		/// 5 == 中央(デフォルト) == ゲーム終了時
		/// 2 == 下から退場
		/// 4 == 左から退場
		/// 6 == 右から退場
		/// 8 == 上から退場
		/// </summary>
		public int ExitDirection = 5;

		// ---- game_進行・インベントリ ----

		public bool 神奈子を倒した = false;

		// ----

		// <---- prm

		#region Serialize / Deserialize

		public string Serialize()
		{
			List<string> dest = new List<string>();

			// ★★★ シリアライズ_ここから ★★★

			dest.Add("" + (int)this.StartChara);
			dest.Add("" + this.MaxHP);
			dest.Add("" + this.StartHP);
			dest.Add("" + this.StartPointDirection);
			dest.Add("" + (this.StartFacingLeft ? 1 : 0));
			dest.Add("" + this.ExitDirection);
			dest.Add("" + (this.神奈子を倒した ? 1 : 0));

			// ★★★ シリアライズ_ここまで ★★★

			return SCommon.Serializer.I.Join(dest.ToArray());
		}

		private void S_Deserialize(string value)
		{
			string[] lines = SCommon.Serializer.I.Split(value);
			int c = 0;

			// ★★★ デシリアライズ_ここから ★★★

			this.StartChara = (Player.Chara_e)int.Parse(lines[c++]);
			this.MaxHP = int.Parse(lines[c++]);
			this.StartHP = int.Parse(lines[c++]);
			this.StartPointDirection = int.Parse(lines[c++]);
			this.StartFacingLeft = int.Parse(lines[c++]) != 0;
			this.ExitDirection = int.Parse(lines[c++]);
			this.神奈子を倒した = int.Parse(lines[c++]) != 0;

			// ★★★ デシリアライズ_ここまで ★★★
		}

		public static GameStatus Deserialize(string value)
		{
			GameStatus gameStatus = new GameStatus();
			gameStatus.S_Deserialize(value);
			return gameStatus;
		}

		#endregion

		public GameStatus GetClone()
		{
			return Deserialize(this.Serialize());
		}
	}
}
