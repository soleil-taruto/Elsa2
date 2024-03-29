﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DxLibDLL;
using Charlotte.Commons;
using Charlotte.GameCommons;
using Charlotte.Games.Attacks;
using Charlotte.Games.Enemies;
using Charlotte.Games.Shots;
using Charlotte.Games.Tiles;
using Charlotte.Games.Walls;
using Charlotte.LevelEditors;

namespace Charlotte.Games
{
	public class Game : IDisposable
	{
		public World World;
		public GameStatus Status;

		// <---- prm

		public static Game I;

		public Game()
		{
			I = this;
		}

		public void Dispose()
		{
			I = null;
		}

		public Player Player = new Player();

		public Map Map;
		private Wall Wall;

		private bool CamSlideMode; // ? カメラ・スライド_モード中
		private bool CamSlided;
		public int CamSlideX; // -1 ～ 1
		public int CamSlideY; // -1 ～ 1

		public int Frame;
		public bool UserInputDisabled = false;

		public bool RequestReturnToTitleMenu = false;

		public void Perform()
		{
			Func<bool> f_ゴミ回収 = SCommon.Supplier(this.E_ゴミ回収());

			this.Map = new Map(GameCommon.GetMapFile(this.World.GetCurrMapName()));
			this.ReloadEnemies();

			// デフォルトの「プレイヤーのスタート地点」
			// -- マップの中央
			this.Player.X = this.Map.W * GameConsts.TILE_W / 2.0;
			this.Player.Y = this.Map.H * GameConsts.TILE_H / 2.0;

			{
				Enemy enemy = this.Enemies.Iterate().FirstOrDefault(v => v is Enemy_スタート地点 && ((Enemy_スタート地点)v).Direction == this.Status.StartPointDirection);

				if (enemy != null)
				{
					this.Player.X = enemy.X;
					this.Player.Y = enemy.Y;
				}
			}

			// ★★★★★
			// プレイヤー・ステータス反映(マップ入場時)
			// その他の反映箇所：
			// -- マップ退場時
			// -- セーブ時
			{
				this.Player.Chara = this.Status.StartChara;
				this.Player.HP = this.Status.StartHP;
				this.Player.FacingLeft = this.Status.StartFacingLeft;
			}

			this.Wall = WallCreator.Create(this.Map.WallName);

			MusicCollection.Get(this.Map.MusicName).Play();

			DDGround.Camera.X = this.Player.X - DDConsts.Screen_W / 2.0;
			DDGround.Camera.Y = this.Player.Y - DDConsts.Screen_H / 2.0;

			DDCurtain.SetCurtain(0, -1.0);
			DDCurtain.SetCurtain(10);

			DDEngine.FreezeInput();

			for (this.Frame = 0; ; this.Frame++)
			{
				// Attack_ほむらシールド 終了から Shot_ほむらシールド の PlayerTracer.Start 実行の間に
				// ポーズできるタイミングは無いはずだけど、曲芸的で気持ち悪い。

				if (
					!this.UserInputDisabled &&
					//Game.I.Player.Attack == null && // ? プレイヤーの攻撃モーション中ではない。// モーション中でも良いはず！
					DDInput.PAUSE.GetInput() == 1
					)
				{
					this.Pause();

					if (this.Pause_ReturnToTitleMenu)
					{
						this.Status.ExitDirection = 5;
						break;
					}
				}
				if (this.RequestReturnToTitleMenu)
				{
					this.Status.ExitDirection = 5;
					break;
				}
				if (DDConfig.LOG_ENABLED && DDKey.GetInput(DX.KEY_INPUT_RETURN) == 1)
				{
					this.DebugPause();
				}

				// 死亡時にカメラ移動を止める。
				//if (this.Player.DeadFrame == 0)
				//    this.カメラ位置調整(false);

				this.カメラ位置調整(false);

				if (DDConfig.LOG_ENABLED && DDKey.GetInput(DX.KEY_INPUT_E) == 1) // エディットモード(デバッグ用)
				{
					this.Edit();
					this.ReloadEnemies();
					this.Frame = 0;
				}

				if (this.Player.Attack != null) // プレイヤー攻撃中
				{
					if (this.Player.Attack.EachFrame()) // ? このプレイヤー攻撃を継続する。
						goto endPlayer;

					this.Player.Attack = null; // プレイヤー攻撃_終了
				}

				// プレイヤー入力
				{
					bool deadOrDamageOrUID = 1 <= this.Player.DeadFrame || 1 <= this.Player.DamageFrame || this.UserInputDisabled;
					bool move = false;
					bool slow = false;
					bool camSlide = false;
					int jump = 0;
					bool shagami = false;
					int attack = 0;
					int extendedAttack = 0;

					if (!deadOrDamageOrUID && 1 <= DDInput.DIR_2.GetInput())
					{
						shagami = true;
					}

					// 入力抑止中であるか否かに関わらず左右の入力は受け付ける様にする。
					int freezeInputFrameBackup = DDEngine.FreezeInputFrame;
					DDEngine.FreezeInputFrame = 0;

					if (!deadOrDamageOrUID && 1 <= DDInput.DIR_4.GetInput())
					{
						this.Player.FacingLeft = true;
						move = true;
					}
					if (!deadOrDamageOrUID && 1 <= DDInput.DIR_6.GetInput())
					{
						this.Player.FacingLeft = false;
						move = true;
					}

					DDEngine.FreezeInputFrame = freezeInputFrameBackup; // restore

					if (1 <= DDInput.L.GetInput())
					{
						move = false;
						camSlide = true;
					}
					if (!deadOrDamageOrUID && 1 <= DDInput.R.GetInput())
					{
						slow = true;
					}
					if (!deadOrDamageOrUID && 1 <= DDInput.A.GetInput())
					{
						jump = DDInput.A.GetInput();
					}
					if (!deadOrDamageOrUID && 1 <= DDInput.B.GetInput())
					{
						attack = DDInput.B.GetInput();
					}
					if (!deadOrDamageOrUID && 1 <= DDInput.C.GetInput())
					{
						extendedAttack = DDInput.C.GetInput();
					}

					if (move)
					{
						this.Player.MoveFrame++;
						shagami = false;
					}
					else
						this.Player.MoveFrame = 0;

					this.Player.MoveSlow = move && slow;

					if (1 <= this.Player.JumpFrame)
					{
						if (1 <= jump)
						{
							this.Player.JumpFrame++;
						}
						else
						{
							// ★ ジャンプを中断・終了した。

							this.Player.JumpFrame = 0;

							if (this.Player.YSpeed < 0.0)
								this.Player.YSpeed /= 2.0;
						}
					}
					else
					{
						// 事前入力 == 着地前の数フレーム間にジャンプボタンを押し始めてもジャンプできるようにする。
						// 入力猶予 == 落下(地面から離れた)直後の数フレーム間にジャンプボタンを押し始めてもジャンプできるようにする。

						const int 事前入力時間 = 5;
						const int 入力猶予時間 = 10;

						if (this.Player.AirborneFrame < 入力猶予時間) // ? 接地状態からのジャンプが可能な状態
						{
							if (1 <= jump && jump < 事前入力時間)
							{
								// ★ ジャンプを開始した。

								this.Player.JumpFrame = 1;
								this.Player.JumpCount = 1;

								this.Player.YSpeed = GameConsts.PLAYER_ジャンプ初速度;
							}
							else
							{
								this.Player.JumpCount = 0;
							}
						}
						else // ? 接地状態からのジャンプが「可能ではない」状態
						{
							// 滞空状態に入ったら「通常ジャンプの状態」にする。
							if (this.Player.JumpCount < 1)
								this.Player.JumpCount = 1;

							if (1 <= jump && jump < 事前入力時間 && this.Player.JumpCount < GameConsts.JUMP_MAX)
							{
								// ★ 空中(n-段)ジャンプを開始した。

								this.Player.JumpFrame = 1;
								this.Player.JumpCount++;

								this.Player.YSpeed = GameConsts.PLAYER_ジャンプ初速度;

								DDGround.EL.Add(SCommon.Supplier(Effects.空中ジャンプの足場(this.Player.X, this.Player.Y + 48)));
							}
							else
							{
								// noop
							}
						}
					}

					if (camSlide)
					{
						if (DDInput.DIR_4.IsPound())
						{
							this.CamSlided = true;
							this.CamSlideX--;
						}
						if (DDInput.DIR_6.IsPound())
						{
							this.CamSlided = true;
							this.CamSlideX++;
						}
						if (DDInput.DIR_8.IsPound())
						{
							this.CamSlided = true;
							this.CamSlideY--;
						}
						if (DDInput.DIR_2.IsPound())
						{
							this.CamSlided = true;
							this.CamSlideY++;
						}
						DDUtils.ToRange(ref this.CamSlideX, -1, 1);
						DDUtils.ToRange(ref this.CamSlideY, -1, 1);
					}
					else
					{
						if (this.CamSlideMode && !this.CamSlided)
						{
							this.CamSlideX = 0;
							this.CamSlideY = 0;
						}
						this.CamSlided = false;
					}
					this.CamSlideMode = camSlide;

					if (this.Player.AirborneFrame != 0) // ? 滞空状態
						shagami = false;

					if (shagami)
						this.Player.ShagamiFrame++;
					else
						this.Player.ShagamiFrame = 0;

					{
						const int 事前入力時間 = 2; // 無効
						//const int 事前入力時間 = 5;
						//const int 事前入力時間 = 10; // HACK: ちょっと長すぎるかもしれない。無効でも良いかもしれない。// 暴発があるので事前入力は無効にする。

						if (1 <= attack && attack < 事前入力時間)
						{
							switch (this.Player.Chara)
							{
								case Player.Chara_e.HOMURA:
									{
										if (this.Player.AirborneFrame == 0)
											this.Player.Attack = new Attack_ほむら接地攻撃();
										else
											this.Player.Attack = new Attack_ほむら滞空攻撃();
									}
									break;

								case Player.Chara_e.SAYAKA:
									{
										if (this.Player.AirborneFrame == 0)
											this.Player.Attack = new Attack_さやか接地攻撃();
										else
											this.Player.Attack = new Attack_さやか滞空攻撃();
									}
									break;

								default:
									throw null; // never
							}
						}
						if (1 <= extendedAttack && extendedAttack < 事前入力時間)
						{
							switch (this.Player.Chara)
							{
								case Player.Chara_e.HOMURA:
									{
										if (this.Player.AirborneFrame == 0)
											this.Player.Attack = new Attack_ほむらシールド();
									}
									break;

								case Player.Chara_e.SAYAKA:
									{
										this.Player.Attack = new Attack_さやか突き();
									}
									break;

								default:
									throw null; // never
							}
						}
					}
				}

			startDead:
				if (1 <= this.Player.DeadFrame) // プレイヤー死亡中の処理
				{
					if (GameConsts.PLAYER_DEAD_FRAME_MAX < ++this.Player.DeadFrame)
					{
						this.Player.DeadFrame = 0;
						this.Status.ExitDirection = 5;
						break;
					}
					int frame = this.Player.DeadFrame; // 値域 == 2 ～ GameConsts.PLAYER_DEAD_FRAME_MAX
					double rate = DDUtils.RateAToB(2, GameConsts.PLAYER_DEAD_FRAME_MAX, frame);

					// ---- Dead

					// noop
				}
				//endDead:

				//startDamage:
				if (1 <= this.Player.DamageFrame) // プレイヤー・ダメージ中の処理
				{
					if (GameConsts.PLAYER_DAMAGE_FRAME_MAX < ++this.Player.DamageFrame)
					{
						this.Player.DamageFrame = 0;

						if (1 <= this.Player.HP)
						{
							this.Player.InvincibleFrame = 1;
							goto endDamage;
						}
						else
						{
							this.Player.DeadFrame = 1;
							goto startDead;
						}
					}
					int frame = this.Player.DamageFrame; // 値域 == 2 ～ GameConsts.PLAYER_DAMAGE_FRAME_MAX
					double rate = DDUtils.RateAToB(2, GameConsts.PLAYER_DAMAGE_FRAME_MAX, frame);

					// ---- Damage

					this.Player.X -= (9.0 - 6.0 * rate) * (this.Player.FacingLeft ? -1 : 1);
				}
			endDamage:

				//startInvincible:
				if (1 <= this.Player.InvincibleFrame) // プレイヤー無敵時間中の処理
				{
					if (GameConsts.PLAYER_INVINCIBLE_FRAME_MAX < ++this.Player.InvincibleFrame)
					{
						this.Player.InvincibleFrame = 0;
						goto endInvincible;
					}
					int frame = this.Player.InvincibleFrame; // 値域 == 2 ～ GameConsts.PLAYER_INVINCIBLE_FRAME_MAX
					double rate = DDUtils.RateAToB(2, GameConsts.PLAYER_INVINCIBLE_FRAME_MAX, frame);

					// ---- Invincible

					// noop
				}
			endInvincible:

				// プレイヤー移動
				{
					if (1 <= this.Player.MoveFrame)
					{
						double speed = 0.0;

						if (this.Player.MoveSlow)
						{
							speed = this.Player.MoveFrame / 10.0;
							DDUtils.Minim(ref speed, GameConsts.PLAYER_SLOW_SPEED);
						}
						else
							speed = GameConsts.PLAYER_SPEED;

						speed *= this.Player.FacingLeft ? -1 : 1;

						this.Player.X += speed;
					}
					else
						this.Player.X = (double)SCommon.ToInt(this.Player.X);

					// 重力による加速
					this.Player.YSpeed += GameConsts.PLAYER_GRAVITY;

					// 自由落下の最高速度を超えないように矯正
					DDUtils.Minim(ref this.Player.YSpeed, GameConsts.PLAYER_FALL_SPEED_MAX);

					// 自由落下
					this.Player.Y += this.Player.YSpeed;
				}

				// プレイヤー位置矯正
				{
					bool touchSide_L =
						this.Map.GetCell(GameCommon.ToTablePoint(this.Player.X - GameConsts.PLAYER_側面判定Pt_X, this.Player.Y - GameConsts.PLAYER_側面判定Pt_Y)).Tile.IsWall() ||
						this.Map.GetCell(GameCommon.ToTablePoint(this.Player.X - GameConsts.PLAYER_側面判定Pt_X, this.Player.Y)).Tile.IsWall() ||
						this.Map.GetCell(GameCommon.ToTablePoint(this.Player.X - GameConsts.PLAYER_側面判定Pt_X, this.Player.Y + GameConsts.PLAYER_側面判定Pt_Y)).Tile.IsWall();

					bool touchSide_R =
						this.Map.GetCell(GameCommon.ToTablePoint(this.Player.X + GameConsts.PLAYER_側面判定Pt_X, this.Player.Y - GameConsts.PLAYER_側面判定Pt_Y)).Tile.IsWall() ||
						this.Map.GetCell(GameCommon.ToTablePoint(this.Player.X + GameConsts.PLAYER_側面判定Pt_X, this.Player.Y)).Tile.IsWall() ||
						this.Map.GetCell(GameCommon.ToTablePoint(this.Player.X + GameConsts.PLAYER_側面判定Pt_X, this.Player.Y + GameConsts.PLAYER_側面判定Pt_Y)).Tile.IsWall();

					if (touchSide_L && touchSide_R) // -> 壁抜け防止のため再チェック
					{
						touchSide_L = this.Map.GetCell(GameCommon.ToTablePoint(this.Player.X - GameConsts.PLAYER_側面判定Pt_X, this.Player.Y)).Tile.IsWall();
						touchSide_R = this.Map.GetCell(GameCommon.ToTablePoint(this.Player.X + GameConsts.PLAYER_側面判定Pt_X, this.Player.Y)).Tile.IsWall();
					}

					if (touchSide_L && touchSide_R)
					{
						// noop
					}
					else if (touchSide_L)
					{
						this.Player.X = (double)SCommon.ToInt(this.Player.X / GameConsts.TILE_W) * GameConsts.TILE_W + GameConsts.PLAYER_側面判定Pt_X;
					}
					else if (touchSide_R)
					{
						this.Player.X = (double)SCommon.ToInt(this.Player.X / GameConsts.TILE_W) * GameConsts.TILE_W - GameConsts.PLAYER_側面判定Pt_X;
					}

					bool touchCeiling =
						this.Map.GetCell(GameCommon.ToTablePoint(this.Player.X - GameConsts.PLAYER_脳天判定Pt_X, this.Player.Y - GameConsts.PLAYER_脳天判定Pt_Y)).Tile.IsWall() ||
						this.Map.GetCell(GameCommon.ToTablePoint(this.Player.X + GameConsts.PLAYER_脳天判定Pt_X, this.Player.Y - GameConsts.PLAYER_脳天判定Pt_Y)).Tile.IsWall();

					if (touchCeiling)
					{
						if (this.Player.YSpeed < 0.0)
						{
							double plY = ((int)((this.Player.Y - GameConsts.PLAYER_脳天判定Pt_Y) / GameConsts.TILE_H) + 1) * GameConsts.TILE_H + GameConsts.PLAYER_脳天判定Pt_Y;

							this.Player.Y = plY;
							this.Player.YSpeed = 0.0;
						}
					}

					bool touchGround =
						this.Map.GetCell(GameCommon.ToTablePoint(this.Player.X - GameConsts.PLAYER_接地判定Pt_X, this.Player.Y + GameConsts.PLAYER_接地判定Pt_Y)).Tile.IsWall() ||
						this.Map.GetCell(GameCommon.ToTablePoint(this.Player.X + GameConsts.PLAYER_接地判定Pt_X, this.Player.Y + GameConsts.PLAYER_接地判定Pt_Y)).Tile.IsWall();

					if (touchGround)
					{
						if (0.0 < this.Player.YSpeed)
						{
							double plY = (int)((this.Player.Y + GameConsts.PLAYER_接地判定Pt_Y) / GameConsts.TILE_H) * GameConsts.TILE_H - GameConsts.PLAYER_接地判定Pt_Y;

							this.Player.Y = plY;
							this.Player.YSpeed = 0.0;
						}
					}

					if (touchGround)
						this.Player.AirborneFrame = 0;
					else
						this.Player.AirborneFrame++;
				}
			endPlayer: // Attack 合流点

				if (this.Player.X < 0.0) // ? マップの左側に出た。
				{
					this.Status.ExitDirection = 4;
					break;
				}
				if (this.Map.W * GameConsts.TILE_W < this.Player.X) // ? マップの右側に出た。
				{
					this.Status.ExitDirection = 6;
					break;
				}
				if (this.Player.Y < 0.0) // ? マップの上側に出た。
				{
					this.Status.ExitDirection = 8;
					break;
				}
				if (this.Map.H * GameConsts.TILE_H < this.Player.Y) // ? マップの下側に出た。
				{
					this.Status.ExitDirection = 2;
					break;
				}

				// 画面遷移時の微妙なカメラ位置ズレ解消
				// -- スタート地点(入場地点)が地面と接していると、最初のフレームでプレイヤーは上に押し出されてカメラの初期位置とズレてしまう。
				if (this.Frame == 0)
				{
					this.カメラ位置調整(true);
				}

				DDCrash plCrash = DDCrashUtils.Point(new D2Point(this.Player.X, this.Player.Y));

				// ====
				// 描画ここから
				// ====

				this.DrawWall();
				this.DrawMap();
				this.Player.Draw();

				// memo: DeadFlag をチェックするのは「当たり判定」から

				foreach (Enemy enemy in this.Enemies.Iterate())
				{
					enemy.Crash = DDCrashUtils.None(); // reset
					enemy.Draw();
				}
				foreach (Shot shot in this.Shots.Iterate())
				{
					shot.Crash = DDCrashUtils.None(); // reset
					shot.Draw();
				}

				if (this.当たり判定表示)
				{
					// 最後に描画されるように DDGround.EL.Add() する。

					DDGround.EL.Add(() =>
					{
						DDCurtain.DrawCurtain(-0.7);

						const double A = 0.7;

						DDCrashView.Draw(new DDCrash[] { plCrash }, new I3Color(255, 0, 0), 1.0);
						DDCrashView.Draw(this.Enemies.Iterate().Select(v => v.Crash), new I3Color(255, 255, 255), A);
						DDCrashView.Draw(this.Shots.Iterate().Select(v => v.Crash), new I3Color(0, 255, 255), A);

						return false;
					});
				}

				// ====
				// 描画ここまで
				// ====

				// ====
				// 当たり判定ここから
				// ====

				// ? 無敵な攻撃中 -> 敵 x 自機 の衝突判定を行わない。
				bool attackInvincibleMode =
					Game.I.Player.Attack != null &&
					Game.I.Player.Attack.IsInvincibleMode();

				foreach (Enemy enemy in this.Enemies.Iterate())
				{
					if (1 <= enemy.HP) // ? 敵：生存 && 無敵ではない
					{
						foreach (Shot shot in this.Shots.Iterate())
						{
							// 衝突判定：敵 x 自弾
							if (
								!shot.DeadFlag && // ? 自弾：生存
								enemy.Crash.IsCrashed(shot.Crash) // ? 衝突
								)
							{
								// ★ 敵_被弾ここから

								if (!shot.敵を貫通する)
									shot.Kill();

								enemy.HP -= shot.AttackPoint;

								if (1 <= enemy.HP) // ? まだ生存している。
								{
									enemy.Damaged(shot);
								}
								else // ? 撃破した。
								{
									enemy.Kill(true);
									break; // この敵は死亡したので、この敵について以降の当たり判定は不要
								}

								// ★ 敵_被弾ここまで
							}
						}
					}

					// 衝突判定：敵 x 自機
					if (
						this.Player.DeadFrame == 0 && // ? プレイヤー死亡中ではない。
						this.Player.DamageFrame == 0 && // ? プレイヤー・ダメージ中ではない。
						this.Player.InvincibleFrame == 0 && // ? プレイヤー無敵時間中ではない。
						!attackInvincibleMode && // 無敵になる攻撃中ではない。
						!enemy.DeadFlag && // ? 敵：生存
						DDCrashUtils.IsCrashed(enemy.Crash, plCrash) // ? 衝突
						)
					{
						// ★ 自機_被弾ここから

						if (enemy.自機に当たると消滅する)
							enemy.Kill();

						this.Player.HP -= enemy.AttackPoint;

						if (1 <= this.Player.HP) // ? まだ生存している。
						{
							this.Player.DamageFrame = 1;
						}
						else // ? 死亡した。
						{
							this.Player.HP = -1;
							//this.Player.DeadFrame = 1; // ヒットバックした後で死亡フレームを上げる。
							this.Player.DamageFrame = 1;
						}

						// ★ 自機_被弾ここまで
					}
				}

				foreach (Shot shot in this.Shots.Iterate())
				{
					// 壁への当たり判定は自弾の「中心座標のみ」であることに注意して下さい。

					if (
						!shot.DeadFlag && // ? 自弾：生存
						!shot.壁をすり抜ける && // ? この自弾は壁に当たる。
						this.Map.GetCell(GameCommon.ToTablePoint(shot.X, shot.Y)).Tile.IsWall() // ? 壁に当たった。
						)
						shot.Kill();
				}

				// ====
				// 当たり判定ここまで
				// ====

				f_ゴミ回収();

				this.Enemies.RemoveAll(v => v.DeadFlag);
				this.Shots.RemoveAll(v => v.DeadFlag);

				DDEngine.EachFrame();

				// ★★★ ゲームループの終わり ★★★
			}

			DDEngine.FreezeInput();

			if (this.Status.ExitDirection == 5)
			{
				DDMusicUtils.Fade();
				DDCurtain.SetCurtain(30, -1.0);

				foreach (DDScene scene in DDSceneUtils.Create(40))
				{
					this.DrawWall();
					this.DrawMap();

					DDEngine.EachFrame();
				}
			}
			else
			{
				double destSlide_X = 0.0;
				double destSlide_Y = 0.0;

				switch (this.Status.ExitDirection)
				{
					case 4:
						destSlide_X = DDConsts.Screen_W;
						break;

					case 6:
						destSlide_X = -DDConsts.Screen_W;
						break;

					case 8:
						destSlide_Y = DDConsts.Screen_H;
						break;

					case 2:
						destSlide_Y = -DDConsts.Screen_H;
						break;

					default:
						throw null; // never
				}
				using (DDSubScreen wallMapScreen = new DDSubScreen(DDConsts.Screen_W, DDConsts.Screen_H))
				{
					using (wallMapScreen.Section())
					{
						this.DrawWall();
						this.DrawMap();
					}
					foreach (DDScene scene in DDSceneUtils.Create(30))
					{
						double slide_X = destSlide_X * scene.Rate;
						double slide_Y = destSlide_Y * scene.Rate;

						DDCurtain.DrawCurtain();
						DDDraw.DrawSimple(wallMapScreen.ToPicture(), slide_X, slide_Y);

						DDEngine.EachFrame();
					}
				}
				DDCurtain.SetCurtain(0, -1.0);
			}

			// ★★★★★
			// プレイヤー・ステータス反映(マップ退場時)
			// その他の反映箇所：
			// -- マップ入場時
			// -- セーブ時
			{
				this.Status.StartChara = this.Player.Chara;
				this.Status.StartHP = this.Player.HP;
				this.Status.StartFacingLeft = this.Player.FacingLeft;
			}

			// ★★★ end of Perform() ★★★
		}

