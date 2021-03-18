using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Commons;
using Charlotte.GameCommons;

namespace Charlotte
{
	public class ResourcePicture
	{
		public DDPicture Dummy = DDPictureLoaders.Standard(@"dat\General\Dummy.png");
		public DDPicture WhiteBox = DDPictureLoaders.Standard(@"dat\General\WhiteBox.png");
		public DDPicture WhiteCircle = DDPictureLoaders.Standard(@"dat\General\WhiteCircle.png");
		public DDPicture DummyScreen = DDPictureLoaders.Standard(@"dat\General\DummyScreen.png");

		public DDPicture Copyright = DDPictureLoaders.Standard(@"dat\Logo\Copyright.png");

		public DDPicture Title = DDPictureLoaders.Standard(@"dat\run\22350006_big_p12.jpg");

		public DDPicture ほむらシールド = DDPictureLoaders.BgTrans(@"dat\Konbe\ほむらシールド.png");
		public DDPicture ほむらバズーカ = DDPictureLoaders.BgTrans(@"dat\Konbe\ほむらバズーカ.png");
		public DDPicture ほむら死亡 = DDPictureLoaders.BgTrans(@"dat\Konbe\ほむら死亡.png");
		public DDPicture ほむら走り = DDPictureLoaders.BgTrans(@"dat\Konbe\ほむら走り.png");
		public DDPicture ほむら滞空攻撃 = DDPictureLoaders.BgTrans(@"dat\Konbe\ほむら滞空攻撃.png");
		public DDPicture ほむら被弾 = DDPictureLoaders.BgTrans(@"dat\Konbe\ほむら被弾.png");
		public DDPicture ほむら立ち = DDPictureLoaders.BgTrans(@"dat\Konbe\ほむら立ち.png");

		public DDPicture さやか死亡 = DDPictureLoaders.BgTrans(@"dat\Konbe\さやか死亡.png");
		public DDPicture さやか接地攻撃 = DDPictureLoaders.BgTrans(@"dat\Konbe\さやか接地攻撃.png");
		public DDPicture さやか走り = DDPictureLoaders.BgTrans(@"dat\Konbe\さやか走り.png");
		public DDPicture さやか滞空攻撃 = DDPictureLoaders.BgTrans(@"dat\Konbe\さやか滞空攻撃.png");
		public DDPicture さやか突き = DDPictureLoaders.BgTrans(@"dat\Konbe\さやか突き.png");
		public DDPicture さやか被弾 = DDPictureLoaders.BgTrans(@"dat\Konbe\さやか被弾.png");
		public DDPicture さやか立ち = DDPictureLoaders.BgTrans(@"dat\Konbe\さやか立ち.png");

		public DDPicture Tile_None = DDPictureLoaders.Standard(@"dat\Tile\Tile_None.png");
		public DDPicture Tile_B0001 = DDPictureLoaders.Standard(@"dat\Tile\Tile_B0001.png");
		public DDPicture Tile_B0002 = DDPictureLoaders.Standard(@"dat\Tile\Tile_B0002.png");
		public DDPicture Tile_B0003 = DDPictureLoaders.Standard(@"dat\Tile\Tile_B0003.png");
		public DDPicture Tile_B0004 = DDPictureLoaders.Standard(@"dat\Tile\Tile_B0004.png");

		public DDPicture Wall_R0001 = DDPictureLoaders.Standard(@"dat\run\22350006_big_p30.jpg");
		public DDPicture Wall_R0002 = DDPictureLoaders.Standard(@"dat\run\22350006_big_p10.jpg");
		public DDPicture Wall_R0003 = DDPictureLoaders.Standard(@"dat\run\22350006_big_p31.jpg");

		public DDPicture Enemy_B0001_01 = DDPictureLoaders.Standard(@"dat\Enemy_B0001_01.png");
		public DDPicture Enemy_B0001_02 = DDPictureLoaders.Standard(@"dat\Enemy_B0001_02.png");
		public DDPicture Enemy_B0001_03 = DDPictureLoaders.Standard(@"dat\Enemy_B0001_03.png");
		public DDPicture Enemy_B0001_04 = DDPictureLoaders.Standard(@"dat\Enemy_B0001_04.png");

		public DDPicture Enemy_B0002_01 = DDPictureLoaders.Standard(@"dat\Enemy_B0002_01.png");
		public DDPicture Enemy_B0002_02 = DDPictureLoaders.Standard(@"dat\Enemy_B0002_02.png");

