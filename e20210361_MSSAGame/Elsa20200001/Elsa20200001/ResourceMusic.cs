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

		public DDMusic Title = new DDMusic(true, @"dat\ユーフルカ\Crazy-Halloween-Night_loop\Crazy-Halloween-Night_loop.ogg").SetLoop(184074, 4141574);

		public DDMusic Field_01 = new DDMusic(false, @"dat\ユーフルカ\Everlasting-Snow_loop\Everlasting-Snow_loop.ogg").SetLoop(551345, 4961249);
		public DDMusic Field_02 = new DDMusic(false, @"dat\ユーフルカ\Silent-Avalon_loop\Silent-Avalon_loop.ogg").SetLoop(875922, 7883346);

		public DDMusic 神さびた古戦場 = new DDMusic(false, @"dat\みるふぃ\nc200903.mp3");

		public ResourceMusic()
		{
			//this.Dummy.Volume = 0.1; // 非推奨
		}
	}
}
