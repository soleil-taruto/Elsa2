using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DxLibDLL;
using Charlotte.Commons;
using Charlotte.GameCommons;
using Charlotte.Novels.Surfaces;

namespace Charlotte.Novels
{
	public class Novel : IDisposable
	{
		public NovelStatus Status = new NovelStatus(); // 軽量な仮設オブジェクト

		// <---- prm // HACK: abolished !!!

		public static Novel I;

		public Novel()
		{
			I = this;
		}

		public void Dispose()
		{
			I = null;
		}

		public ScenarioPage CurrPage;
		public int SelectedSystemButtonIndex = -1; // -1 == システムボタン未選択
		public bool SkipMode;
		public bool AutoMode;

		public int DispSubtitleCharCount;
		public int DispCharCount;
		public int DispPageEndedCount;
		public bool DispFastMode;

		public bool ForceNextPage = false; // ? 強制的に次のページへ進む

		public void Perform()
		{
			DDUtils.SetMouseDispMode(true);

			// reset
			{
				this.SkipMode = false;
				this.AutoMode = false;

				Surface_MessageWindow.Hide = false;
				Surface_SystemButtons.Hide = false;
			}

			DDCurtain.SetCurtain(0, -1.0);
			DDCurtain.SetCurtain();

		restartCurrPage:
			this.CurrPage = this.Status.Scenario.Pages[this.Status.CurrPageIndex];

			foreach (ScenarioCommand command in this.CurrPage.Commands)
				command.Invoke();

			this.DispSubtitleCharCount = 0;
			this.DispCharCount = 0;
			this.DispPageEndedCount = 0;
			this.DispFastMode = false;

			DDEngine.FreezeInput();

			for (; ; )
			{
				bool nextPageFlag = false;

				// ★★★ キー押下は 1 マウス押下は -1 で判定する。

				// 入力：シナリオを進める。(マウスホイール)
				if (DDMouse.Rot < 0)
				{
					this.CancelSkipAutoMode();

					if (this.DispPageEndedCount < NovelConsts.NEXT_PAGE_INPUT_INTERVAL) // ? ページ表示_未完了 -> ページ表示_高速化
					{
						this.DispFastMode = true;
					}
					else // ? ページ表示_完了 -> 次ページ
					{
						if (!this.Status.HasSelect())
							nextPageFlag = true;
					}
					DDEngine.FreezeInput(NovelConsts.SHORT_INPUT_SLEEP);
				}

				// 入力：シナリオを進める。(マウスホイール_以外)
				if (
					//DDKey.GetInput(DX.KEY_INPUT_SPACE) == 1 ||
					//DDKey.GetInput(DX.KEY_INPUT_RETURN) == 1 ||
					DDMouse.L.GetInput() == -1 && this.SelectedSystemButtonIndex == -1 || // システムボタン以外を左クリック
					DDInput.A.GetInput() == 1
					)
				{
					if (this.DispPageEndedCount < NovelConsts.NEXT_PAGE_INPUT_INTERVAL) // ? ページ表示_未完了 -> ページ表示_高速化
					{
						this.DispFastMode = true;
					}
					else // ? ページ表示_完了 -> 次ページ
					{
						if (!this.Status.HasSelect()) // ? 選択肢表示中ではない。
						{
							nextPageFlag = true;
						}
						else // ? 選択肢表示中
						{
							int index = this.Status.GetSelect().GetMouseFocusedIndex();

							if (index != -1) // 選択中の選択肢へ進む
							{
								string scenarioName = this.Status.GetSelect().Options[index].ScenarioName;

								this.Status.Scenario = new Scenario(scenarioName);
								this.Status.CurrPageIndex = 0;
								this.Status.RemoveSelect(); // 選択肢_終了

								goto restartCurrPage;
							}
						}
					}
				}

				if (this.SkipMode)
					if (!this.Status.HasSelect())
						if (1 <= this.DispPageEndedCount)
							nextPageFlag = true;

				if (this.AutoMode)
					if (!this.Status.HasSelect())
						if (NovelConsts.AUTO_NEXT_PAGE_INTERVAL <= this.DispPageEndedCount)
							nextPageFlag = true;

				if (nextPageFlag || this.ForceNextPage) // 次ページ
				{
					this.ForceNextPage = false;

					// スキップモード時はページを進める毎にエフェクトを強制終了する。
					if (this.SkipMode)
						foreach (Surface surface in this.Status.Surfaces)
							surface.Act.Flush();

					this.Status.CurrPageIndex++;

					if (this.Status.Scenario.Pages.Count <= this.Status.CurrPageIndex)
						break;

					goto restartCurrPage;
				}

				// 入力：過去ログ
				if (
					//DDKey.GetInput(DX.KEY_INPUT_LEFT) == 1 ||
					DDInput.DIR_4.GetInput() == 1 ||
					0 < DDMouse.Rot
					)
				{
					this.BackLog();
				}

				// 入力：鑑賞モード
				if (
					//DDKey.GetInput(DX.KEY_INPUT_BACK) == 1 ||
					DDMouse.R.GetInput() == -1 ||
					DDInput.B.GetInput() == 1
					)
				{
					this.Appreciate();
				}

				// 入力：システムボタン
				if (DDMouse.L.GetInput() == -1 && this.SelectedSystemButtonIndex != -1) // システムボタンを左クリック
				{
					switch (this.SelectedSystemButtonIndex)
					{
						case 0: this.SkipMode = !this.SkipMode; break;
						case 1: this.AutoMode = !this.AutoMode; break;
						case 2: this.BackLog(); break;
						case 3: this.SystemMenu(); break;

						default:
							throw null; // never
					}
					if (this.SystemMenu_ReturnToTitleMenu)
						break;
				}

				if (
					this.CurrPage.Subtitle.Length < this.DispSubtitleCharCount &&
					this.CurrPage.Text.Length < this.DispCharCount
					)
					this.DispPageEndedCount++;

				if (this.SkipMode)
				{
					this.DispSubtitleCharCount += 8;
					this.DispCharCount += 8;
				}
				else if (this.DispFastMode)
				{
					this.DispSubtitleCharCount += 2;
					this.DispCharCount += 2;
				}
				else
				{
					if (DDEngine.ProcFrame % 2 == 0)
						this.DispSubtitleCharCount++;

					if (DDEngine.ProcFrame % 3 == 0)
						this.DispCharCount++;
				}
				DDUtils.ToRange(ref this.DispSubtitleCharCount, 0, SCommon.IMAX);
				DDUtils.ToRange(ref this.DispCharCount, 0, SCommon.IMAX);

				// ====
				// 描画ここから
				// ====

				this.DrawSurfaces();

				// ====
				// 描画ここまで
				// ====

				DDEngine.EachFrame();

				// ★★★ ゲームループの終わり ★★★
			}

			DDCurtain.SetCurtain(30, -1.0);
			DDMusicUtils.Fade();

			foreach (DDScene scene in DDSceneUtils.Create(40))
			{
				this.DrawSurfaces();

				DDEngine.EachFrame();
			}

			DDEngine.FreezeInput();

			DDUtils.SetMouseDispMode(false);

			// ★★★ end of Perform() ★★★
		}

