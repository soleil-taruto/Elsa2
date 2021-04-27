using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.GameCommons;

namespace Charlotte
{
	public class ResourceSE
	{
		public DDSE Dummy = new DDSE(@"dat\General\muon.wav");

		public DDSE Jump = new DDSE(@"dat\小森平\jump12.mp3");

		public DDSE Goal = new DDSE(@"dat\効果音ラボ\ピアノの単音.mp3");
		public DDSE Miss = new DDSE(@"dat\効果音ラボ\カーソル移動6.mp3");
		public DDSE Death = new DDSE(@"dat\効果音ラボ\メニューを開く2.mp3");
		public DDSE Reborn = new DDSE(@"dat\効果音ラボ\決定、ボタン押下33.mp3");
		public DDSE Snapshot = new DDSE(@"dat\効果音ラボ\カメラのシャッター1.mp3");

		public ResourceSE()
		{
			//this.Dummy.Volume = 0.1; // 非推奨
		}
	}
}
