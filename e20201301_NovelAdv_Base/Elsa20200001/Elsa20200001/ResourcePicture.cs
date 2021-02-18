using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.GameCommons;

namespace Charlotte
{
	public class ResourcePicture
	{
		public DDPicture Dummy = DDPictureLoaders.Standard(@"dat\General\Dummy.png");
		public DDPicture WhiteBox = DDPictureLoaders.Standard(@"dat\General\WhiteBox.png");
		public DDPicture WhiteCircle = DDPictureLoaders.Standard(@"dat\General\WhiteCircle.png");
		public DDPicture DummyScreen = DDPictureLoaders.Standard(@"dat\DummyScreen.png");

		public DDPicture Copyright = DDPictureLoaders.Standard(@"dat\Logo\Copyright.png");

		public DDPicture MessageFrame_Message = DDPictureLoaders.Standard(@"dat\フリー素材\空想曲線\Messageframe_29\material\01 message\message.png");
		public DDPicture MessageFrame_Button = DDPictureLoaders.Standard(@"dat\フリー素材\空想曲線\Messageframe_29\material\02 button\button.png");
		public DDPicture MessageFrame_Button2 = DDPictureLoaders.Standard(@"dat\フリー素材\空想曲線\Messageframe_29\material\02 button\button2.png");
		public DDPicture MessageFrame_Button3 = DDPictureLoaders.Standard(@"dat\フリー素材\空想曲線\Messageframe_29\material\02 button\button3.png");
		public DDPicture MessageFrame_Auto = DDPictureLoaders.Standard(@"dat\フリー素材\空想曲線\Messageframe_29\material\03 system_button\auto.png");
		public DDPicture MessageFrame_Auto2 = DDPictureLoaders.Standard(@"dat\フリー素材\空想曲線\Messageframe_29\material\03 system_button\auto2.png");
		public DDPicture MessageFrame_Load = DDPictureLoaders.Standard(@"dat\フリー素材\空想曲線\Messageframe_29\material\03 system_button\load.png");
		public DDPicture MessageFrame_Load2 = DDPictureLoaders.Standard(@"dat\フリー素材\空想曲線\Messageframe_29\material\03 system_button\load2.png");
		public DDPicture MessageFrame_Log = DDPictureLoaders.Standard(@"dat\フリー素材\空想曲線\Messageframe_29\material\03 system_button\log.png");
		public DDPicture MessageFrame_Log2 = DDPictureLoaders.Standard(@"dat\フリー素材\空想曲線\Messageframe_29\material\03 system_button\log2.png");
		public DDPicture MessageFrame_Menu = DDPictureLoaders.Standard(@"dat\フリー素材\空想曲線\Messageframe_29\material\03 system_button\menu.png");
		public DDPicture MessageFrame_Menu2 = DDPictureLoaders.Standard(@"dat\フリー素材\空想曲線\Messageframe_29\material\03 system_button\menu2.png");
		public DDPicture MessageFrame_Save = DDPictureLoaders.Standard(@"dat\フリー素材\空想曲線\Messageframe_29\material\03 system_button\save.png");
		public DDPicture MessageFrame_Save2 = DDPictureLoaders.Standard(@"dat\フリー素材\空想曲線\Messageframe_29\material\03 system_button\save2.png");
		public DDPicture MessageFrame_Skip = DDPictureLoaders.Standard(@"dat\フリー素材\空想曲線\Messageframe_29\material\03 system_button\skip.png");
		public DDPicture MessageFrame_Skip2 = DDPictureLoaders.Standard(@"dat\フリー素材\空想曲線\Messageframe_29\material\03 system_button\skip2.png");

