using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.GameCommons;

namespace Charlotte
{
	public class ResourceSE
	{
		public DDSE Dummy = new DDSE(@"dat\General\muon.wav");

		public DDSE EnemyDamaged = new DDSE(@"dat\小森平\sfxrse\05hit\hit04.mp3");
		public DDSE EnemyKilled = new DDSE(@"dat\小森平\sfxrse\03explosion\explosion06.mp3");

		public ResourceSE()
		{
			//this.Dummy.Volume = 0.1; // 非推奨
		}
	}
}
