using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Commons;
using Charlotte.GameCommons;
using Charlotte.Games.Designs;

namespace Charlotte.Games.Enemies.Events
{
	public class Enemy_Event9003B : Enemy
	{
		public Enemy_Event9003B(D2Point pos)
			: base(pos)
		{ }

		public override void Draw()
		{
			if (DDUtils.GetDistance(new D2Point(this.X, this.Y), new D2Point(Game.I.Player.X, Game.I.Player.Y)) < 50.0)
			{
				if (!Game.I.FZEvent9003B_Actived)
				{
					Game.I.FZEvent9003B_Actived = true;

					Game.I.Map.Design = new Design_0003();

					DDGround.EL.Add(SCommon.Supplier(this.E_フラッシュ()));
				}
			}
		}

		private IEnumerable<bool> E_フラッシュ()
		{
			foreach (DDScene scene in DDSceneUtils.Create(60))
			{
				DDCurtain.DrawCurtain((1.0 - scene.Rate) * 0.5);
				yield return true;
			}
		}

		public override Enemy GetClone()
		{
			return new Enemy_Event9003B(new D2Point(this.X, this.Y));
		}
	}
}