		/// <summary>
		/// あまりにもマップから離れすぎている敵・自弾の死亡フラグを立てる。
		/// </summary>
		/// <returns></returns>
		private IEnumerable<bool> E_ゴミ回収()
		{
			for (; ; )
			{
				foreach (Enemy enemy in this.Enemies.Iterate())
				{
					if (this.IsProbablyEvacuated(enemy.X, enemy.Y))
						enemy.DeadFlag = true;

					yield return true;
				}
				foreach (Shot shot in this.Shots.Iterate())
				{
					if (this.IsProbablyEvacuated(shot.X, shot.Y))
						shot.DeadFlag = true;

					yield return true;
				}
				yield return true; // ループ内で1度も実行されない場合を想定
			}
		}

		private void カメラ位置調整(bool 一瞬で)
		{
			double targCamX = this.Player.X - DDConsts.Screen_W / 2 + (this.CamSlideX * DDConsts.Screen_W / 3);
			double targCamY = this.Player.Y - DDConsts.Screen_H / 2 + (this.CamSlideY * DDConsts.Screen_H / 3);

			DDUtils.ToRange(ref targCamX, 0.0, this.Map.W * GameConsts.TILE_W - DDConsts.Screen_W);
			DDUtils.ToRange(ref targCamY, 0.0, this.Map.H * GameConsts.TILE_H - DDConsts.Screen_H);

			// 不要
			//if (this.Map.W * GameConsts.TILE_W - DDConsts.Screen_W < GameConsts.TILE_W) // ? カメラの横の可動域が1タイルより狭い場合
			//    targCamX = (this.Map.W * GameConsts.TILE_W - DDConsts.Screen_W) / 2; // 中心に合わせる。

			if (this.Map.H * GameConsts.TILE_H - DDConsts.Screen_H < GameConsts.TILE_H) // ? カメラの縦の可動域が1タイルより狭い場合
				targCamY = (this.Map.H * GameConsts.TILE_H - DDConsts.Screen_H) / 2; // 中心に合わせる。

			DDUtils.Approach(ref DDGround.Camera.X, targCamX, 一瞬で ? 0.0 : 0.8);
			DDUtils.Approach(ref DDGround.Camera.Y, targCamY, 一瞬で ? 0.0 : 0.8);

			//DDUtils.ToRange(ref DDGround.Camera.X, 0.0, this.Map.W * Consts.TILE_W - DDConsts.Screen_W);
			//DDUtils.ToRange(ref DDGround.Camera.Y, 0.0, this.Map.H * Consts.TILE_H - DDConsts.Screen_H);

			DDGround.ICamera.X = SCommon.ToInt(DDGround.Camera.X);
			DDGround.ICamera.Y = SCommon.ToInt(DDGround.Camera.Y);
		}

