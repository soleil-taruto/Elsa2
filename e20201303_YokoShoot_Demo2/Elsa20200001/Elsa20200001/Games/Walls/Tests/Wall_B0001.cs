using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Commons;
using Charlotte.GameCommons;

namespace Charlotte.Games.Walls.Tests
{
	public class Wall_B0001 : Wall
	{
		public override IEnumerable<bool> E_Draw()
		{
			Func<double> getA = SCommon.Supplier(WallCommon.E_GetA_フェードイン(this));

			for (int slide = 0; ; slide += 19, slide %= 180)
			{
				DDDraw.SetAlpha(getA());

				for (int dx = -slide; dx < DDConsts.Screen_W; dx += 180)
				{
					for (int dy = 0; dy < DDConsts.Screen_H; dy += 180)
					{
						DDDraw.DrawSimple(Ground.I.Picture.Wall0001, dx, dy);
					}
				}
				DDDraw.Reset();

				yield return true;
			}
		}
	}
}
