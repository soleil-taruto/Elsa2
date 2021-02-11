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
				Game.I.World = new World("w0001", "t0001");
				Game.I.Status = new GameStatus();
				Game.I.Perform();
			}
		}

		public void Test03()
		{
			string sNames;

			// ---- choose one ----

			sNames = "w0001:t0001";
			//sNames = "w0001:t0002";
			//sNames = "w0001:t0003";

			// ----

			string[] names = sNames.Split(':');
			string worldName = names[0];
			string startMapName = names[1];

			using (new Game())
			{
				Game.I.World = new World(worldName, startMapName);
				Game.I.Status = new GameStatus();
				Game.I.Perform();
			}
		}
	}
}
