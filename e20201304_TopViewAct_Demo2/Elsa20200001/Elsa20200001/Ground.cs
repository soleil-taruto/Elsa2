using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Commons;
using Charlotte.Games;
using Charlotte.Novels;

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

		public bool FastReverseMode; // 高速ボタンを「離している」ときに高速になる。

		public int NovelMessageSpeed = NovelConsts.MESSAGE_SPEED_DEF;

		public class GameSaveDataInfo
		{
			public string TimeStamp;
			public string MapName;
			public GameStatus GameStatus;

			#region Serialize / Deserialize

			public string Serialize()
			{
				List<string> dest = new List<string>();

				// ★★★ シリアライズ_ここから ★★★

				dest.Add(this.TimeStamp);
				dest.Add(this.MapName);
				dest.Add(this.GameStatus.Serialize());

				// ★★★ シリアライズ_ここまで ★★★

				return AttachString.I.Untokenize(dest);
			}

			private void S_Deserialize(string value)
			{
				string[] lines = AttachString.I.Tokenize(value);
				int c = 0;

				// ★★★ デシリアライズ_ここから ★★★

				this.TimeStamp = lines[c++];
				this.MapName = lines[c++];
				this.GameStatus = GameStatus.Deserialize(lines[c++]);

				// ★★★ デシリアライズ_ここまで ★★★
			}

			public static GameSaveDataInfo Deserialize(string value)
			{
				GameSaveDataInfo gameSaveData = new GameSaveDataInfo();
				gameSaveData.S_Deserialize(value);
				return gameSaveData;
			}

			#endregion
		}

		public GameSaveDataInfo[] GameSaveDataSlots = new GameSaveDataInfo[Consts.GAME_SAVE_DATA_SLOT_NUM]; // null 要素 == セーブデータ無し
	}
}
