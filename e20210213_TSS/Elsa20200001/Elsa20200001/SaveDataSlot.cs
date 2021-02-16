﻿using System;
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
		public string SerializedGameStatus = null;

		/// <summary>
		/// セーブ日時
		/// .Year == 1 のとき、無効な日付とする。
		/// </summary>
		public SCommon.SimpleDateTime SavedTime = new SCommon.SimpleDateTime(0L);

		/// <summary>
		/// サムネイル画像
		/// </summary>
		public byte[] Thumbnail = GetDefaultThumbnail();

		public string Serialize()
		{
			return AttachString.I.Untokenize(new string[]
			{
				Common.WrapNullOrString(this.SerializedGameStatus),
				"" + this.SavedTime.ToTimeStamp(),
				SCommon.Base64.I.Encode(this.Thumbnail),
			});
		}

		public void Deserialize(string value)
		{
			string[] lines = AttachString.I.Tokenize(value);
			int c = 0;

			this.SerializedGameStatus = Common.UnwrapNullOrString(lines[c++]);
			this.SavedTime = SCommon.SimpleDateTime.FromTimeStamp(long.Parse(lines[c++]));
			this.Thumbnail = SCommon.Base64.I.Decode(lines[c++]);
		}

		private static byte[] _defaultThumbnail = null;

		private static byte[] GetDefaultThumbnail()
		{
			if (_defaultThumbnail == null)
				_defaultThumbnail = DDResource.Load(@"dat\SaveData_DefaultThumbnail.png");

			return _defaultThumbnail;
		}
	}
}
