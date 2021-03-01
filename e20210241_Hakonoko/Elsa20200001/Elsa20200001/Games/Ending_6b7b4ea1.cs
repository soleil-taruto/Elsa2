using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.GameCommons;

namespace Charlotte.Games
{
	public class Ending_死亡 : Ending
	{
		protected override IEnumerable<int> Script()
		{
			DDGround.EL.Add(() =>
			{
				DDPrint.SetPrint();
				DDPrint.Print("Ending_死亡");

				return true;
			});

			yield return 300;
		}
	}
}
