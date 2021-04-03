using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Commons;
using Charlotte.GameCommons;

namespace Charlotte.Games.Enemies.Tests
{
	public class Enemy_BItem : Enemy
	{
		public enum 効用_e
		{
			ZANKI_UP,
			BOMB_ADD,
			POWER_UP_WEAPON,
		}

		public static string[] 効用_e_Names = new string[]
		{
			"\u2605\u2605\u2605 \u6b8b \u6a5f \u8ffd \u52a0 \u2605\u2605\u2605",
			"\u2606\u2606\u2606 \u30dc \u30e0 \u8ffd \u52a0 \u2606\u2606\u2606",
			"\u6b66\u5668\u30d1\u30ef\u30fc\u30a2\u30c3\u30d7",
		};

		private 効用_e 効用;

		public Enemy_BItem(double x, double y, 効用_e 効用)
			: base(x, y, 0, Kind_e.アイテム)
		{
			this.効用 = 効用;
		}

		protected override IEnumerable<bool> E_Draw()
		{
			double xSpeed = 1.0;

			for (; ; )
			{
				xSpeed -= 0.1;
				this.X += xSpeed;

				if (DDUtils.GetDistance(new D2Point(Game.I.Player.X, Game.I.Player.Y), new D2Point(this.X, this.Y)) < 60.0)
				{
					this.プレイヤーがアイテムを取得した();
					break;
				}

				DDDraw.DrawBegin(Ground.I.Picture.Dummy, this.X, this.Y);
				DDDraw.DrawRotate(DDEngine.ProcFrame / 10.0);
				DDDraw.DrawEnd();

				DDPrint.SetDebug((int)this.X, (int)this.Y);
				DDPrint.SetBorder(new I3Color(0, 0, 0));
				DDPrint.PrintLine("\u30a2\u30a4\u30c6\u30e0");
				DDPrint.PrintLine("\u52b9\u7528\uff1a" + \u52b9\u7528_e_Names[(int)this.\u52b9\u7528]);
				DDPrint.Reset();

				// 当たり判定無し

				yield return true;
			}
		}

		private void プレイヤーがアイテムを取得した()
		{
			switch (this.効用)
			{
				case 効用_e.ZANKI_UP:
					Game.I.Status.Zanki++;
					break;

				case 効用_e.BOMB_ADD:
					Game.I.Status.ZanBomb++;
					break;

				case 効用_e.POWER_UP_WEAPON:
					Game.I.Player.AttackLevel = Math.Min(Game.I.Player.AttackLevel + 1, Player.ATTACK_LEVEL_MAX);
					break;

				default:
					throw null; // never
			}
		}
	}
}