		/// <summary>
		/// スキップモード・オートモードを解除する。
		/// 両モード中、何か入力があれば解除されるのが自然だと思う。
		/// どこで解除しているか分かるようにメソッド化した。
		/// </summary>
		public void CancelSkipAutoMode()
		{
			this.SkipMode = false;
			this.AutoMode = false;
		}

		/// <summary>
		/// <para>主たる画面描画</para>
		/// <para>色々な場所(モード)から呼び出されるだろう。</para>
		/// </summary>
		public void DrawSurfaces()
		{
			DDCurtain.DrawCurtain(); // 画面クリア

			// Z-オーダー順
			Novel.I.Status.Surfaces.Sort((a, b) =>
			{
				int ret = a.Z - b.Z;
				if (ret != 0)
					return ret;

				ret = SCommon.Comp(a.X, b.X);
				if (ret != 0)
					return ret;

				ret = SCommon.Comp(a.Y, b.Y);
				return ret;
			});

			foreach (Surface surface in Novel.I.Status.Surfaces) // キャラクタ・オブジェクト・壁紙
				if (!surface.Act.Draw())
					surface.Draw();
		}

		/// <summary>
		/// 過去ログ
		/// </summary>
		private void BackLog()
		{
			List<string> logLines = new List<string>();

			for (int index = 0; index < this.Status.CurrPageIndex; index++)
				foreach (string line in this.Status.Scenario.Pages[index].Lines)
					logLines.Add(line);

			DDEngine.FreezeInput(NovelConsts.SHORT_INPUT_SLEEP);

			int backIndex = 0;

			for (; ; )
			{
				if (
					//DDKey.GetInput(DX.KEY_INPUT_SPACE) == 1 ||
					//DDKey.GetInput(DX.KEY_INPUT_RETURN) == 1 ||
					DDMouse.L.GetInput() == -1 ||
					DDInput.A.GetInput() == 1
					)
					break;

				if (
					//DDKey.IsPound(DX.KEY_INPUT_UP) ||
					DDInput.DIR_8.IsPound() ||
					0 < DDMouse.Rot
					)
					backIndex++;

				if (
					//DDKey.IsPound(DX.KEY_INPUT_DOWN) ||
					DDInput.DIR_2.IsPound() ||
					DDMouse.Rot < 0
					)
					backIndex--;

				if (
					//DDKey.IsPound(DX.KEY_INPUT_RIGHT) ||
					DDInput.DIR_6.GetInput() == 1
					)
					backIndex = -1;

				DDUtils.ToRange(ref backIndex, -1, logLines.Count - 1);

				if (backIndex < 0)
					break;

				this.DrawSurfaces();
				DDCurtain.DrawCurtain(-0.5);

				for (int c = 1; c <= 16; c++)
				{
					int i = logLines.Count - backIndex - c;

					if (0 <= i)
					{
						DDFontUtils.DrawString(10, DDConsts.Screen_H - c * 30 - 15, logLines[i], DDFontUtils.GetFont("Kゴシック", 16));
					}
				}
				DDEngine.EachFrame();
			}
			DDEngine.FreezeInput(NovelConsts.SHORT_INPUT_SLEEP);
		}