		#region Edit

		private void Edit()
		{
			this.Map.Load(); // ゲーム中にマップを書き換える場合があるので、再ロードする。

			DDEngine.FreezeInput();
			DDUtils.SetMouseDispMode(true);
			LevelEditor.ShowDialog();

			int lastMouseX = DDMouse.X;
			int lastMouseY = DDMouse.Y;

			for (; ; )
			{
				if (LevelEditor.Dlg.XPressed)
					break;

				// 廃止
				//if (DDKey.GetInput(DX.KEY_INPUT_E) == 1)
				//    break;

				I2Point cellPos = GameCommon.ToTablePoint(
					DDGround.Camera.X + DDMouse.X,
					DDGround.Camera.Y + DDMouse.Y
					);

				MapCell cell = Game.I.Map.GetCell(cellPos);

				if (cell.IsDefault)
				{
					// noop
				}
				else if (1 <= DDKey.GetInput(DX.KEY_INPUT_LSHIFT) && 1 <= DDKey.GetInput(DX.KEY_INPUT_LCONTROL)) // 左シフト・コントロール押下 -> 塗り潰し / none
				{
					if (DDMouse.L.GetInput() == -1) // クリックを検出
					{
						this.Map.Save(); // 失敗を想定して、セーブしておく

						switch (LevelEditor.Dlg.GetMode())
						{
							case LevelEditor.Mode_e.TILE:
								{
									string tileName = LevelEditor.Dlg.GetTile();

									if (tileName != cell.TileName)
									{
										string targetTileName = cell.TileName; // cell.TileName は this.EditFill で変更される。

										this.EditFill(
											cellPos,
											v => v.TileName == targetTileName,
											v =>
											{
												v.TileName = tileName;
												v.Tile = TileCatalog.Create(tileName);
											}
											);
									}
								}
								break;

							case LevelEditor.Mode_e.ENEMY:
								{
									string enemyName = LevelEditor.Dlg.GetEnemy();

									if (enemyName != cell.EnemyName)
									{
										string targetEnemyName = cell.EnemyName; // cell.EnemyName は this.EditFill で変更される。

										this.EditFill(
											cellPos,
											v => v.EnemyName == targetEnemyName,
											v => v.EnemyName = enemyName
											);
									}
								}
								break;

							default:
								throw null; // never
						}
					}
					else if (1 <= DDMouse.R.GetInput())
					{
						// none
					}
				}
				else if (1 <= DDKey.GetInput(DX.KEY_INPUT_LSHIFT)) // 左シフト押下 -> 移動 / none
				{
					if (1 <= DDMouse.L.GetInput())
					{
						DDGround.Camera.X -= DDMouse.X - lastMouseX;
						DDGround.Camera.Y -= DDMouse.Y - lastMouseY;

						DDUtils.ToRange(ref DDGround.Camera.X, 0.0, this.Map.W * GameConsts.TILE_W - DDConsts.Screen_W);
						DDUtils.ToRange(ref DDGround.Camera.Y, 0.0, this.Map.H * GameConsts.TILE_H - DDConsts.Screen_H);

						DDGround.ICamera.X = SCommon.ToInt(DDGround.Camera.X);
						DDGround.ICamera.Y = SCommon.ToInt(DDGround.Camera.Y);
					}
					else if (1 <= DDMouse.R.GetInput())
					{
						// none
					}
				}
				else if (1 <= DDKey.GetInput(DX.KEY_INPUT_LCONTROL)) // 左コントロール押下 -> スポイト / none
				{
					if (1 <= DDMouse.L.GetInput())
					{
						switch (LevelEditor.Dlg.GetMode())
						{
							case LevelEditor.Mode_e.TILE:
								LevelEditor.Dlg.SetTile(cell.TileName);
								break;

							case LevelEditor.Mode_e.ENEMY:
								LevelEditor.Dlg.SetEnemy(cell.EnemyName);
								break;

							default:
								throw null; // never
						}
					}
					else if (1 <= DDMouse.R.GetInput())
					{
						// none
					}
				}
				else if (1 <= DDKey.GetInput(DX.KEY_INPUT_LALT)) // 左 ALT 押下 -> 自機ワープ / none
				{
					if (DDMouse.L.GetInput() == -1) // クリックを検出
					{
						this.Player.X = cellPos.X * GameConsts.TILE_W + GameConsts.TILE_W / 2;
						this.Player.Y = cellPos.Y * GameConsts.TILE_H + GameConsts.TILE_H / 2;

						DDGround.EL.Add(SCommon.Supplier(Effects.中爆発(this.Player.X, this.Player.Y))); // アクションが分かるように
					}
					else if (1 <= DDMouse.R.GetInput())
					{
						// none
					}
				}
				else // シフト系押下無し -> セット / クリア
				{
					if (1 <= DDMouse.L.GetInput())
					{
						switch (LevelEditor.Dlg.GetMode())
						{
							case LevelEditor.Mode_e.TILE:
								{
									string tileName = LevelEditor.Dlg.GetTile();

									cell.TileName = tileName;
									cell.Tile = TileCatalog.Create(tileName);
								}
								break;

							case LevelEditor.Mode_e.ENEMY:
								{
									string enemyName = LevelEditor.Dlg.GetEnemy();

									cell.EnemyName = enemyName;
								}
								break;

							default:
								throw null; // never
						}
					}
					else if (1 <= DDMouse.R.GetInput())
					{
						switch (LevelEditor.Dlg.GetMode())
						{
							case LevelEditor.Mode_e.TILE:
								cell.TileName = GameConsts.TILE_NONE;
								cell.Tile = new Tile_None();
								break;

							case LevelEditor.Mode_e.ENEMY:
								cell.EnemyName = GameConsts.ENEMY_NONE;
								break;

							default:
								throw null; // never
						}
					}
				}

				if (DDKey.GetInput(DX.KEY_INPUT_S) == 1) // S キー --> Save
				{
					this.Map.Save();

					// 表示
					{
						int endFrame = DDEngine.ProcFrame + 60;

						DDGround.EL.Add(() =>
						{
							DDPrint.SetDebug(0, 16);
							DDPrint.SetBorder(new I3Color(0, 0, 0));
							DDPrint.Print("セーブしました...");
							DDPrint.Reset();

							return DDEngine.ProcFrame < endFrame;
						});
					}
				}
				if (DDKey.GetInput(DX.KEY_INPUT_L) == 1) // L キー --> Load
				{
					this.Map.Load();

					// 表示
					{
						int endFrame = DDEngine.ProcFrame + 60;

						DDGround.EL.Add(() =>
						{
							DDPrint.SetDebug(0, 16);
							DDPrint.SetBorder(new I3Color(0, 0, 0));
							DDPrint.Print("ロードしました...");
							DDPrint.Reset();

							return DDEngine.ProcFrame < endFrame;
						});
					}
				}

				DDCurtain.DrawCurtain();

				if (LevelEditor.Dlg.IsShowTile())
					this.DrawMap();

				if (LevelEditor.Dlg.IsShowEnemy())
					LevelEditor.DrawEnemy();

				lastMouseX = DDMouse.X;
				lastMouseY = DDMouse.Y;

				DDEngine.EachFrame();
			}
			DDEngine.FreezeInput();
			DDUtils.SetMouseDispMode(false);
			LevelEditor.CloseDialog();

			this.Map.Save(); // ★★★ マップをセーブする ★★★
		}

