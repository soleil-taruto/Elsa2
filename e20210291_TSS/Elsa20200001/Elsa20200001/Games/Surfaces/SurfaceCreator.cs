using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Commons;
using Charlotte.GameCommons;

namespace Charlotte.Games.Surfaces
{
	public static class SurfaceCreator
	{
		private class Info
		{
			public string TypeName;
			public Func<Surface> CreateSurface;

			public Info(string typeName, Func<Surface> createSurface)
			{
				this.TypeName = typeName;
				this.CreateSurface = createSurface;
			}
		}

		private static string _tn;
		private static string _in;

		private static Info[] Infos = new Info[]
		{
			new Info("MessageWindow", () => new Surface_MessageWindow(_tn, _in)),
			new Info("Select", () => new Surface_Select(_tn, _in)),
			new Info("System", () => new Surface_System(_tn, _in)),
			new Info("SystemButtons", () => new Surface_SystemButtons(_tn, _in)),
			new Info("\u30a8\u30d5\u30a7\u30af\u30c8", () => new Surface_\u30a8\u30d5\u30a7\u30af\u30c8(_tn, _in)),
			new Info("\u30ad\u30e3\u30e9\u30af\u30bf", () => new Surface_\u30ad\u30e3\u30e9\u30af\u30bf(_tn, _in)),
			new Info("\u30b9\u30af\u30ea\u30fc\u30f3", () => new Surface_\u30b9\u30af\u30ea\u30fc\u30f3(_tn, _in)),
			new Info("\u97f3\u697d", () => new Surface_\u97f3\u697d(_tn, _in)),
			new Info("\u52b9\u679c\u97f3", () => new Surface_\u52b9\u679c\u97f3(_tn, _in)),

			// 新しいサーフェスをここへ追加..
		};

		public static Surface Create(string typeName, string instanceName)
		{
			int index = SCommon.IndexOf(Infos, v => v.TypeName == typeName);

			if (index == -1)
				throw new DDError("\u4e0d\u660e\u306a\u30bf\u30a4\u30d7\u540d\uff1a" + typeName);

			_tn = typeName;
			_in = instanceName;

			Surface surface = Infos[index].CreateSurface();

			_tn = null;
			_in = null;

			return surface;
		}
	}
}
