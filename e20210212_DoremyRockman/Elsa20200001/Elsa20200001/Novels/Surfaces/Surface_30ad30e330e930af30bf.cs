using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Commons;
using Charlotte.GameCommons;

namespace Charlotte.Novels.Surfaces
{
	public class Surface_キャラクタ : Surface
	{
		public double Draw_Rnd = DDUtils.Random.Real() * Math.PI * 2.0;

		public static string[] CHARA_NAMES = new string[]
		{
			"ゆかり",
			"マキ",
		};

		private class ImageInfo
		{
			public string Name;
			public DDPicture Image;

			public ImageInfo(string name, DDPicture image)
			{
				this.Name = name;
				this.Image = image;
			}
		}

		private ImageInfo[][] ImageTable = new ImageInfo[][]
		{
			new ImageInfo[] // ゆかり
			{
				new ImageInfo("普", Ground.I.Picture.結月ゆかり02),
				new ImageInfo("怒", Ground.I.Picture.結月ゆかり03),
			},
			new ImageInfo[] // マキ
			{
				new ImageInfo("普", Ground.I.Picture.弦巻マキ01),
				new ImageInfo("怒", Ground.I.Picture.弦巻マキ02),
			},
		};

		public int Chara = 0; // 箱
		public int Mode = 0;
		public double A = 1.0;
		public double Zoom = 1.0;
		public bool Mirrored = false;

		public Surface_キャラクタ(string typeName, string instanceName)
			: base(typeName, instanceName)
		{
			this.Z = 20000;
		}

		public override IEnumerable<bool> E_Draw()
		{
			for (; ; )
			{
				this.P_Draw();

				yield return true;
			}
		}

		private void P_Draw()
		{
			const double BASIC_ZOOM = 0.5;

			DDDraw.SetAlpha(this.A);
			DDDraw.DrawBegin(this.ImageTable[(int)this.Chara][this.Mode].Image, this.X, this.Y + Math.Sin(DDEngine.ProcFrame / 67.0 + this.Draw_Rnd) * 2.0);
			DDDraw.DrawZoom(BASIC_ZOOM * this.Zoom);
			DDDraw.DrawZoom_X(this.Mirrored ? -1 : 1);
			DDDraw.DrawEnd();
			DDDraw.Reset();
		}

		protected override void Invoke_02(string command, params string[] arguments)
		{
			int c = 0;

			if (command == "Chara")
			{
				this.Act.AddOnce(() =>
				{
					string charaName = arguments[c++];
					int chara = SCommon.IndexOf(CHARA_NAMES, charaName);

					if (chara == -1)
						throw new DDError("Bad chara: " + charaName);

					this.Chara = chara;
				});
			}
			else if (command == "Mode")
			{
				this.Act.AddOnce(() =>
				{
					string modeName = arguments[c++];
					int mode = SCommon.IndexOf(this.ImageTable[this.Chara], v => v.Name == modeName);

					if (mode == -1)
						throw new DDError("Bad mode: " + mode);

					this.Mode = mode;
				});
			}
			else if (command == "A")
			{
				this.Act.AddOnce(() => this.A = double.Parse(arguments[c++]));
			}
			else if (command == "Zoom")
			{
				this.Act.AddOnce(() => this.Zoom = double.Parse(arguments[c++]));
			}
			else if (command == "Mirror")
			{
				this.Act.AddOnce(() => this.Mirrored = int.Parse(arguments[c++]) != 0);
			}
			else if (command == "待ち")
			{
				int frame = int.Parse(arguments[c++]);

				this.Act.Add(SCommon.Supplier(this.待ち(frame)));
			}
			else if (command == "フェードイン")
			{
				this.Act.Add(SCommon.Supplier(this.フェードイン()));
			}
			else if (command == "フェードアウト")
			{
				this.Act.Add(SCommon.Supplier(this.フェードアウト()));
			}
			else if (command == "モード変更")
			{
				string modeName = arguments[c++];

				this.Act.Add(SCommon.Supplier(this.モード変更(modeName)));
			}
			else if (command == "モード変更_Mirror")
			{
				string modeName = arguments[c++];
				bool mirrored = int.Parse(arguments[c++]) != 0;

				this.Act.Add(SCommon.Supplier(this.モード変更(modeName, mirrored)));
			}
			else if (command == "スライド")
			{
				double x = double.Parse(arguments[c++]);
				double y = double.Parse(arguments[c++]);

				this.Act.Add(SCommon.Supplier(this.スライド(x, y)));
			}
			else
			{
				ProcMain.WriteLog("不明なコマンド：" + command);
				throw new DDError();
			}
		}

		private IEnumerable<bool> 待ち(int frame)
		{
			foreach (DDScene scene in DDSceneUtils.Create(frame))
			{
				if (NovelAct.IsFlush)
					break;

				this.P_Draw();
				yield return true;
			}
		}

		private IEnumerable<bool> フェードイン()
		{
			foreach (DDScene scene in DDSceneUtils.Create(10))
			{
				if (NovelAct.IsFlush)
				{
					this.A = 1.0;
					break;
				}
				this.A = scene.Rate;
				this.P_Draw();

				yield return true;
			}
		}

		private IEnumerable<bool> フェードアウト()
		{
			foreach (DDScene scene in DDSceneUtils.Create(10))
			{
				if (NovelAct.IsFlush)
				{
					this.A = 0.0;
					break;
				}
				this.A = 1.0 - scene.Rate;
				this.P_Draw();

				yield return true;
			}
		}

		private IEnumerable<bool> モード変更(string modeName)
		{
			return this.モード変更(modeName, this.Mirrored);
		}

		private IEnumerable<bool> モード変更(string modeName, bool mirrored)
		{
			int mode = SCommon.IndexOf(this.ImageTable[this.Chara], v => v.Name == modeName);

			if (mode == -1)
				throw new DDError("Bad mode: " + mode);

			return this.モード変更(mode, mirrored);
		}

		private IEnumerable<bool> モード変更(int destMode, bool destMirrored)
		{
			int currMode = this.Mode;
			bool currMirrored = this.Mirrored;

			foreach (DDScene scene in DDSceneUtils.Create(30))
			{
				if (NovelAct.IsFlush)
				{
					this.A = 1.0;
					this.Mode = destMode;
					this.Mirrored = destMirrored;
					break;
				}
				this.A = DDUtils.Parabola(scene.Rate * 0.5 + 0.5);
				this.Mode = currMode;
				this.Mirrored = currMirrored;
				this.P_Draw();

				this.A = DDUtils.Parabola(scene.Rate * 0.5 + 0.0);
				this.Mode = destMode;
				this.Mirrored = destMirrored;
				this.P_Draw();

				yield return true;
			}
		}

		private IEnumerable<bool> スライド(double destX, double destY)
		{
			double currX = this.X;
			double currY = this.Y;

			foreach (DDScene scene in DDSceneUtils.Create(30))
			{
				if (NovelAct.IsFlush)
				{
					this.X = destX;
					this.Y = destY;
					break;
				}
				this.X = DDUtils.AToBRate(currX, destX, DDUtils.SCurve(scene.Rate));
				this.Y = DDUtils.AToBRate(currY, destY, DDUtils.SCurve(scene.Rate));
				this.P_Draw();

				yield return true;
			}
		}
	}
}