		private void EditFill(I2Point targetPos, Predicate<MapCell> isFillable, Action<MapCell> fill)
		{
			Queue<I2Point> q = new Queue<I2Point>();

			q.Enqueue(targetPos);

			while (1 <= q.Count)
			{
				I2Point cellPos = q.Dequeue();
				MapCell cell = this.Map.GetCell(cellPos);

				if (!cell.IsDefault && isFillable(cell))
				{
					fill(cell);

					q.Enqueue(new I2Point(cellPos.X - 1, cellPos.Y));
					q.Enqueue(new I2Point(cellPos.X + 1, cellPos.Y));
					q.Enqueue(new I2Point(cellPos.X, cellPos.Y - 1));
					q.Enqueue(new I2Point(cellPos.X, cellPos.Y + 1));
				}
			}
		}

		#endregion

		private void DrawWall()
		{
			double xRate = (double)DDGround.ICamera.X / (this.Map.W * GameConsts.TILE_W - DDConsts.Screen_W);
			double yRate = (double)DDGround.ICamera.Y / (this.Map.H * GameConsts.TILE_H - DDConsts.Screen_H);

			this.Wall.Draw(xRate, yRate);
		}

		private void DrawMap()
		{
			int w = this.Map.W;
			int h = this.Map.H;

			int cam_l = DDGround.ICamera.X;
			int cam_t = DDGround.ICamera.Y;
			int cam_r = cam_l + DDConsts.Screen_W;
			int cam_b = cam_t + DDConsts.Screen_H;

			I2Point lt = GameCommon.ToTablePoint(cam_l, cam_t);
			I2Point rb = GameCommon.ToTablePoint(cam_r, cam_b);

			// マージン付与
			// -- マップセルの範囲をはみ出て描画されるタイルのためにマージンを増やす。
			{
				lt.X -= 2;
				lt.Y -= 2;
				rb.X += 2;
				rb.Y += 2;
			}

			lt.X = SCommon.ToRange(lt.X, 0, w - 1);
			lt.Y = SCommon.ToRange(lt.Y, 0, h - 1);
			rb.X = SCommon.ToRange(rb.X, 0, w - 1);
			rb.Y = SCommon.ToRange(rb.Y, 0, h - 1);

			for (int x = lt.X; x <= rb.X; x++)
			{
				for (int y = lt.Y; y <= rb.Y; y++)
				{
					MapCell cell = this.Map.Table[x, y];

					int tileX = x * GameConsts.TILE_W + GameConsts.TILE_W / 2;
					int tileY = y * GameConsts.TILE_H + GameConsts.TILE_H / 2;

					cell.Tile.Draw(tileX - cam_l, tileY - cam_t);
				}
			}
		}

