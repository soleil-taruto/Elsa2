using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Commons;
using Charlotte.GameCommons;

namespace Charlotte.Games.Enemies.Events
{
	public class Enemy_Event9004 : Enemy
	{
		public Enemy_Event9004(D2Point pos)
			: base(pos)
		{ }

		public override void Draw()
		{
			if (DDUtils.GetDistance(new D2Point(this.X, this.Y), new D2Point(Game.I.Player.X, Game.I.Player.Y)) < 50.0)
			{
				if (!Game.I.FZEvent9004_Actived)
				{
					Game.I.FZEvent9004_Actived = true;

					// none ???

					//DDCurtain.SetCurtain(50, -1.0);
				}
			}
		}

		public override Enemy GetClone()
		{
			return new Enemy_Event9004(new D2Point(this.X, this.Y));
		}
	}
}
