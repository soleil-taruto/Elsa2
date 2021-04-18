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

		public DDMusic Title = new DDMusic(true, @"dat\フリー素材\ajapa\TheLabyrinth.mp3");

		public ResourceMusic()
		{
			//this.Dummy.Volume = 0.1; // 非推奨
		}
	}
}
