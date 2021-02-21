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
		public DDPicture DummyScreen = DDPictureLoaders.Standard(@"dat\DummyScreen.png");

		//public DDPicture Copyright = DDPictureLoaders.Standard(@"dat\Claris_Resource\Logo.png");
		public DDPicture Copyright = DDPictureLoaders.Standard(@"dat\Logo\Copyright.png");

		public DDPicture TitleWall = DDPictureLoaders.Standard(@"dat\Claris_Resource\k-after\BG23a_80.jpg");
		public DDPicture Title = DDPictureLoaders.Standard(@"dat\Claris_Resource\Actor83_1.png");
		public DDPicture TitleMenuItem = DDPictureLoaders.Standard(@"dat\Claris_Resource\TitleMenuItem.png");

		public DDPicture Player_00 = DDPictureLoaders.Standard(@"dat\Claris_Resource\usui\Actor83\$Actor83.png");
		public DDPicture Player_01 = DDPictureLoaders.Standard(@"dat\Claris_Resource\usui\Actor83\$Actor83_1.png");
		public DDPicture Player_02 = DDPictureLoaders.Standard(@"dat\Claris_Resource\usui\Actor83\$Actor83_2.png");
		public DDPicture Player_03 = DDPictureLoaders.Standard(@"dat\Claris_Resource\usui\Actor83\$Actor83_3.png");
		public DDPicture Player_05 = DDPictureLoaders.Standard(@"dat\Claris_Resource\usui\Actor83\$Actor83_5.png");
		public DDPicture PlayerFace_00 = DDPictureLoaders.Standard(@"dat\Claris_Resource\usui\Actor83\Actor83.png");
		public DDPicture PlayerFace_01 = DDPictureLoaders.Standard(@"dat\Claris_Resource\usui\Actor83\Actor83_ex.png");
		public DDPicture PlayerRaf = DDPictureLoaders.Standard(@"dat\Claris_Resource\usui\Actor83\Actor83_raf.jpg");

		public DDPicture Tile_A1 = DDPictureLoaders.Standard(@"dat\Claris_Resource\FSM\vx_map\TileA1.png");
		public DDPicture Tile_A2 = DDPictureLoaders.Standard(@"dat\Claris_Resource\FSM\vx_map\TileA2.png");
		public DDPicture Tile_A3 = DDPictureLoaders.Standard(@"dat\Claris_Resource\FSM\vx_map\TileA3.png");
		public DDPicture Tile_A4 = DDPictureLoaders.Standard(@"dat\Claris_Resource\FSM\vx_map\TileA4.png");
		public DDPicture Tile_A5 = DDPictureLoaders.Standard(@"dat\Claris_Resource\FSM\vx_map\TileA5.png");
		public DDPicture Tile_B = DDPictureLoaders.Standard(@"dat\Claris_Resource\FSM\vx_map\TileB.png");
		public DDPicture Tile_C = DDPictureLoaders.Standard(@"dat\Claris_Resource\FSM\vx_map\TileC.png");
		public DDPicture Tile_D = DDPictureLoaders.Standard(@"dat\Claris_Resource\FSM\vx_map\TileD.png");
		public DDPicture Tile_E = DDPictureLoaders.Standard(@"dat\Claris_Resource\FSM\vx_map\TileE.png");

		public DDPicture IconSet = DDPictureLoaders.Standard(@"dat\Claris_Resource\usui\IconSet.png");

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