		public DDList<Enemy> Enemies = new DDList<Enemy>();
		public DDList<Shot> Shots = new DDList<Shot>();

		private void ReloadEnemies()
		{
			this.Enemies.Clear();

			for (int x = 0; x < this.Map.W; x++)
			{
				for (int y = 0; y < this.Map.H; y++)
				{
					string enemyName = this.Map.Table[x, y].EnemyName;

					if (enemyName != GameConsts.ENEMY_NONE)
					{
						this.Enemies.Add(EnemyCatalog.Create(
							this.Map.Table[x, y].EnemyName,
							x * GameConsts.TILE_W + GameConsts.TILE_W / 2.0,
							y * GameConsts.TILE_H + GameConsts.TILE_H / 2.0
							));
					}
				}
			}
		}

		/// <summary>
		/// マップから離れすぎているか
		/// 退場と見なして良いか
		/// </summary>
		/// <param name="x">X_座標</param>
		/// <param name="y">Y_座標</param>
		/// <returns></returns>
		private bool IsProbablyEvacuated(double x, double y)
		{
			const int MGN_SCREEN_NUM = 3;

			return
				x < -DDConsts.Screen_W * MGN_SCREEN_NUM || this.Map.W * GameConsts.TILE_W + DDConsts.Screen_W * MGN_SCREEN_NUM < x ||
				y < -DDConsts.Screen_H * MGN_SCREEN_NUM || this.Map.H * GameConsts.TILE_H + DDConsts.Screen_H * MGN_SCREEN_NUM < y;
		}

