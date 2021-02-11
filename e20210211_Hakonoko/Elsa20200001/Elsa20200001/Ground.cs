using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Games;

namespace Charlotte
{
	public class Ground
	{
		public static Ground I;

		public ResourceMusic Music = new ResourceMusic();
		public ResourcePicture Picture = new ResourcePicture();
		public ResourcePicture2 Picture2;
		public ResourceSE SE = new ResourceSE();

		// DDSaveData.Save/Load でセーブ・ロードする情報はここに保持する。

		/// <summary>
		/// 0 == クリアしたステージ無し
		/// 1 == ステージ 1 クリア
		/// 2 == ステージ 2 クリア
		/// 3 == ステージ 3 クリア
		/// ...
		/// </summary>
		public int ReachedStageIndex;

		/// <summary>
		/// 残りリスポーン地点設置回数_初期値
		/// </summary>
		public int StartSnapshotCount = Consts.START_SNAPSHOT_COUNT_DEF;
	}
}
