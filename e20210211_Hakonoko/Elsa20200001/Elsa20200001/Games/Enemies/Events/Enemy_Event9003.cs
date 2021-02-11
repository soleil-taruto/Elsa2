﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Commons;
using Charlotte.GameCommons;
using Charlotte.Games.Designs;

namespace Charlotte.Games.Enemies.Events
{
	public class Enemy_Event9003 : Enemy
	{
		public Enemy_Event9003(D2Point pos)
			: base(pos)
		{ }

		public override void Draw()
		{
			if (DDUtils.GetDistance(new D2Point(this.X, this.Y), new D2Point(Game.I.Player.X, Game.I.Player.Y)) < 50.0)
			{
				if (!Game.I.FZEvent9003_Actived)
				{
					Game.I.FZEvent9003_Actived = true;

					for (int x = 0; x < Game.I.Map.W; x++)
					{
						for (int y = 0; y < Game.I.Map.H; y++)
						{
							MapCell cell = Game.I.Map.GetCell(x, y);

							if (
								cell.Kind == MapCell.Kind_e.DEATH ||
								cell.Kind == MapCell.Kind_e.GOAL
								)
								cell.Kind = MapCell.Kind_e.WALL;
						}
					}
#if true
					Game.I.Enemies.RemoveAll(enemy => !(
						enemy is Enemy_Event9003B ||
						enemy is Enemy_Event9004 ||
						enemy is Enemy_Event9005
						));
#elif true // ng
					Game.I.Enemies.RemoveAll(enemy =>
						enemy is Enemy_Death ||
						enemy is Enemy_Meteor ||
						enemy is Enemy_MeteorLoader
						);
#elif true // ng
					Game.I.Enemies.Clear();
#else // ng
					// MeteorLoader に動かれるとマズい。なので Clear した方が手っ取り早い。
					// -- memo: DeadFlag による除去に統一する必要は無い。
					//
					foreach (Enemy enemy in Game.I.Enemies.Iterate())
						enemy.DeadFlag = true;
#endif

					Game.I.Map.Design = new Design_0002();

					DDGround.EL.Add(SCommon.Supplier(this.E_フラッシュ()));
				}
			}
		}

		private IEnumerable<bool> E_フラッシュ()
		{
			foreach (DDScene scene in DDSceneUtils.Create(40))
			{
				DDCurtain.DrawCurtain((1.0 - scene.Rate) * 0.5);
				yield return true;
			}
		}

		public override Enemy GetClone()
		{
			return new Enemy_Event9003(new D2Point(this.X, this.Y));
		}
	}
}
