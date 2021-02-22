﻿using System;
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

		public static void DrawTiles()
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
					I3Color color = new I3Color(0, 0, 0);
					string name = "";

					switch (cell.Kind)
					{
						case MapCell.Kind_e.EMPTY:
							goto endDraw;

						case MapCell.Kind_e.START:
							color = new I3Color(0, 255, 0);
							break;

						case MapCell.Kind_e.GOAL:
							color = new I3Color(0, 255, 255);
							break;

						case MapCell.Kind_e.WALL:
							color = new I3Color(255, 255, 0);
							break;

						case MapCell.Kind_e.DEATH:
							color = new I3Color(255, 0, 0);
							break;

						default:
							color = new I3Color(128, 128, 255);
							name = MapCell.Kine_e_Names[(int)cell.Kind];

							if (name.Contains(':'))
								name = name.Substring(0, name.IndexOf(':'));

							break;
					}

					{
						int tileL = x * GameConsts.TILE_W;
						int tileT = y * GameConsts.TILE_H;

						DDDraw.SetBright(color);
						DDDraw.DrawRect(
							Ground.I.Picture.WhiteBox,
							tileL - cam_l,
							tileT - cam_t,
							GameConsts.TILE_W,
							GameConsts.TILE_H
							);
						DDDraw.Reset();

						DDDraw.SetAlpha(0.1);
						DDDraw.SetBright(new I3Color(0, 0, 0));
						DDDraw.DrawRect(
							Ground.I.Picture.WhiteBox,
							tileL - cam_l,
							tileT - cam_t,
							GameConsts.TILE_W,
							GameConsts.TILE_H / 2
							);
						DDDraw.Reset();

						DDDraw.SetAlpha(0.2);
						DDDraw.SetBright(new I3Color(0, 0, 0));
						DDDraw.DrawRect(
							Ground.I.Picture.WhiteBox,
							tileL - cam_l,
							tileT - cam_t,
							GameConsts.TILE_W / 2,
							GameConsts.TILE_H
							);
						DDDraw.Reset();

						DDGround.EL.Add(() =>
						{
							DDPrint.SetBorder(new I3Color(0, 64, 64));
							DDPrint.SetPrint(tileL - cam_l, tileT - cam_t);
							DDPrint.Print(name);
							DDPrint.Reset();

							return false;
						});
					}

				endDraw:
					;
				}
			}
		}
	}
}
