using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Commons;
using Charlotte.GameCommons;
using Charlotte.GameCommons.Options;

namespace Charlotte.Games.Shots
{
	public class Shot_ほむら滞空攻撃 : Shot
	{
		public Shot_ほむら滞空攻撃(double x, double y, bool facingLeft)
			: base(x, y, facingLeft, 1, false, false)
		{ }

		protected override IEnumerable<bool> E_Draw()
		{
			DDGround.EL.Add(SCommon.Supplier(Effects.小爆発(this.X, this.Y))); // 発射エフェクト

			for (; ; )
			{
				this.X += 24.0 * (this.FacingLeft ? -1 : 1);

				DDDraw.DrawBeginRect(
					Ground.I.Picture.Dummy,
					this.X - 12.0 - DDGround.ICamera.X,
					this.Y - 1.0 - DDGround.ICamera.Y,
					24.0,
					2.0
					);
				DDDraw.DrawEnd();

				this.Crash = DDCrashUtils.Circle(new D2Point(this.X, this.Y), 6.0);

				yield return true;
			}
		}
	}
}
