using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.GameCommons;

namespace Charlotte.Games
{
	public class SaveOrLoadMenu : IDisposable
	{
		public static SaveOrLoadMenu I;

		public SaveOrLoadMenu()
		{
			I = this;
		}

		public void Dispose()
		{
			I = null;
		}

		public void Save(Action drawWall)
		{
			this.Perform(drawWall, true);
		}

		public void Load(Action drawWall)
		{
			this.Perform(drawWall, false);
		}

		private const int THUMB_W = 290;
		private const int THUMB_H = 200;

		private Action _drawWall;

		private void Perform(Action drawWall, bool saveMode)
		{
			_drawWall = drawWall;

			DDEngine.FreezeInput();

			for (; ; )
			{
				// ====
				// 入力判定ここから
				// ====

				if (
					DDInput.A.GetInput() == 1 ||
					DDInput.B.GetInput() == 1 ||
					DDMouse.R.GetInput() == -1
					)
					break;

				if (DDMouse.L.GetInput() == -1)
				{
					// TODO
				}

				// TODO

				// ====
				// 入力判定ここまで
				// ====

				// ====
				// 描画ここから
				// ====

				_drawWall();

				DDDraw.DrawSimple(Ground.I.Picture.詳細設定枠, 0, 0);

				// TODO

				// ====
				// 描画ここまで
				// ====

				DDEngine.EachFrame();
			}
			DDEngine.FreezeInput();
		}
	}
}