		private bool Pause_ReturnToTitleMenu = false;
		private bool 当たり判定表示 = false;

		private static DDSubScreen Pause_KeptMainScreen = new DDSubScreen(DDConsts.Screen_W, DDConsts.Screen_H);

		/// <summary>
		/// ポーズメニュー
		/// </summary>
		private void Pause()
		{
			DDMain.KeepMainScreen();
			SCommon.Swap(ref DDGround.KeptMainScreen, ref Pause_KeptMainScreen);

			DDSimpleMenu simpleMenu = new DDSimpleMenu()
			{
				BorderColor = new I3Color(0, 64, 128),
				WallDrawer = () =>
				{
					DDDraw.DrawSimple(Pause_KeptMainScreen.ToPicture(), 0, 0);

					DDDraw.SetAlpha(0.5);
					DDDraw.SetBright(0, 0, 0);
					DDDraw.DrawRect(Ground.I.Picture.WhiteBox, 0, DDConsts.Screen_H / 4, DDConsts.Screen_W, DDConsts.Screen_H / 2);
					DDDraw.Reset();
				},
			};

			DDEngine.FreezeInput();

			int selectIndex = 0;

			for (; ; )
			{
				selectIndex = simpleMenu.Perform(
					100,
					180,
					50,
					24,
					"システムメニュー",
					new string[]
					{
						"設定",
						"タイトルに戻る",
						"ゲームに戻る",
					},
					selectIndex,
					true
					);

				switch (selectIndex)
				{
					case 0:
						using (new SettingMenu()
						{
							SimpleMenu = new DDSimpleMenu()
							{
								BorderColor = new I3Color(0, 64, 128),
								WallDrawer = () =>
								{
									DDDraw.DrawSimple(Pause_KeptMainScreen.ToPicture(), 0, 0);
									DDCurtain.DrawCurtain(-0.7);
								},
							},
						})
						{
							SettingMenu.I.Perform();
						}
						break;

					case 1:
						if (new Confirm() { BorderColor = new I3Color(0, 0, 200), }.Perform("タイトル画面に戻ります。", "はい", "いいえ") == 0)
						{
							this.Pause_ReturnToTitleMenu = true;
							goto endLoop;
						}
						break;

					case 2:
						goto endLoop;

					default:
						throw null; // never
				}
				//DDEngine.EachFrame(); // 不要
			}
		endLoop:
			DDEngine.FreezeInput();

			DDInput.A.FreezeInputUntilRelease = true;
			DDInput.B.FreezeInputUntilRelease = true;
		}

