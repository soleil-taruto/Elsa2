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
		public DDMusic Endinge_死亡 = new DDMusic(@"dat\夜野ムクロジ\sito-sito.mp3");
		public DDMusic Music_0001 = new DDMusic(@"dat\夜野ムクロジ\yu-rei.mp3");

		public DDMusic Floor_01 = new DDMusic(@"dat\甘茶の音楽工房\orb1_muon-100-100.mp3");
		public DDMusic Floor_02 = new DDMusic(@"dat\甘茶の音楽工房\orb2_muon-100-100.mp3");
		public DDMusic Floor_03 = new DDMusic(@"dat\甘茶の音楽工房\daremoinaisabaku_muon-100-100.mp3");
		public DDMusic Floor_04 = new DDMusic(@"dat\甘茶の音楽工房\natsunokiri_muon-100-100.mp3");
		public DDMusic Floor_05 = new DDMusic(@"dat\甘茶の音楽工房\kanashiminotexture2_muon-100-100.mp3");
		public DDMusic Floor_06 = new DDMusic(@"dat\甘茶の音楽工房\kanashiminotexture1_muon-100-100.mp3");
		public DDMusic Floor_07 = new DDMusic(@"dat\甘茶の音楽工房\moeochirusakura_muon-100-100.mp3");
		public DDMusic Floor_08 = new DDMusic(@"dat\甘茶の音楽工房\seijaku2_muon-100-100.mp3");
		public DDMusic Floor_09 = new DDMusic(@"dat\General\muon.wav"); // TODO

		public ResourceMusic()
		{
			//this.Dummy.Volume = 0.1; // 非推奨
		}
	}
}
