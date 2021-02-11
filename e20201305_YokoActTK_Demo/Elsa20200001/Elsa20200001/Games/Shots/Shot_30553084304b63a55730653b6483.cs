using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.GameCommons.Options;
using Charlotte.Commons;

namespace Charlotte.Games.Shots
{
	public class Shot_さやか接地攻撃 : Shot
	{
		public Shot_さやか接地攻撃(double x, double y, bool facingLeft)
			: base(x, y, facingLeft, 10, true, true)
		{ }

		protected override IEnumerable<bool> E_Draw()
		{
			this.Crash = DDCrashUtils.Circle(new D2Point(this.X, this.Y), 80.0);

			yield return true; // 1フレームで終了
		}
	}
}
