﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.GameCommons;
using Charlotte.Commons;
using Charlotte.Games.Enemies;
using Charlotte.Games.Shots;
using Charlotte.Games.Attacks;

namespace Charlotte.Games
{
	/// <summary>
	/// プレイヤーに関する情報と機能
	/// 唯一のインスタンスを Game.I.Player に保持する。
	/// </summary>
	public class Player
	{
		public enum Chara_e
		{
			TEWI,
			CIRNO,
		}

		public static string GetName(Chara_e chara)
		{
			return new string[]
			{
				"因幡てゐ",
				"チルノ",
			}
			[(int)chara];
		}

		public Chara_e Chara;
		public double X;
		public double Y;
		public double YSpeed;
		public bool FacingLeft;
		public int MoveFrame;
		public bool MoveSlow; // ? 低速移動
		public int JumpFrame;
		public int JumpCount;
		public int AirborneFrame; // 0 == 接地状態, 1～ == 滞空状態
		public int ShagamiFrame; // 0 == 無効, 1～ == しゃがみ中
		public int DeadFrame = 0; // 0 == 無効, 1～ == 死亡中
		public int DamageFrame = 0; // 0 == 無効, 1～ == ダメージ中
		public int InvincibleFrame = 0; // 0 == 無効, 1～ == 無敵時間中
		public int HP = 1; // -1 == 死亡, 1～ == 生存

		public int AirborneJumpFrame;

		/// <summary>
		/// プレイヤーの攻撃モーション
		/// null の場合は無効
		/// null ではない場合 Attack.EachFrame() が実行される代わりに、プレイヤーの入力・被弾処理などは実行されない。
		/// </summary>
		public Attack Attack = null;

		/// <summary>
		/// プレイヤー描画の代替タスクリスト
		/// 空の場合は無効
		/// 空ではない場合 Draw_EL.ExecuteAllTask() が実行される代わりに Draw() の主たる処理は実行されない。
		/// --- プレイヤーの攻撃モーションから使用されることを想定する。
		/// </summary>
		public DDTaskList Draw_EL = new DDTaskList();

