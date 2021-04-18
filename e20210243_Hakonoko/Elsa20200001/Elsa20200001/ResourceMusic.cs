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

		public DDMusic Title = new DDMusic(@"dat\夜野ムクロジ\komore-bi.mp3");
		public DDMusic Novel = new DDMusic(@"dat\夜野ムクロジ\si-piano.mp3");
		public DDMusic Ending_死亡 = new DDMusic(@"dat\夜野ムクロジ\sito-sito.mp3");
		public DDMusic Music_0001 = new DDMusic(@"dat\夜野ムクロジ\yoake.mp3");
		public DDMusic Music_0002 = new DDMusic(@"dat\夜野ムクロジ\yu-rei.mp3");

		public ResourceMusic()
		{
			//this.Dummy.Volume = 0.1; // 非推奨
		}
	}
}
