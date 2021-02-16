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
		public string SerializedGameStatus = null;

		/// <summary>
		/// セーブ日時
		/// .Year == 1 のとき、無効な日付とする。
		/// </summary>
		public SCommon.SimpleDateTime SavedTime = new SCommon.SimpleDateTime(0L);

		/// <summary>
		/// セーブした地点についての情報
		/// </summary>
		public string AboutSavedPoint = "none";

		public string Serialize()
		{
			return AttachString.I.Untokenize(new string[]
			{
				Common.WrapNullOrString(this.SerializedGameStatus),
				"" + this.SavedTime.ToTimeStamp(),
				this.AboutSavedPoint,
			});
		}

		public void Deserialize(string value)
		{
			string[] lines = AttachString.I.Tokenize(value);
			int c = 0;

			this.SerializedGameStatus = Common.UnwrapNullOrString(lines[c++]);
			this.SavedTime = SCommon.SimpleDateTime.FromTimeStamp(long.Parse(lines[c++]));
			this.AboutSavedPoint = lines[c++];
		}
	}
}
