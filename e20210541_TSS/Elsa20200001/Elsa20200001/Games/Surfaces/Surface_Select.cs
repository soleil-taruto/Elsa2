﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DxLibDLL;
using Charlotte.Commons;
using Charlotte.GameCommons;

namespace Charlotte.Games.Surfaces
{
	/// <summary>
	/// 選択肢
	/// </summary>
	public class Surface_Select : Surface
	{
		public static bool Hide = false; // Game から制御される。

		public Surface_Select(string typeName, string instanceName)
			: base(typeName, instanceName)
		{
			this.Z = 80000;
		}

		public class OptionInfo
		{
			public string Title = "ここに選択肢に表示する文字列を設定します。";
			public string ScenarioName = GameConsts.DUMMY_SCENARIO_NAME;

			// <---- prm

			public bool MouseFocused = false;
		}

		public List<OptionInfo> Options = new List<OptionInfo>();

		public int GetMouseFocusedIndex()
		{
			for (int index = 0; index < this.Options.Count; index++)
				if (this.Options[index].MouseFocused)
					return index;

			return -1; // フォーカス無し
		}

		public override IEnumerable<bool> E_Draw()
		{
			Game.I.SkipMode = false;

			for (; ; )
			{
				//Game.I.CancelSkipAutoMode();

				if (
					this.Options.Count < GameConsts.SELECT_OPTION_MIN ||
					this.Options.Count > GameConsts.SELECT_OPTION_MAX
					)
					throw new DDError("選択肢の個数に問題があります。");

				// ---- 入力ここから

				if (!Game.I.BacklogMode)
				{
					int moving = 0;

					if (DDInput.DIR_8.IsPound())
						moving = -1;

					if (DDInput.DIR_2.IsPound())
						moving = 1;

					if (moving != 0)
					{
						int optIndex = this.GetMouseFocusedIndex();

						if (optIndex == -1)
						{
							optIndex = 0;
						}
						else
						{
							optIndex += this.Options.Count + moving;
							optIndex %= this.Options.Count;
						}

						DDMouse.X =
							GameConsts.SELECT_FRAME_L +
							Ground.I.Picture.MessageFrame29_Button2.Get_W() * 2 -
							10;
						DDMouse.Y =
							GameConsts.SELECT_FRAME_T + GameConsts.SELECT_FRAME_T_STEP * optIndex +
							Ground.I.Picture.MessageFrame29_Button2.Get_H() * 2 -
							10;

						DDMouse.PosChanged();
					}
				}

				// ---- ここから描画

				if (!Hide)
				{
					for (int index = 0; index < GameConsts.SELECT_FRAME_NUM; index++)
					{
						DDPicture picture = Ground.I.Picture.MessageFrame29_Button;

						if (index < this.Options.Count)
						{
							picture = Ground.I.Picture.MessageFrame29_Button2;

							if (this.Options[index].MouseFocused)
								picture = Ground.I.Picture.MessageFrame29_Button3;
						}

						DDDraw.DrawBeginRect(
							picture,
							GameConsts.SELECT_FRAME_L,
							GameConsts.SELECT_FRAME_T + GameConsts.SELECT_FRAME_T_STEP * index,
							picture.Get_W() * 2.0,
							picture.Get_H() * 2.0
							);
						DDCrash drawedCrash = DDDraw.DrawGetCrash();
						DDDraw.DrawEnd();

						// フォーカスしている選択項目を再設定
						{
							if (index < this.Options.Count)
							{
								bool mouseIn = drawedCrash.IsCrashed(DDCrashUtils.Point(new D2Point(DDMouse.X, DDMouse.Y)));

								this.Options[index].MouseFocused = mouseIn;
							}
						}
					}
					for (int index = 0; index < this.Options.Count; index++)
					{
						const int title_x = 160;
						const int title_y = 56;

						DDFontUtils.DrawString(
							 GameConsts.SELECT_FRAME_L + title_x,
							 GameConsts.SELECT_FRAME_T + GameConsts.SELECT_FRAME_T_STEP * index + title_y,
							 this.Options[index].Title,
							 DDFontUtils.GetFont("Kゴシック", 32),
							 false,
							 new I3Color(110, 100, 90)
							 );
					}
				}

				// 隠しているなら選択出来ない。
				if (Hide)
					foreach (OptionInfo option in this.Options)
						option.MouseFocused = false;

				yield return true;
			}
		}

		protected override void Invoke_02(string command, params string[] arguments)
		{
			//int c = 0;

			if (command == "選択肢") // 即時
			{
				this.Options.Add(new OptionInfo()
				{
					Title = arguments[0],
				});
			}
			else if (command == "分岐先") // 即時
			{
				this.Options[this.Options.Count - 1].ScenarioName = arguments[0];
			}
			else
			{
				throw new DDError();
			}
		}

		protected override string[] Serialize_02()
		{
			List<string> lines = new List<string>();

			lines.Add(this.Options.Count.ToString());

			foreach (OptionInfo option in this.Options)
			{
				lines.Add(option.Title);
				lines.Add(option.ScenarioName);
			}
			return lines.ToArray();
		}

		protected override void Deserialize_02(string[] lines)
		{
			int c = 0;

			{
				int count = int.Parse(lines[c++]);

				this.Options.Clear();

				for (int index = 0; index < count; index++)
				{
					OptionInfo option = new OptionInfo();

					option.Title = lines[c++];
					option.ScenarioName = lines[c++];

					this.Options.Add(option);
				}
			}
		}
	}
}
