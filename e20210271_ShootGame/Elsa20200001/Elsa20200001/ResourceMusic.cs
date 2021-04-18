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

		public DDMusic Title = new DDMusic(true, @"dat\hmix\n118.mp3");

		public DDMusic Stage_01 = new DDMusic(false, @"dat\hmix\n138.mp3");
		public DDMusic Stage_02 = new DDMusic(false, @"dat\hmix\n70.mp3");
		public DDMusic Stage_03 = new DDMusic(false, @"dat\hmix\n13.mp3");

		// memo: ループ開始・終了位置を探すツール --> C:\Dev\wb\t20201022_SoundLoop

		public DDMusic Boss_01 = new DDMusic(false, @"dat\ユーフルカ\Battle-Vampire_loop\Battle-Vampire_loop.ogg").SetLoop(241468, 4205876);
		public DDMusic Boss_02 = new DDMusic(false, @"dat\ユーフルカ\Battle-Conflict_loop\Battle-Conflict_loop.ogg").SetLoop(281888, 3704134);
		public DDMusic Boss_03 = new DDMusic(false, @"dat\ユーフルカ\Battle-rapier_loop\Battle-rapier_loop.ogg").SetLoop(422312, 2767055);

		public ResourceMusic()
		{
			//this.Dummy.Volume = 0.1; // 非推奨
		}
	}
}
