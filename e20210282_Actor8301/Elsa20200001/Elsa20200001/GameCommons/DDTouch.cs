using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.GameCommons
{
	/// <summary>
	/// 特定の或いは全てのリソースをロードする。
	/// 泥縄式にリソースをロードすることによって処理落ちする場合があるため、予めロードしておく。
	/// </summary>
	public static class DDTouch
	{
		public static void Touch()
		{
			UnloadLocally();
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
