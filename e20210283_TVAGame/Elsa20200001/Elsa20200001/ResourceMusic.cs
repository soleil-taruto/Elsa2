using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.GameCommons;

namespace Charlotte
{
	public class ResourceMusic
	{
		public DDMusic Dummy = new DDMusic(true, @"dat\General\muon.wav");

		public DDMusic Title = new DDMusic(true, @"dat\フリー素材\hmix\n51.mp3");

		public DDMusic Field_01 = new DDMusic(false, @"dat\フリー素材\hmix\m2.mp3");
		public DDMusic Field_02 = new DDMusic(false, @"dat\フリー素材\hmix\n19.mp3");

		public DDMusic 神さびた古戦場 = new DDMusic(false, @"dat\フリー素材\みるふぃ\nc200903.mp3");

		public ResourceMusic()
		{
			//this.Dummy.Volume = 0.1; // 非推奨
		}
	}
}
