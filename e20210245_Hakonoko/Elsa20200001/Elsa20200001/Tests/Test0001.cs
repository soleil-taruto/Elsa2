using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DxLibDLL;
using Charlotte.Commons;
using Charlotte.GameCommons;

namespace Charlotte.Tests
{
	public class Test0001
	{
		/// <summary>
		/// NG !!!
		/// </summary>
		public void Test01()
		{
			const int DER_WH = 60;
			const int DERS_W = 16;
			const int DERS_H = 9;

			DDPicture wallPicture = DDCCResource.GetPicture(@"dat\テスト用\IMG_20160000_000040.jpg");

			DDPicture[,] ders = DDDerivations.GetAnimation(DDGround.MainScreen.ToPicture(), 0, 0, DERS_W, DERS_H, DER_WH, DER_WH);
			DDSubScreen screen = new DDSubScreen(DDConsts.Screen_W, DDConsts.Screen_H);

			for (; ; )
			{
				DDDraw.DrawCenter(wallPicture, DDConsts.Screen_W / 2, DDConsts.Screen_H / 2);

				using (screen.Section())
				{
					for (int x = 0; x < DERS_W; x++)
						for (int y = 0; y < DERS_H; y++)
							DDDraw.DrawRect(ders[x, y], x * DER_WH, y * DER_WH, (x + 1) * DER_WH, (y + 1) * DER_WH);
				}

				DDDraw.DrawSimple(screen.ToPicture(), 0, 0);

				DDEngine.EachFrame();
			}
		}

		/// <summary>
		/// NG !!!
		/// </summary>
		public void Test02()
		{
			const int DER_WH = 60;
			const int DERS_W = 16;
			const int DERS_H = 9;

			DDPicture wallPicture = DDCCResource.GetPicture(@"dat\テスト用\IMG_20160000_000040.jpg");

			DDSubScreen screen = new DDSubScreen(DDConsts.Screen_W, DDConsts.Screen_H);
			DDPicture[,] ders = DDDerivations.GetAnimation(screen.ToPicture(), 0, 0, DERS_W, DERS_H, DER_WH, DER_WH);

			for (; ; )
			{
				DDDraw.DrawCenter(wallPicture, DDConsts.Screen_W / 2, DDConsts.Screen_H / 2);

				using (screen.Section())
				{
					DDDraw.DrawSimple(DDGround.MainScreen.ToPicture(), 0, 0);
				}

				for (int x = 0; x < DERS_W; x++)
					for (int y = 0; y < DERS_H; y++)
						DDDraw.DrawRect(ders[x, y], x * DER_WH, y * DER_WH, (x + 1) * DER_WH, (y + 1) * DER_WH);

				DDEngine.EachFrame();
			}
		}

		/// <summary>
		/// NG !!!
		/// </summary>
		public void Test03()
		{
			const int DER_WH = 60;
			const int DERS_W = 16;
			const int DERS_H = 9;

			DDPicture wallPicture = DDCCResource.GetPicture(@"dat\テスト用\IMG_20160000_000040.jpg");

			DDSubScreen screen = new DDSubScreen(DDConsts.Screen_W, DDConsts.Screen_H);
			DDPicture[,] ders = DDDerivations.GetAnimation(screen.ToPicture(), 0, 0, DERS_W, DERS_H, DER_WH, DER_WH);

			for (; ; )
			{
				using (screen.Section())
				{
					DDDraw.DrawCenter(wallPicture, DDConsts.Screen_W / 2, DDConsts.Screen_H / 2);
				}

				for (int x = 0; x < DERS_W; x++)
					for (int y = 0; y < DERS_H; y++)
						DDDraw.DrawRect(ders[x, y], x * DER_WH, y * DER_WH, (x + 1) * DER_WH, (y + 1) * DER_WH);

				DDEngine.EachFrame();
			}
		}

		public void Test04()
		{
			const int DER_WH = 60;
			const int DERS_W = 16;
			const int DERS_H = 9;

			DDPicture wallPicture = DDCCResource.GetPicture(@"dat\テスト用\IMG_20160000_000040.jpg");

			DDSubScreen screen = new DDSubScreen(DDConsts.Screen_W, DDConsts.Screen_H);
			DDSubScreen[,] ders = new DDSubScreen[DERS_W, DERS_H];

			for (int x = 0; x < DERS_W; x++)
				for (int y = 0; y < DERS_H; y++)
					ders[x, y] = new DDSubScreen(DER_WH, DER_WH);

			for (; ; )
			{
				DDDraw.DrawCenter(wallPicture, DDConsts.Screen_W / 2, DDConsts.Screen_H / 2);

				for (int x = 0; x < DERS_W; x++)
					for (int y = 0; y < DERS_H; y++)
						using (ders[x, y].Section())
							DX.DrawRectGraph(0, 0, x * DER_WH, y * DER_WH, (x + 1) * DER_WH, (y + 1) * DER_WH, DDGround.MainScreen.GetHandle(), 0);

				using (screen.Section())
				{
					for (int x = 0; x < DERS_W; x++)
						for (int y = 0; y < DERS_H; y++)
							DDDraw.DrawFree(
								ders[x, y].ToPicture(),
								new D2Point((x + 0) * DER_WH, (y + 0) * DER_WH),
								new D2Point((x + 1) * DER_WH, (y + 0) * DER_WH),
								new D2Point((x + 1) * DER_WH, (y + 1) * DER_WH),
								new D2Point((x + 0) * DER_WH, (y + 1) * DER_WH)
								);
				}

				DDDraw.DrawSimple(screen.ToPicture(), 0, 0);

				DDEngine.EachFrame();
			}
		}

