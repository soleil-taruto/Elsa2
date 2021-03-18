using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.GameCommons;
using Charlotte.Commons;
using Charlotte.Games;

namespace Charlotte.LevelEditors
{
	/// <summary>
	/// 編集モードに関する機能
	/// </summary>
	public static class LevelEditor
	{
		public enum Mode_e
		{
			TILE,
			ENEMY,
		}

		public static LevelEditorDlg Dlg = null;

		public static void ShowDialog()
		{
			if (Dlg != null)
				throw null; // never

			Dlg = new LevelEditorDlg();
			Dlg.Show();
		}

		public static void CloseDialog()
		{
			Dlg.Close();
			Dlg.Dispose();
			Dlg = null;
		}

		public static void DrawEnemy()
		{
			int cam_l = DDGround.ICamera.X;
			int cam_t = DDGround.ICamera.Y;
			int cam_r = cam_l + DDConsts.Screen_W;
			int cam_b = cam_t + DDConsts.Screen_H;

			I2Point lt = GameCommon.ToTablePoint(cam_l, cam_t);
			I2Point rb = GameCommon.ToTablePoint(cam_r, cam_b);

			for (int x = lt.X; x <= rb.X; x++)
			{
				for (int y = lt.Y; y <= rb.Y; y++)
				{
					MapCell cell = Game.I.Map.GetCell(x, y);

					if (cell.EnemyName != GameConsts.ENEMY_NONE)
					{
						int tileL = x * GameConsts.TILE_W;
						int tileT = y * GameConsts.TILE_H;

						DDDraw.SetAlpha(0.3);
						DDDraw.SetBright(new I3Color(0, 128, 255));
						DDDraw.DrawRect(
							Ground.I.Picture.WhiteBox,
							tileL - cam_l,
							tileT - cam_t,
							GameConsts.TILE_W,
							GameConsts.TILE_H
							);
						DDDraw.Reset();

						DDPrint.SetBorder(new I3Color(0, 128, 255));
						DDPrint.SetDebug(tileL - cam_l, tileT - cam_t);
						DDPrint.Print(cell.EnemyName);
						DDPrint.Reset();
					}
				}
			}
		}
	}
}
