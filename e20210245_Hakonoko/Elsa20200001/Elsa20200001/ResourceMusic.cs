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
		public DDMusic Ending_生還 = new DDMusic(true, @"dat\甘茶の音楽工房\seijakunohoshizora.mp3");
		public DDMusic Ending_復讐 = new DDMusic(true, @"dat\ユーフルカ\Horror-ginen_loop.ogg");
		public DDMusic Music_0001 = new DDMusic(true, @"dat\夜野ムクロジ\yu-rei.mp3");

		public DDMusic Floor_01 = new DDMusic(false, @"dat\甘茶の音楽工房\orb1_muon-100-100.mp3");
		public DDMusic Floor_02 = new DDMusic(false, @"dat\甘茶の音楽工房\orb2_muon-100-100.mp3");
		public DDMusic Floor_03 = new DDMusic(false, @"dat\甘茶の音楽工房\daremoinaisabaku_muon-100-100.mp3");
		public DDMusic Floor_04 = new DDMusic(false, @"dat\甘茶の音楽工房\natsunokiri_muon-100-100.mp3");
		public DDMusic Floor_05 = new DDMusic(false, @"dat\甘茶の音楽工房\amenoprelude_muon-100-100.mp3");
		public DDMusic Floor_06 = new DDMusic(false, @"dat\甘茶の音楽工房\kanashiminotexture1_muon-100-100.mp3");
		public DDMusic Floor_07 = new DDMusic(false, @"dat\甘茶の音楽工房\moeochirusakura_muon-100-100.mp3");
		public DDMusic Floor_08 = new DDMusic(false, @"dat\甘茶の音楽工房\kanashiminotexture2_muon-100-100.mp3");
		public DDMusic Floor_09 = new DDMusic(false, @"dat\ユーフルカ\Horror-naraku_loop.ogg").SetLoopByStLength(974485, 1940413);
		public DDMusic FinalZone = new DDMusic(false, @"dat\ユーフルカ\Horror-NeverLookBack_loop.ogg").SetLoopByStLength(456198, 3097689);

		public DDMusic 地鳴り = new DDMusic(true, @"dat\DovaSyndrome\ゴゴゴ_激しい地鳴り音.mp3").SetLoopByStEnd(44100 + 12345, 44100 * 18 + 12345);

		public ResourceMusic()
		{
			//this.Dummy.Volume = 0.1; // 非推奨
		}
	}
}
