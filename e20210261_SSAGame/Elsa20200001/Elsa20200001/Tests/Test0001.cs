﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Commons;
using Charlotte.GameCommons;

namespace Charlotte.Tests
{
	public class Test0001
	{
		public void Test01()
		{
			DDPicture[][] motions = new DDPicture[][]
			{
				Ground.I.Picture2.Tewi_立ち,
				//Ground.I.Picture2.Tewi_振り向き,
				//Ground.I.Picture2.Tewi_しゃがみ,
				//Ground.I.Picture2.Tewi_しゃがみ振り向き,
				//Ground.I.Picture2.Tewi_ジャンプ,
				//Ground.I.Picture2.Tewi_歩く,
				//Ground.I.Picture2.Tewi_走る,
				//Ground.I.Picture2.Tewi_小ダメージ,
				//Ground.I.Picture2.Tewi_大ダメージ,
				//Ground.I.Picture2.Tewi_しゃがみ小ダメージ,
				//Ground.I.Picture2.Tewi_しゃがみ大ダメージ,
				//Ground.I.Picture2.Tewi_飛翔,
				//Ground.I.Picture2.Tewi_弱攻撃,
				//Ground.I.Picture2.Tewi_中攻撃,
				//Ground.I.Picture2.Tewi_強攻撃,
				//Ground.I.Picture2.Tewi_しゃがみ弱攻撃,
				//Ground.I.Picture2.Tewi_しゃがみ中攻撃,
				//Ground.I.Picture2.Tewi_しゃがみ強攻撃,
				//Ground.I.Picture2.Tewi_ジャンプ弱攻撃,
				//Ground.I.Picture2.Tewi_ジャンプ中攻撃,
				//Ground.I.Picture2.Tewi_ジャンプ強攻撃,

				// TODO チルノ
			};

			DDEngine.FreezeInput();

			int motionIndex = 0;
			int komaIndex = 0;

			for (int frame = 0; ; frame++)
			{
				if (DDInput.DIR_8.IsPound())
					motionIndex--;

				if (DDInput.DIR_2.IsPound())
					motionIndex++;

				if (DDInput.DIR_4.IsPound())
					komaIndex--;

				if (DDInput.DIR_6.IsPound())
					komaIndex++;

				motionIndex += motions.Length;
				motionIndex %= motions.Length;

				DDPicture[] motion = motions[motionIndex];

				komaIndex = SCommon.ToRange(komaIndex, -1, motion.Length - 1);

				int koma = komaIndex;

				if (koma == -1)
					koma = (frame / 5) % motion.Length;

				DDCurtain.DrawCurtain(1.0);
				DDCurtain.DrawCurtain(-0.5);

				DDPrint.SetDebug(0, 16);
				DDPrint.Print(string.Join(", ", motionIndex, komaIndex, koma));

				DDDraw.DrawCenter(motion[koma], DDConsts.Screen_W / 2, DDConsts.Screen_H / 2);

				DDDraw.DrawBegin(Ground.I.Picture.Dummy, DDConsts.Screen_W / 2, DDConsts.Screen_H / 2);
				DDDraw.DrawZoom(0.1);
				DDDraw.DrawEnd();

				DDEngine.EachFrame();
			}
		}
	}
}
