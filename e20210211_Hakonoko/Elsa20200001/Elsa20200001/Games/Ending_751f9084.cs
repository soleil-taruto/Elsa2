using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.GameCommons;

namespace Charlotte.Games
{
	public static class Ending_生還
	{
		public static void Perform()
		{
			DDCurtain.SetCurtain();

			foreach (DDScene scene in DDSceneUtils.Create(120))
			{
				// TODO: 3秒後あたりから L でスキップできるようにする。

				DDCurtain.DrawCurtain();

				DDPrint.SetPrint();
				DDPrint.Print("Ending_生還");

				DDEngine.EachFrame();
			}
		}
	}
}
