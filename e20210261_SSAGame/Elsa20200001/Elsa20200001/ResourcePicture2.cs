using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Commons;
using Charlotte.GameCommons;

namespace Charlotte
{
	public class ResourcePicture2
	{
		//public DDPicture[] Dummy = DDDerivations.GetAnimation(Ground.I.Picture.Dummy, 0, 0, 25, 25, 2, 2).ToArray();

		// ---- 因幡てゐ ----

		public DDPicture[] Tewi_立ち = DDDerivations.GetAnimation_YX(Ground.I.Picture.Tewi_01, 0, 0, 160, 192).Take(12).ToArray();
		public DDPicture[] Tewi_振り向き;
		public DDPicture[] Tewi_しゃがみ;
		public DDPicture[] Tewi_しゃがみ振り向き;
		public DDPicture[] Tewi_ジャンプ;
		public DDPicture[] Tewi_歩く;
		public DDPicture[] Tewi_走る;
		public DDPicture[] Tewi_小ダメージ;
		public DDPicture[] Tewi_大ダメージ;
		public DDPicture[] Tewi_しゃがみ小ダメージ;
		public DDPicture[] Tewi_しゃがみ大ダメージ;
		public DDPicture[] Tewi_飛翔;
		public DDPicture[] Tewi_弱攻撃;
		public DDPicture[] Tewi_中攻撃;
		public DDPicture[] Tewi_強攻撃;
		public DDPicture[] Tewi_しゃがみ弱攻撃;
		public DDPicture[] Tewi_しゃがみ中攻撃;
		public DDPicture[] Tewi_しゃがみ強攻撃;
		public DDPicture[] Tewi_ジャンプ弱攻撃;
		public DDPicture[] Tewi_ジャンプ中攻撃;
		public DDPicture[] Tewi_ジャンプ強攻撃;

		// ---- チルノ ----

		public DDPicture[] Cirno_立ち;
		public DDPicture[] Cirno_振り向き;
		public DDPicture[] Cirno_しゃがみ;
		public DDPicture[] Cirno_しゃがみ振り向き;
		public DDPicture[] Cirno_ジャンプ;
		public DDPicture[] Cirno_歩く;
		public DDPicture[] Cirno_走る;
		public DDPicture[] Cirno_小ダメージ;
		public DDPicture[] Cirno_大ダメージ;
		public DDPicture[] Cirno_しゃがみ小ダメージ;
		public DDPicture[] Cirno_しゃがみ大ダメージ;
		public DDPicture[] Cirno_飛翔;
		public DDPicture[] Cirno_弱攻撃;
		public DDPicture[] Cirno_中攻撃;
		public DDPicture[] Cirno_強攻撃;
		public DDPicture[] Cirno_しゃがみ弱攻撃;
		public DDPicture[] Cirno_しゃがみ中攻撃;
		public DDPicture[] Cirno_しゃがみ強攻撃;
		public DDPicture[] Cirno_ジャンプ弱攻撃;
		public DDPicture[] Cirno_ジャンプ中攻撃;
		public DDPicture[] Cirno_ジャンプ強攻撃;

		// ----

		public DDPicture[] Enemy_神奈子 = DDDerivations.GetAnimation_YX(Ground.I.Picture.Enemy_神奈子, 0, 0, 250, 250).ToArray();
	}
}