		public DDPicture Enemy_B0003 = DDPictureLoaders.Standard(@"dat\Enemy_B0003.png");

		//public DDPicture Enemy_神奈子 = DDPictureLoaders.Reduct(@"dat\きつね仮\yukkuri-kanako.png", 4); // 4000x4000 -> 1000x1000
		public DDPicture Enemy_神奈子 = DDPictureLoaders.Standard(@"dat\きつね仮\yukkuri-kanako.png"); // 1000x1000 resized png

		// ノベルパート用システム画像
		public DDPicture MessageFrame_Message = DDPictureLoaders.Standard(@"dat\Novel\フリー素材\空想曲線\Messageframe_29\material\01 message\message.png");
		public DDPicture MessageFrame_Button = DDPictureLoaders.Standard(@"dat\Novel\フリー素材\空想曲線\Messageframe_29\material\02 button\button.png");
		public DDPicture MessageFrame_Button2 = DDPictureLoaders.Standard(@"dat\Novel\フリー素材\空想曲線\Messageframe_29\material\02 button\button2.png");
		public DDPicture MessageFrame_Button3 = DDPictureLoaders.Standard(@"dat\Novel\フリー素材\空想曲線\Messageframe_29\material\02 button\button3.png");
		public DDPicture MessageFrame_Auto = DDPictureLoaders.Standard(@"dat\Novel\フリー素材\空想曲線\Messageframe_29\material\03 system_button\auto.png");
		public DDPicture MessageFrame_Auto2 = DDPictureLoaders.Standard(@"dat\Novel\フリー素材\空想曲線\Messageframe_29\material\03 system_button\auto2.png");
		public DDPicture MessageFrame_Load = DDPictureLoaders.Standard(@"dat\Novel\フリー素材\空想曲線\Messageframe_29\material\03 system_button\load.png");
		public DDPicture MessageFrame_Load2 = DDPictureLoaders.Standard(@"dat\Novel\フリー素材\空想曲線\Messageframe_29\material\03 system_button\load2.png");
		public DDPicture MessageFrame_Log = DDPictureLoaders.Standard(@"dat\Novel\フリー素材\空想曲線\Messageframe_29\material\03 system_button\log.png");
		public DDPicture MessageFrame_Log2 = DDPictureLoaders.Standard(@"dat\Novel\フリー素材\空想曲線\Messageframe_29\material\03 system_button\log2.png");
		public DDPicture MessageFrame_Menu = DDPictureLoaders.Standard(@"dat\Novel\フリー素材\空想曲線\Messageframe_29\material\03 system_button\menu.png");
		public DDPicture MessageFrame_Menu2 = DDPictureLoaders.Standard(@"dat\Novel\フリー素材\空想曲線\Messageframe_29\material\03 system_button\menu2.png");
		public DDPicture MessageFrame_Save = DDPictureLoaders.Standard(@"dat\Novel\フリー素材\空想曲線\Messageframe_29\material\03 system_button\save.png");
		public DDPicture MessageFrame_Save2 = DDPictureLoaders.Standard(@"dat\Novel\フリー素材\空想曲線\Messageframe_29\material\03 system_button\save2.png");
		public DDPicture MessageFrame_Skip = DDPictureLoaders.Standard(@"dat\Novel\フリー素材\空想曲線\Messageframe_29\material\03 system_button\skip.png");
		public DDPicture MessageFrame_Skip2 = DDPictureLoaders.Standard(@"dat\Novel\フリー素材\空想曲線\Messageframe_29\material\03 system_button\skip2.png");

		public DDPicture 結月ゆかり02 = DDPictureLoaders.Standard(@"dat\Novel\フリー素材\からい\ゆかマキ制服\yukari02.png"); // 仮
		public DDPicture 結月ゆかり03 = DDPictureLoaders.Standard(@"dat\Novel\フリー素材\からい\ゆかマキ制服\yukari03.png"); // 仮

		public DDPicture 弦巻マキ01 = DDPictureLoaders.Standard(@"dat\Novel\フリー素材\からい\ゆかマキ制服\maki01.png"); // 仮
		public DDPicture 弦巻マキ02 = DDPictureLoaders.Standard(@"dat\Novel\フリー素材\からい\ゆかマキ制服\maki02.png"); // 仮
	}
}
