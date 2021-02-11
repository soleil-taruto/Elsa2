using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.GameCommons;

namespace Charlotte
{
	public class ResourceMusic
	{
		public DDMusic Dummy = new DDMusic(@"dat\General\muon.wav");

		public DDMusic Title = new DDMusic(@"dat\フリー素材\魔王魂\bgm_maoudamashii_piano\bgm_maoudamashii_piano_milkeyway.mp3");

		public ResourceMusic()
		{
			//this.Dummy.Volume = 0.1; // 非推奨
		}
	}
}