		/// <summary>
		/// ポーズメニュー(デバッグ用)
		/// </summary>
		private void DebugPause()
		{
			DDMain.KeepMainScreen();

			DDSimpleMenu simpleMenu = new DDSimpleMenu()
			{
				BorderColor = new I3Color(0, 128, 64),
				WallDrawer = () =>
				{
					DDDraw.DrawSimple(DDGround.KeptMainScreen.ToPicture(), 0, 0);
					DDCurtain.DrawCurtain(-0.5);
				},
			};

			DDEngine.FreezeInput();

			int selectIndex = 0;

			for (; ; )
			{
				selectIndex = simpleMenu.Perform(
					40,
					40,
					40,
					24,
					"デバッグ用メニュー",
					new string[]
					{
						"キャラクタ切り替え [ 現在のキャラクタ：" + Player.GetName(this.Player.Chara) + " ]",
						"デバッグ強制遅延 [ 現在の設定：" + DDEngine.SlowdownLevel + " ]",
						"当たり判定表示 [ 現在の設定：" + this.当たり判定表示 + " ]",
						"ゲームに戻る",
					},
					selectIndex,
					true,
					true
					);

				switch (selectIndex)
				{
					case 0:
						this.Player.Chara = (Player.Chara_e)(((int)this.Player.Chara + 1) % Enum.GetValues(typeof(Player.Chara_e)).Length);
						break;

					case 1:
						if (DDEngine.SlowdownLevel == 0)
							DDEngine.SlowdownLevel++;
						else
							DDEngine.SlowdownLevel *= 2;

						DDEngine.SlowdownLevel %= 16;
						break;

					case 2:
						this.当たり判定表示 = !this.当たり判定表示;
						break;

					case 3:
						goto endLoop;

					default:
						throw null; // never
				}
				//DDEngine.EachFrame(); // 不要
			}
		endLoop:
			DDEngine.FreezeInput();

			DDInput.A.FreezeInputUntilRelease = true;
			DDInput.B.FreezeInputUntilRelease = true;
		}
	}
}