		public DDPicture 弦巻マキ01 = DDPictureLoaders.Standard(@"dat\フリー素材\からい\ゆかマキ制服\maki01.png");
		public DDPicture 弦巻マキ02 = DDPictureLoaders.Standard(@"dat\フリー素材\からい\ゆかマキ制服\maki02.png");
		public DDPicture 弦巻マキ03 = DDPictureLoaders.Standard(@"dat\フリー素材\からい\ゆかマキ制服\maki03.png");
		public DDPicture 弦巻マキ04 = DDPictureLoaders.Standard(@"dat\フリー素材\からい\ゆかマキ制服\maki04.png");
		public DDPicture 弦巻マキ05 = DDPictureLoaders.Standard(@"dat\フリー素材\からい\ゆかマキ制服\maki05.png");
		public DDPicture 弦巻マキ06 = DDPictureLoaders.Standard(@"dat\フリー素材\からい\ゆかマキ制服\maki06.png");
		public DDPicture 結月ゆかり01 = DDPictureLoaders.Standard(@"dat\フリー素材\からい\ゆかマキ制服\yukari01.png");
		public DDPicture 結月ゆかり02 = DDPictureLoaders.Standard(@"dat\フリー素材\からい\ゆかマキ制服\yukari02.png");
		public DDPicture 結月ゆかり03 = DDPictureLoaders.Standard(@"dat\フリー素材\からい\ゆかマキ制服\yukari03.png");
		public DDPicture 結月ゆかり04 = DDPictureLoaders.Standard(@"dat\フリー素材\からい\ゆかマキ制服\yukari04.png");
		public DDPicture 結月ゆかり05 = DDPictureLoaders.Standard(@"dat\フリー素材\からい\ゆかマキ制服\yukari05.png");
		public DDPicture 結月ゆかり11 = DDPictureLoaders.Standard(@"dat\フリー素材\からい\結月ゆかり\立ち絵1.png");
		public DDPicture 結月ゆかり12 = DDPictureLoaders.Standard(@"dat\フリー素材\からい\結月ゆかり\立ち絵2.png");
		public DDPicture 結月ゆかり13 = DDPictureLoaders.Standard(@"dat\フリー素材\からい\結月ゆかり\立ち絵3.png");
		public DDPicture 結月ゆかり14 = DDPictureLoaders.Standard(@"dat\フリー素材\からい\結月ゆかり\立ち絵4.png");
		public DDPicture 結月ゆかり15 = DDPictureLoaders.Standard(@"dat\フリー素材\からい\結月ゆかり\立ち絵5.png");
		public DDPicture 結月ゆかり16 = DDPictureLoaders.Standard(@"dat\フリー素材\からい\結月ゆかり\立ち絵6.png");
		public DDPicture 東北ずん子01 = DDPictureLoaders.Standard(@"dat\フリー素材\からい\東北ずん子\zunko01.png");
		public DDPicture 東北ずん子02 = DDPictureLoaders.Standard(@"dat\フリー素材\からい\東北ずん子\zunko02.png");
		public DDPicture 東北ずん子03 = DDPictureLoaders.Standard(@"dat\フリー素材\からい\東北ずん子\zunko03.png");
		public DDPicture 東北ずん子04 = DDPictureLoaders.Standard(@"dat\フリー素材\からい\東北ずん子\zunko04.png");
		public DDPicture 東北ずん子05 = DDPictureLoaders.Standard(@"dat\フリー素材\からい\東北ずん子\zunko05.png");
		public DDPicture 東北ずん子06 = DDPictureLoaders.Standard(@"dat\フリー素材\からい\東北ずん子\zunko06.png");
		public DDPicture 東北ずん子07 = DDPictureLoaders.Standard(@"dat\フリー素材\からい\東北ずん子\zunko07.png");
		public DDPicture 東北ずん子08 = DDPictureLoaders.Standard(@"dat\フリー素材\からい\東北ずん子\zunko08.png");
		public DDPicture 東北ずん子09 = DDPictureLoaders.Standard(@"dat\フリー素材\からい\東北ずん子\zunko09.png");
		public DDPicture 東北ずん子10 = DDPictureLoaders.Standard(@"dat\フリー素材\からい\東北ずん子\zunko10.png");
		public DDPicture 東北ずん子11 = DDPictureLoaders.Standard(@"dat\フリー素材\からい\東北ずん子\zunko11.png");

		public DDPicture 星屑物語02 = DDPictureLoaders.Standard(@"dat\フリー素材\星屑物語\82536135_p2.png");
		public DDPicture 星屑物語04 = DDPictureLoaders.Standard(@"dat\フリー素材\星屑物語\82536135_p4.png");
		public DDPicture 星屑物語05 = DDPictureLoaders.Standard(@"dat\フリー素材\星屑物語\82536135_p5.png");
		public DDPicture 星屑物語11 = DDPictureLoaders.Standard(@"dat\フリー素材\星屑物語\82536135_p11.png");

		public DDPicture ゆかりステッカーLogo = DDPictureLoaders.Standard(@"dat\フリー素材\結月ゆかりステッカー\8503377751145710088_1.png");
		public DDPicture ゆかりステッカーうさ01 = DDPictureLoaders.Standard(@"dat\フリー素材\結月ゆかりステッカー\4827885262456488968_1.png");
		public DDPicture ゆかりステッカーうさ02 = DDPictureLoaders.Standard(@"dat\フリー素材\結月ゆかりステッカー\6061915549009478152_1.png");
		public DDPicture ゆかりステッカーうさ03 = DDPictureLoaders.Standard(@"dat\フリー素材\結月ゆかりステッカー\6206035137146126856_1.png");
		public DDPicture ゆかりステッカー純 = DDPictureLoaders.Standard(@"dat\フリー素材\結月ゆかりステッカー\4621344201733342720_1.png");
		public DDPicture ゆかりステッカー穏 = DDPictureLoaders.Standard(@"dat\フリー素材\結月ゆかりステッカー\5908816251893843456_1.png");
		public DDPicture ゆかりステッカー凛 = DDPictureLoaders.Standard(@"dat\フリー素材\結月ゆかりステッカー\5917819044042375688_1.png");
	}
}
