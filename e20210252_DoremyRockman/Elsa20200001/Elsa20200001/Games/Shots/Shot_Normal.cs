using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Commons;
using Charlotte.GameCommons;

namespace Charlotte.Games.Shots
{
	public class Shot_Normal : Shot
	{
		public Shot_Normal(double x, double y, bool facingLeft)
			: base(x, y, facingLeft, 1, true, false)
		{ }

		protected override IEnumerable<bool> E_Draw()
		{
			while (!DDUtils.IsOutOfCamera(new D2Point(this.X, this.Y)))
			{
				this.X += 12.0 * (this.FacingLeft ? -1 : 1);

				DDDraw.DrawBegin(Ground.I.Picture.Shot_Normal, this.X - DDGround.ICamera.X, this.Y - DDGround.ICamera.Y);
				DDDraw.DrawEnd();

				this.Crash = DDCrashUtils.Circle(new D2Point(this.X, this.Y), 10.0);

				yield return true;
			}
		}
	}
}
