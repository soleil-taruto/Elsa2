﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.GameCommons;
using Charlotte.Games.Shots;

namespace Charlotte.Games.Attacks
{
	public class Attack_Tewi_ジャンプ強攻撃 : Attack
	{
		public override bool IsInvincibleMode()
		{
			return false;
		}

		protected override IEnumerable<bool> E_Draw()
		{
			for (int frame = 0; ; frame++)
			{
				if (DDInput.A.GetInput() == 1) // ? ジャンプ押下
					break;

				int koma = frame;
				//int koma = frame / 2;
				//int koma = frame / 3;

				koma += 2; // 最初の2コマを飛ばす。
				if (6 <= koma) // koma == 6 以降は(1コマ/2フレーム)
				{
					koma -= 6;
					koma /= 2;
					koma += 6;
				}
				if (8 <= koma) koma -= 2; // koma == 6, 7 の 2 回目
				if (8 <= koma) koma -= 2; // koma == 6, 7 の 3 回目
				if (8 <= koma) koma -= 2; // koma == 6, 7 の 4 回目

				if (Ground.I.Picture2.Tewi_ジャンプ強攻撃.Length <= koma)
					break;

				double x = Game.I.Player.X;
				double y = Game.I.Player.Y;
				double xZoom = Game.I.Player.FacingLeft ? -1.0 : 1.0;
				bool facingLeft = Game.I.Player.FacingLeft;

				if (frame == 0)
				{
					// TODO: 当たり判定設定
				}

				AttackCommon.ProcPlayer_移動();
				AttackCommon.ProcPlayer_Fall();

				AttackCommon.ProcPlayer_側面();
				AttackCommon.ProcPlayer_脳天();

				if (AttackCommon.ProcPlayer_接地())
					break;

				DDDraw.SetTaskList(Game.I.Player.Draw_EL);
				DDDraw.DrawBegin(
					Ground.I.Picture2.Tewi_ジャンプ強攻撃[koma],
					x - DDGround.ICamera.X,
					y - DDGround.ICamera.Y
					);
				DDDraw.DrawZoom_X(xZoom);
				DDDraw.DrawEnd();
				DDDraw.Reset();

				yield return true;
			}
		}
	}
}
