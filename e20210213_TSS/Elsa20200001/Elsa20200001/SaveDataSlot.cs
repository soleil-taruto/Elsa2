using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Commons;
using Charlotte.GameCommons;

namespace Charlotte
{
	public class SaveDataSlot
	{
		/// <summary>
		/// セーブデータ本体
		/// GameStatus.Serialize()の戻り値であること。
		/// null == セーブデータ無し
		/// </summary>
		public string SavedData = null;

		/// <summary>
		/// セーブ日時
		/// .Year == 1 のとき、無効な日付とする。
		/// </summary>
		public SCommon.SimpleTimeStamp SavedTime = new SCommon.SimpleTimeStamp(0L);

		/// <summary>
		/// サムネイル画像
		/// </summary>
		public byte[] Thumbnail = GetDefaultThumbnail();

		public string Serialize()
		{
			return AttachString.I.Untokenize(new string[]
			{
				Common.WrapNullOrString(this.SavedData),
				"" + this.SavedTime.ToTimeStamp(),
				SCommon.Base64.I.Encode(this.Thumbnail),
			});
		}

		public void Deserialize(string gameSavedData)
		{
			string[] lines = AttachString.I.Tokenize(gameSavedData);
			int c = 0;

			this.SavedData = Common.UnwrapNullOrString(lines[c++]);
			this.SavedTime = new SCommon.SimpleTimeStamp(long.Parse(lines[c++]));
			this.Thumbnail = SCommon.Base64.I.Decode(lines[c++]);
		}

		// ---- DefaultThumbnail ----

		private static byte[] _defaultThumbnail = null;

		private static byte[] GetDefaultThumbnail()
		{
			if (_defaultThumbnail == null)
				_defaultThumbnail = DDResource.Load(@"dat\SaveData_DefaultThumbnail.png");

			return _defaultThumbnail;
		}

		// ----
	}
}
