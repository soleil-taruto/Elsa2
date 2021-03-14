using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.GameCommons;
using Charlotte.Commons;

namespace Charlotte.Games
{
	public static class Effects
	{
		public static IEnumerable<bool> 小爆発(double x, double y)
		{
			foreach (DDScene scene in DDSceneUtils.Create(5))
			{
				DDDraw.SetAlpha(0.7);
				DDDraw.SetBright(1.0, 0.5, 0.5);
				DDDraw.DrawBegin(Ground.I.Picture.WhiteCircle, x - DDGround.ICamera.X, y - DDGround.ICamera.Y);
				DDDraw.DrawZoom(0.3 * scene.Rate);
				DDDraw.DrawEnd();
				DDDraw.Reset();

				yield return true;
			}
		}

		public static IEnumerable<bool> 中爆発(double x, double y)
		{
			foreach (DDScene scene in DDSceneUtils.Create(10))
			{
				DDDraw.SetAlpha(0.7);
				DDDraw.SetBright(1.0, 0.6, 0.3);
				DDDraw.DrawBegin(Ground.I.Picture.WhiteCircle, x - DDGround.ICamera.X, y - DDGround.ICamera.Y);
				DDDraw.DrawZoom(3.0 * scene.Rate);
				DDDraw.DrawEnd();
				DDDraw.Reset();

				yield return true;
			}
		}

		public static IEnumerable<bool> FireBall爆発(double x, double y)
		{
			foreach (DDScene scene in DDSceneUtils.Create(10))
			{
				DDDraw.SetAlpha(0.7);
				DDDraw.SetBright(1.0, 1.0, 0.0);
				DDDraw.DrawBegin(Ground.I.Picture.WhiteCircle, x - DDGround.ICamera.X, y - DDGround.ICamera.Y);
				DDDraw.DrawZoom(1.0 * scene.Rate);
				DDDraw.DrawEnd();
				DDDraw.Reset();

				yield return true;
			}
		}

		public static IEnumerable<bool> Liteフラッシュ()
		{
			foreach (DDScene scene in DDSceneUtils.Create(60))
			{
				DDCurtain.DrawCurtain((1.0 - scene.Rate) * 0.5);
				yield return true;
			}
		}

		public static IEnumerable<bool> 行き先案内(double centerX, double centerY, bool 正しいルート)
		{
			double rot = DDUtils.ToAngle(DDEngine.ProcFrame, 60);

			foreach (DDScene scene in DDSceneUtils.Create(100))
			{
				centerX = Game.I.Player.X;
				centerY = Game.I.Player.Y;

				double d = scene.Rate * 500.0;
				double x = centerX + Math.Cos(rot) * d;
				double y = centerY + Math.Sin(rot) * d;

				I2Point mapPt = GameCommon.ToTablePoint(new D2Point(x, y));
				MapCell cell = Game.I.Map.GetCell(mapPt);

				if (!cell.IsDefault && cell.IsWall())
				{
					if (正しいルート)
					{
						cell.ColorPhase *= 0.5;
					}
					else
					{
						DDUtils.Approach(ref cell.ColorPhase, 1.0, 0.5);
					}
				}
				rot += 0.1;

				yield return true;
			}
		}
	}
}
