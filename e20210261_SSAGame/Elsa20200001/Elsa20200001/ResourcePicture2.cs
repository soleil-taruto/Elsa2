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

		// 基本
		//
		public DDPicture[] Tewi_立ち;
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

		// 攻撃
		//
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

		// TODO

		// ----

		public DDPicture[] Enemy_神奈子 = DDDerivations.GetAnimation_YX(Ground.I.Picture.Enemy_神奈子, 0, 0, 250, 250).ToArray();
	}
}
