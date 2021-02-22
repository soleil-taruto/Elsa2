using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

		/// <summary>
		/// プレイヤーのHP
		/// </summary>
		public int StartHP = GameConsts.PLAYER_HP_MAX;

		/// <summary>
		/// スタート地点の Direction 値
		/// 5 == 中央(デフォルト) == ゲームスタート時
		/// 2 == 下から入場
		/// 4 == 左から入場
		/// 6 == 右から入場
		/// 8 == 上から入場
		/// </summary>
		public int StartPointDirection = 5;

		/// <summary>
		/// プレイヤーが左を向いているか
		/// </summary>
		public bool StartFacingLeft = false;

		/// <summary>
		/// スタート時点のプレイヤーの状態
		/// </summary>
		public class StartPlayerStatusInfo
		{
			public double X; // マップ上の位置(X-軸,ドット単位)
			public double Y; // マップ上の位置(Y-軸,ドット単位)
			public bool Ladder; // 梯子を登っているか
			public bool FacingLeft; // 左を向いているか
		}

		/// <summary>
		/// スタート時点のプレイヤーの状態
		/// null == 無効
		/// null ではない場合、ゲーム開始時(Game.Perform 開始時)にこの状態をプレイヤーに反映すること。
		/// </summary>
		public StartPlayerStatusInfo StartPlayerStatus = null;

		/// <summary>
		/// 最後のマップを退場した方向
		/// 5 == 中央(デフォルト) == ゲーム終了時
		/// 2 == 下から退場
		/// 4 == 左から退場
		/// 6 == 右から退場
		/// 8 == 上から退場
		/// </summary>
		public int ExitDirection = 5;

		// <---- prm
	}
}
