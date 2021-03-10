using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Commons;
using Charlotte.GameCommons;

namespace Charlotte.Games.Shots
{
	public class Shot_さやか突き : Shot
	{
		public Shot_さやか突き(double x, double y, bool facingLeft)
			: base(x, y, facingLeft, 1, true, true)
		{ }

		protected override IEnumerable<bool> E_Draw()
		{
			this.Crash = DDCrashUtils.Circle(new D2Point(this.X, this.Y), 50.0);

			yield return true; // 1フレームで終了
		}
	}
}
