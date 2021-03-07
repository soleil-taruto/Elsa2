using System;
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
				Ground.I.Picture2.ほむらシールド,
				Ground.I.Picture2.ほむらバズーカ,
				Ground.I.Picture2.ほむら死亡,
				Ground.I.Picture2.ほむら走り,
				Ground.I.Picture2.ほむら滞空攻撃,
				Ground.I.Picture2.ほむら被弾,
				Ground.I.Picture2.ほむら立ち,

				Ground.I.Picture2.さやか死亡,
				Ground.I.Picture2.さやか接地攻撃,
				Ground.I.Picture2.さやか走り,
				Ground.I.Picture2.さやか滞空攻撃,
				Ground.I.Picture2.さやか突き,
				Ground.I.Picture2.さやか被弾,
				Ground.I.Picture2.さやか立ち,
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

				DDPrint.SetDebug();
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
