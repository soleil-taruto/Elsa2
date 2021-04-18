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

		public DDMusic Title = new DDMusic(true, @"dat\夜野ムクロジ\komore-bi.mp3");
		public DDMusic Novel = new DDMusic(true, @"dat\夜野ムクロジ\si-piano.mp3");
		public DDMusic Ending_死亡 = new DDMusic(true, @"dat\夜野ムクロジ\sito-sito.mp3");
		public DDMusic Music_0001 = new DDMusic(true, @"dat\夜野ムクロジ\yu-rei.mp3");

		public DDMusic Floor_01 = new DDMusic(false, @"dat\甘茶の音楽工房\orb1.mp3");
		public DDMusic Floor_02 = new DDMusic(false, @"dat\甘茶の音楽工房\orb2.mp3");
		public DDMusic Floor_03 = new DDMusic(false, @"dat\甘茶の音楽工房\daremoinaisabaku.mp3");
		public DDMusic Floor_04 = new DDMusic(false, @"dat\甘茶の音楽工房\natsunokiri.mp3");
		public DDMusic Floor_05 = new DDMusic(false, @"dat\甘茶の音楽工房\kanashiminotexture2.mp3");
		public DDMusic Floor_06 = new DDMusic(false, @"dat\甘茶の音楽工房\kanashiminotexture1.mp3");
		public DDMusic Floor_07 = new DDMusic(false, @"dat\甘茶の音楽工房\moeochirusakura.mp3");
		public DDMusic Floor_08 = new DDMusic(false, @"dat\甘茶の音楽工房\seijaku2.mp3");
		public DDMusic Floor_09 = new DDMusic(false, @"dat\General\muon.wav"); // TODO

		public ResourceMusic()
		{
			//this.Dummy.Volume = 0.1; // 非推奨
		}
	}
}
