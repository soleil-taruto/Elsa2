using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Games;

namespace Charlotte.Tests.Games
{
	public class WorldGameMasterTest
	{
		public void Test01()
		{
			using (new WorldGameMaster())
			{
				WorldGameMaster.I.Perform();
			}
		}

		public void Test02()
		{
			using (new WorldGameMaster())
			{
				WorldGameMaster.I.World = new World("t0001");
				WorldGameMaster.I.Status = new GameStatus();
				WorldGameMaster.I.Perform();
			}
		}

		public void Test03()
		{
			string startMapName;

			// ---- choose one ----

			startMapName = "t0001";
			//startMapName = "t0002";
			//startMapName = "t0003";
			//startMapName = "t0004";
			//startMapName = "t0005";

			// ----

			using (new WorldGameMaster())
			{
				WorldGameMaster.I.World = new World(startMapName);
				WorldGameMaster.I.Status = new GameStatus();
				WorldGameMaster.I.Perform();
			}
		}
	}
}
