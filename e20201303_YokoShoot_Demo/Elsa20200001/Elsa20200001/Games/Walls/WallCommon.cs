using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.GameCommons;

namespace Charlotte.Games.Walls
{
	public class WallCommon
	{
		public static IEnumerable<double> E_GetA_フェードイン(Wall wall, int frameMax = 150)
		{
			foreach (DDScene scene in DDSceneUtils.Create(frameMax))
				yield return scene.Rate;

			wall.FilledFlag = true;

			for (; ; )
				yield return 1.0;
		}
	}
}
