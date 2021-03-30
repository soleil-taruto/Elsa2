using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Games.Shots
{
	public class Shot_Dummy0001 : Shot
	{
		// memo: 飛び道具はこれで実装してね。実装方法は前のバージョンを参照してね。

		public Shot_Dummy0001(double x, double y, bool facingLeft)
			: base(x, y, facingLeft, 1, false, false)
		{ }

		protected override IEnumerable<bool> E_Draw()
		{
			throw null; // dummy
		}
	}
}