		public void Test05()
		{
			const int DER_WH = 60;
			const int DERS_W = 16;
			const int DERS_H = 9;

			DDPicture wallPicture = DDCCResource.GetPicture(@"dat\テスト用\IMG_20160000_000040.jpg");

			DDSubScreen screen = new DDSubScreen(DDConsts.Screen_W, DDConsts.Screen_H);
			DDSubScreen[,] ders = new DDSubScreen[DERS_W, DERS_H];

			for (int x = 0; x < DERS_W; x++)
				for (int y = 0; y < DERS_H; y++)
					ders[x, y] = new DDSubScreen(DER_WH, DER_WH);

			for (; ; )
			{
				DDDraw.DrawCenter(wallPicture, DDConsts.Screen_W / 2, DDConsts.Screen_H / 2);

				for (int x = 0; x < DERS_W; x++)
					for (int y = 0; y < DERS_H; y++)
						using (ders[x, y].Section())
							DX.DrawRectGraph(0, 0, x * DER_WH, y * DER_WH, (x + 1) * DER_WH, (y + 1) * DER_WH, DDGround.MainScreen.GetHandle(), 0);

				using (screen.Section())
				{
					for (int x = 0; x < DERS_W; x++)
					{
						for (int y = 0; y < DERS_H; y++)
						{
							D2Point lt = new D2Point((x + 0) * DER_WH, (y + 0) * DER_WH);
							D2Point rt = new D2Point((x + 1) * DER_WH, (y + 0) * DER_WH);
							D2Point rb = new D2Point((x + 1) * DER_WH, (y + 1) * DER_WH);
							D2Point lb = new D2Point((x + 0) * DER_WH, (y + 1) * DER_WH);

							Func<D2Point, D2Point> f;

							// -- choose one --

							//f = Test05_波紋効果による頂点の移動;
							f = Test05_波紋効果による頂点の移動_高速;

							// --

							lt = f(lt);
							rt = f(rt);
							rb = f(rb);
							lb = f(lb);

							DDDraw.DrawFree(ders[x, y].ToPicture(), lt, rt, rb, lb);
						}
					}
				}

				DDDraw.DrawSimple(screen.ToPicture(), 0, 0);

				DDEngine.EachFrame();
			}
		}

		private static D2Point Test05_波紋効果による頂点の移動(D2Point pt)
		{
			pt.X -= DDConsts.Screen_W / 2;
			pt.Y -= DDConsts.Screen_H / 2;

			double wave_r = DDEngine.ProcFrame % 1200 - 300;
			double distance = DDUtils.GetDistance(pt);
			double d = distance;

			d -= wave_r;

			// distance の -300 ～ 300 を 0.0 ～ 1.0 にする。
			d /= 300.0;
			d += 1.0;
			d /= 2.0;

			if (0.0 < d && d < 1.0)
			{
				d *= 2.0;

				if (1.0 < d)
					d = 2.0 - d;

				distance += DDUtils.SCurve(d) * 100.0;

				DDUtils.MakeXYSpeed(0.0, 0.0, pt.X, pt.Y, distance, out pt.X, out pt.Y); // distance を pt に反映する。
			}

			// restore
			pt.X += DDConsts.Screen_W / 2;
			pt.Y += DDConsts.Screen_H / 2;

			return pt;
		}

		private static D2Point Test05_波紋効果による頂点の移動_高速(D2Point pt)
		{
			pt.X -= DDConsts.Screen_W / 2;
			pt.Y -= DDConsts.Screen_H / 2;

			double wave_r = DDEngine.ProcFrame % 100;
			double distance = DDUtils.GetDistance(pt);
			double d = distance;

			// 画面の中心から左上までの距離 == 550.*

			d -= wave_r * 6;

			// distance の -50 ～ 50 を 0.0 ～ 1.0 にする。
			d /= 50.0;
			d += 1.0;
			d /= 2.0;

			if (0.0 < d && d < 1.0)
			{
				d *= 2.0;

				if (1.0 < d)
					d = 2.0 - d;

				distance += DDUtils.SCurve(d) * 100.0;

				DDUtils.MakeXYSpeed(0.0, 0.0, pt.X, pt.Y, distance, out pt.X, out pt.Y); // distance を pt に反映する。
			}

			// restore
			pt.X += DDConsts.Screen_W / 2;
			pt.Y += DDConsts.Screen_H / 2;

			return pt;
		}
	}
}