		/// <summary>
		/// 鑑賞モード
		/// </summary>
		private void Appreciate()
		{
			Surface_MessageWindow.Hide = true;
			Surface_SystemButtons.Hide = true;

			DDEngine.FreezeInput(NovelConsts.SHORT_INPUT_SLEEP);

			for (; ; )
			{
				if (
					//DDKey.GetInput(DX.KEY_INPUT_SPACE) == 1 ||
					//DDKey.GetInput(DX.KEY_INPUT_RETURN) == 1 ||
					DDMouse.L.GetInput() == -1 ||
					DDMouse.R.GetInput() == -1 ||
					DDInput.A.GetInput() == 1 ||
					DDInput.B.GetInput() == 1
					)
					break;

				this.DrawSurfaces();
				DDEngine.EachFrame();
			}
			DDEngine.FreezeInput(NovelConsts.SHORT_INPUT_SLEEP);

			Surface_MessageWindow.Hide = false; // restore
			Surface_SystemButtons.Hide = false; // restore
		}

		private bool SystemMenu_ReturnToTitleMenu = false;

		/// <summary>
		/// システムメニュー画面
		/// </summary>
		private void SystemMenu()
		{
			this.SystemMenu_ReturnToTitleMenu = false; // reset

			DDSimpleMenu simpleMenu = new DDSimpleMenu()
			{
				Color = new I3Color(255, 255, 255),
				BorderColor = new I3Color(0, 64, 0),
				WallColor = new I3Color(32, 96, 32),
			};

			int selectIndex = 0;

			for (; ; )
			{
				selectIndex = simpleMenu.Perform(
					"システムメニュー(仮)",
					new string[]
					{
						"タイトルに戻る",
						"ゲームに戻る",
					},
					selectIndex
					);

				switch (selectIndex)
				{
					case 0:
						this.SystemMenu_ReturnToTitleMenu = true;
						goto endLoop;

					case 1:
						goto endLoop;

					default:
						throw null; // never
				}
				//DDEngine.EachFrame(); // 不要
			}
		endLoop:
			DDEngine.FreezeInput(NovelConsts.LONG_INPUT_SLEEP);
		}
	}
}
