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

		public DDMusic Title = new DDMusic(true, @"dat\ユーフルカ\Voyage_loop\Voyage_loop.ogg").SetLoop(655565, 4860197);

		public DDMusic Field_01 = new DDMusic(false, @"dat\ユーフルカ\The-sacred-place_loop\The-sacred-place_loop.ogg").SetLoop(800621, 4233349);

		public DDMusic 神さびた古戦場 = new DDMusic(false, @"dat\みるふぃ\nc200903.mp3");

		public ResourceMusic()
		{
			//this.Dummy.Volume = 0.1; // 非推奨
		}
	}
}
