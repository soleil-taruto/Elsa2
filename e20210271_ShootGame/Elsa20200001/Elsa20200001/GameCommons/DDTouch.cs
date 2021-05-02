using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.GameCommons
{
	/// <summary>
	/// グローバルな(広域で使用する)リソースをロードする。
	/// -- 泥縄式にリソースをロードすることによって処理落ちする場合があるため、予めロードしておく。
	/// ローカルな(広域で使用しない)リソースを解放する。
	/// -- メモリ不足予防
	/// </summary>
	public static class DDTouch
	{
		/// <summary>
		/// 音楽・SEも解放する。
		/// -- 音楽・SEの再生中ではないこと。
		/// </summary>
		public static void Touch()
		{
			UnloadLocally();
			DDSubScreenUtils.UnloadAll(subScreen => subScreen != DDGround.MainScreen);
			DDFontUtils.UnloadAll();
			TouchGlobally();
		}

		private static void TouchGlobally()
		{
			DDPictureUtils.TouchGlobally();
			DDMusicUtils.TouchGlobally();
			DDSEUtils.TouchGlobally();
		}

		private static void UnloadLocally()
		{
			DDPictureUtils.UnloadLocally();
			DDMusicUtils.UnloadLocally();
			DDSEUtils.UnloadLocally();
		}
	}
}
