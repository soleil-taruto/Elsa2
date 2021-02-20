using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Commons;
using Charlotte.Games;
using Charlotte.GameCommons;

namespace Charlotte
{
	public class ResourcePicture2
	{
		//public DDPicture[,] Dummy = DDDerivations.GetAnimation(Ground.I.Picture.Dummy, 0, 0, 25, 25, 2, 2);

		public DDPicture[,] TitleMenuItem = DDDerivations.GetAnimation(Ground.I.Picture.TitleMenuItem, 0, 0, 180, 50);

		public DDPicture[,] Player_00 = DDDerivations.GetAnimation(Ground.I.Picture.Player_00, 0, 0, 32, 32);
		public DDPicture[,] Player_01 = DDDerivations.GetAnimation(Ground.I.Picture.Player_01, 0, 0, 32, 32);
		public DDPicture[,] Player_02 = DDDerivations.GetAnimation(Ground.I.Picture.Player_02, 0, 0, 32, 32);
		public DDPicture[,] Player_03 = DDDerivations.GetAnimation(Ground.I.Picture.Player_03, 0, 0, 32, 32);
		public DDPicture[,] Player_05 = DDDerivations.GetAnimation(Ground.I.Picture.Player_05, 0, 0, 32, 32);
		public DDPicture[,] PlayerFace_00 = DDDerivations.GetAnimation(Ground.I.Picture.PlayerFace_00, 0, 0, 96, 96);
		public DDPicture[,] PlayerFace_01 = DDDerivations.GetAnimation(Ground.I.Picture.PlayerFace_01, 0, 0, 96, 96);

		public DDPicture[,] Tile_A1 = DDDerivations.GetAnimation(Ground.I.Picture.Tile_A1, 0, 0, GameConsts.TILE_W, GameConsts.TILE_H);
		public DDPicture[,] Tile_A2 = DDDerivations.GetAnimation(Ground.I.Picture.Tile_A2, 0, 0, GameConsts.TILE_W, GameConsts.TILE_H);
		public DDPicture[,] Tile_A3 = DDDerivations.GetAnimation(Ground.I.Picture.Tile_A3, 0, 0, GameConsts.TILE_W, GameConsts.TILE_H);
		public DDPicture[,] Tile_A4 = DDDerivations.GetAnimation(Ground.I.Picture.Tile_A4, 0, 0, GameConsts.TILE_W, GameConsts.TILE_H);
		public DDPicture[,] Tile_A5 = DDDerivations.GetAnimation(Ground.I.Picture.Tile_A5, 0, 0, GameConsts.TILE_W, GameConsts.TILE_H);
		public DDPicture[,] Tile_B = DDDerivations.GetAnimation(Ground.I.Picture.Tile_B, 0, 0, GameConsts.TILE_W, GameConsts.TILE_H);
		public DDPicture[,] Tile_C = DDDerivations.GetAnimation(Ground.I.Picture.Tile_C, 0, 0, GameConsts.TILE_W, GameConsts.TILE_H);
		public DDPicture[,] Tile_D = DDDerivations.GetAnimation(Ground.I.Picture.Tile_D, 0, 0, GameConsts.TILE_W, GameConsts.TILE_H);
		public DDPicture[,] Tile_E = DDDerivations.GetAnimation(Ground.I.Picture.Tile_E, 0, 0, GameConsts.TILE_W, GameConsts.TILE_H);

		public DDPicture[,] MiniTile_A1 = DDDerivations.GetAnimation(Ground.I.Picture.Tile_A1, 0, 0, GameConsts.MINI_TILE_W, GameConsts.MINI_TILE_H);
		public DDPicture[,] MiniTile_A2 = DDDerivations.GetAnimation(Ground.I.Picture.Tile_A2, 0, 0, GameConsts.MINI_TILE_W, GameConsts.MINI_TILE_H);
		public DDPicture[,] MiniTile_A3 = DDDerivations.GetAnimation(Ground.I.Picture.Tile_A3, 0, 0, GameConsts.MINI_TILE_W, GameConsts.MINI_TILE_H);
		public DDPicture[,] MiniTile_A4 = DDDerivations.GetAnimation(Ground.I.Picture.Tile_A4, 0, 0, GameConsts.MINI_TILE_W, GameConsts.MINI_TILE_H);
		public DDPicture[,] MiniTile_A5 = DDDerivations.GetAnimation(Ground.I.Picture.Tile_A5, 0, 0, GameConsts.MINI_TILE_W, GameConsts.MINI_TILE_H);
		public DDPicture[,] MiniTile_B = DDDerivations.GetAnimation(Ground.I.Picture.Tile_B, 0, 0, GameConsts.MINI_TILE_W, GameConsts.MINI_TILE_H);
		public DDPicture[,] MiniTile_C = DDDerivations.GetAnimation(Ground.I.Picture.Tile_C, 0, 0, GameConsts.MINI_TILE_W, GameConsts.MINI_TILE_H);
		public DDPicture[,] MiniTile_D = DDDerivations.GetAnimation(Ground.I.Picture.Tile_D, 0, 0, GameConsts.MINI_TILE_W, GameConsts.MINI_TILE_H);
		public DDPicture[,] MiniTile_E = DDDerivations.GetAnimation(Ground.I.Picture.Tile_E, 0, 0, GameConsts.MINI_TILE_W, GameConsts.MINI_TILE_H);

		public DDPicture[,] IconSet = DDDerivations.GetAnimation(Ground.I.Picture.IconSet, 0, 0, 24, 24);

		public DDPicture[] Enemy_神奈子 = DDDerivations.GetAnimation_YX(Ground.I.Picture.Enemy_神奈子, 0, 0, 250, 250).ToArray();
	}
}
