using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Commons;
using Charlotte.GameCommons;

namespace Charlotte.Games.Shots
{
	public class Shot_さやか滞空攻撃 : Shot
	{
		public Shot_さやか滞空攻撃(double x, double y, bool facingLeft)
			: base(x, y, facingLeft, 2, true, true)
		{ }

		protected override IEnumerable<bool> E_Draw()
		{
			this.Crash = DDCrashUtils.Circle(new D2Point(this.X, this.Y), 50.0);

			yield return true; // 1フレームで終了
		}
	}
}