		public void Draw()
		{
			if (1 <= this.Draw_EL.Count)
			{
				this.Draw_EL.ExecuteAllTask();
				return;
			}

			double xZoom = this.FacingLeft ? -1 : 1;
			double yZoom = 1.0;
			DDPicture picture = null;
			double xa = 0.0;
			double ya = 0.0;

			switch (Game.I.Player.Chara) // キャラクタ別_各種モーション
			{
				case Chara_e.TEWI:
					{
						if (1 <= Game.I.Player.ShagamiFrame)
						{
							picture = Ground.I.Picture2.Tewi_しゃがみ[Math.Min(Game.I.Player.ShagamiFrame / 3, Ground.I.Picture2.Tewi_しゃがみ.Length - 1)];
							xa = 14;
							ya = 6;
						}
						else if (Game.I.Player.AirborneFrame != 0) // ? 滞空状態
						{
							int koma = Game.I.Player.AirborneJumpFrame / 3;

							if (0.0 < Game.I.Player.YSpeed && koma < Ground.I.Picture2.Tewi_ジャンプ_上昇.Length) // ? 下降中
								Game.I.Player.AirborneJumpFrame = Ground.I.Picture2.Tewi_ジャンプ_上昇.Length * 3;

							if (koma < Ground.I.Picture2.Tewi_ジャンプ_上昇.Length)
							{
								picture = Ground.I.Picture2.Tewi_ジャンプ_上昇[koma];
							}
							else
							{
								koma -= Ground.I.Picture2.Tewi_ジャンプ_上昇.Length;
								koma %= Ground.I.Picture2.Tewi_ジャンプ_下降.Length;

								picture = Ground.I.Picture2.Tewi_ジャンプ_下降[koma];
							}
						}
						else if (1 <= this.MoveFrame)
						{
							if (this.MoveSlow)
							{
								picture = Ground.I.Picture2.Tewi_歩く[Game.I.Frame / 10 % Ground.I.Picture2.Tewi_歩く.Length];
							}
							else
							{
								picture = Ground.I.Picture2.Tewi_走る[Game.I.Frame / 5 % Ground.I.Picture2.Tewi_走る.Length];
							}
						}
						else
						{
							picture = Ground.I.Picture2.Tewi_立ち[Game.I.Frame / 10 % Ground.I.Picture2.Tewi_立ち.Length];
						}
					}
					break;

				case Chara_e.CIRNO:
					{
						if (1 <= Game.I.Player.ShagamiFrame)
						{
							////picture = Ground.I.Picture2.さやか立ち[0];
							xa = -14;
							ya = 20;
							yZoom = 0.666;
						}
						else if (Game.I.Player.AirborneFrame != 0) // ? 滞空状態
						{
							if (1 <= this.MoveFrame)
							{
								////picture = Ground.I.Picture2.さやか走り[1];
								ya = -4;
							}
							else
							{
								////picture = Ground.I.Picture2.さやか突き[9];
								xa = 30;
							}
						}
						else if (1 <= this.MoveFrame)
						{
							if (this.MoveSlow)
							{
								////picture = Ground.I.Picture2.さやか走り[(Game.I.Frame / 10) % Ground.I.Picture2.さやか走り.Length];
								ya = -4;
							}
							else
							{
								////picture = Ground.I.Picture2.さやか走り[(Game.I.Frame / 5) % Ground.I.Picture2.さやか走り.Length];
								ya = -4;
							}
						}
						else
						{
							////picture = Ground.I.Picture2.さやか立ち[(Game.I.Frame / 10) % Ground.I.Picture2.さやか立ち.Length];
							xa = -14;
							ya = 2;
						}
					}
					break;

				default:
					throw null; // never
			}
			if (1 <= this.DeadFrame) // 死亡モーション
			{
				// 注意：this.DeadFrame == 0 ～ Consts.PLAYER_DEAD_FRAME_MAX + 2

				switch (Game.I.Player.Chara)
				{
					case Chara_e.TEWI:
						////picture = Ground.I.Picture2.ほむら死亡[Math.Min(this.DeadFrame / 6, Ground.I.Picture2.ほむら死亡.Length - 1)];
						ya = 6;
						break;

					case Chara_e.CIRNO:
						////picture = Ground.I.Picture2.さやか死亡[Math.Min(this.DeadFrame / 6, Ground.I.Picture2.さやか死亡.Length - 1)];
						xa = -10;
						ya = -6;
						break;

					default:
						throw null; // never
				}
			}
			if (1 <= this.DamageFrame) // 被弾モーション
			{
				// 注意：this.DamageFrame == 0 ～ Consts.PLAYER_DAMAGE_FRAME_MAX + 2

				switch (Game.I.Player.Chara)
				{
					case Chara_e.TEWI:
						////picture = Ground.I.Picture2.ほむら被弾[(this.DamageFrame * Ground.I.Picture2.ほむら被弾.Length) / (GameConsts.PLAYER_DAMAGE_FRAME_MAX + 3)];
						break;

					case Chara_e.CIRNO:
						////picture = Ground.I.Picture2.さやか被弾[(this.DamageFrame * Ground.I.Picture2.さやか被弾.Length) / (GameConsts.PLAYER_DAMAGE_FRAME_MAX + 3)];
						xa = -20;
						break;

					default:
						throw null; // never
				}
			}
			if (1 <= this.DamageFrame || 1 <= this.InvincibleFrame)
			{
				DDDraw.SetTaskList(DDGround.EL);
			}
			if (1 <= this.InvincibleFrame)
			{
				DDDraw.SetAlpha(0.5);
			}

			double x = this.X;
			double y = this.Y;

			DDDraw.DrawBegin(
				picture,
				x - DDGround.ICamera.X + (xa * xZoom),
				y - DDGround.ICamera.Y + ya
				);
			DDDraw.DrawZoom_X(xZoom);
			DDDraw.DrawZoom_Y(yZoom);
			DDDraw.DrawEnd();
			DDDraw.Reset();
		}
	}
}
