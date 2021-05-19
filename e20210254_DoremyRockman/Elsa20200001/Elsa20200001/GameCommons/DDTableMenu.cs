using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Commons;

namespace Charlotte.GameCommons
{
	public class DDTableMenu
	{
		public int T = 100; // 描画する Y-座標 Top
		public int YStep = 20;
		public int FontSize = 16;
		public Action WallDrawer;

		// <---- prm

		private int Selected_X = 0;
		private int Selected_Y = 0;

		private class ItemInfo
		{
			public bool GroupFlag;
			public string Title;
			public I3Color Color;
			public I3Color BorderColor;
			public Action A_Desided;
		}

		private class ColumnInfo
		{
			public int X; // 描画する X-座標
			public List<ItemInfo> Items = new List<ItemInfo>();
		}

		private List<ColumnInfo> Columns = new List<ColumnInfo>();

		public void AddColumn(int x)
		{
			this.Columns.Add(new ColumnInfo() { X = x });
		}

		public void AddItem(bool groupFlag, string title, I3Color color, I3Color borderColor, Action a_desided)
		{
			this.Columns[this.Columns.Count - 1].Items.Add(new ItemInfo()
			{
				GroupFlag = groupFlag,
				Title = title,
				Color = color,
				BorderColor = borderColor,
				A_Desided = a_desided,
			});
		}

		public void Perform()
		{
			int lastItem_X = this.Columns.Count - 1;
			int lastItem_Y = this.Columns[lastItem_X].Items.Count - 1;

			DDCurtain.SetCurtain();
			DDEngine.FreezeInput();

			for (; ; )
			{
				if (DDInput.PAUSE.GetInput() == 1) // 即_終了
				{
					this.Selected_X = lastItem_X;
					this.Selected_Y = lastItem_Y;
					break;
				}
				if (DDInput.A.GetInput() == 1) // 決定
				{
					break;
				}
				if (DDInput.B.GetInput() == 1) // 一旦カーソルを終了に合わせてから、なお押されたら終了
				{
					if (
						this.Selected_X == lastItem_X &&
						this.Selected_Y == lastItem_Y
						)
						break;

					this.Selected_X = lastItem_X;
					this.Selected_Y = lastItem_Y;
				}

				bool 上へ移動した = false;

				if (DDInput.DIR_8.IsPound())
				{
					this.Selected_Y--;
					上へ移動した = true;
				}
				if (DDInput.DIR_2.IsPound())
				{
					this.Selected_Y++;
				}
				if (DDInput.DIR_4.IsPound())
				{
					this.Selected_X--;
				}
				if (DDInput.DIR_6.IsPound())
				{
					this.Selected_X++;
				}

				for (int trycnt = 1; ; trycnt++)
				{
					this.Selected_X %= this.Columns.Count;
					this.Selected_Y %= this.Columns[this.Selected_X].Items.Count;

					if (!this.Columns[this.Selected_X].Items[this.Selected_Y].GroupFlag)
						break;

					if (30 <= trycnt)
						throw new DDError();

					if (上へ移動した)
						this.Selected_Y--;
					else
						this.Selected_Y++;
				}

				this.WallDrawer();

				for (int x = 0; x < this.Columns.Count; x++)
				{
					ColumnInfo column = this.Columns[x];

					DDPrint.SetPrint(column.X, this.T, this.YStep, this.FontSize);

					for (int y = 0; y < column.Items.Count; y++)
					{
						ItemInfo item = column.Items[y];

						string line;

						if (item.GroupFlag)
							line = item.Title;
						else if (x == this.Selected_X && y == this.Selected_Y)
							line = " [>] " + item.Title;
						else
							line = " [ ] " + item.Title;

						DDPrint.SetColor(item.Color);
						DDPrint.SetBorder(item.BorderColor);
						DDPrint.PrintLine(line);
						DDPrint.Reset();
					}
				}
				DDEngine.EachFrame();
			}

			{
				ColumnInfo column = this.Columns[this.Selected_X];
				ItemInfo item = column.Items[this.Selected_Y];

				item.A_Desided();
			}

			DDEngine.FreezeInput();

			this.Columns.Clear();
		}
	}
}
