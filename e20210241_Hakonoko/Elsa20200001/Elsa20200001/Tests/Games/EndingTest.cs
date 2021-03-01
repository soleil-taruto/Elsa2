using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Games;

namespace Charlotte.Tests.Games
{
	public class EndingTest
	{
		public void Test_死亡()
		{
			new Ending_死亡().Perform();
		}

		public void Test_生還()
		{
			new Ending_生還().Perform();
		}
	}
}
