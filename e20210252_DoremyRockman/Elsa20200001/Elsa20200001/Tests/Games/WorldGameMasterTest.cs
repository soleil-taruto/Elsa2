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
				WorldGameMaster.I.World = new World("w0001", "t0001");
				WorldGameMaster.I.Status = new GameStatus();
				WorldGameMaster.I.Perform();
			}
		}

		public void Test03()
		{
			string sNames;

			// ---- choose one ----

			//sNames = "w0001:t0001";
			//sNames = "w0001:t0002";
			//sNames = "w0001:t0003";

			//sNames = "w1001:t1001";
			//sNames = "w1001:t0002";
			//sNames = "w1001:t0003";

			//sNames = "Stage_Raimu_v001:Start";
			//sNames = "Stage_Raimu_v001:Room_02";
			//sNames = "Stage_Raimu_v001:Room_03";
			sNames = "Stage_Raimu_v001:Room_04";

			// ----

			string[] names = sNames.Split(':');
			string worldName = names[0];
			string startMapName = names[1];

			using (new WorldGameMaster())
			{
				WorldGameMaster.I.World = new World(worldName, startMapName);
				WorldGameMaster.I.Status = new GameStatus();
				WorldGameMaster.I.Perform();
			}
		}
	}
}
