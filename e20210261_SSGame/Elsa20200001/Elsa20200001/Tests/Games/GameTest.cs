using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Games;

namespace Charlotte.Tests.Games
{
	public class GameTest
	{
		public void Test01()
		{
			using (new Game())
			{
				Game.I.Perform();
			}
		}

		public void Test02()
		{
			using (new Game())
			{
				Game.I.World = new World("t0001");
				Game.I.Status = new GameStatus();
				Game.I.Perform();
			}
		}

		public void Test03()
		{
			string startMapName;

			// ---- choose one ----

			startMapName = "t0001";
			//startMapName = "t0002";
			//startMapName = "t0003";

			// ----

			using (new Game())
			{
				Game.I.World = new World(startMapName);
				Game.I.Status = new GameStatus();
				Game.I.Perform();
			}
		}
	}
}
