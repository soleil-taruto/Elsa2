﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.GameCommons;

namespace Charlotte.Games.Tiles
{
	public class Tile_Waterfall_Front : Tile
	{
		/// <summary>
		/// Ground.I.Picture.Stage02_Chip_f0x の [0]～[3] を想定する。
		/// </summary>
		private DDPicture[] Pictures;

		public Tile_Waterfall_Front(DDPicture[] pictures)
		{
			this.Pictures = pictures;
		}

		public override Kind_e GetKind()
		{
			return Kind_e.SPACE;
		}

		public override void Draw(double draw_x, double draw_y, int map_x, int map_y)
		{
			DDGround.EL.Add(() =>
			{
				DDDraw.DrawCenter(this.Pictures[DDEngine.ProcFrame / 4 % 4], draw_x, draw_y);
				return false;
			});
		}
	}
}
