using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.GameCommons;
using Charlotte.Commons;

namespace Charlotte.Games.Surfaces
{
	public class Surface_キャラクタ : Surface
	{
		public double Draw_Rnd = DDUtils.Random.Real() * Math.PI * 2.0;

		public static string[] CHARA_NAMES = new string[]
		{
			"結月ゆかり",
			"東北ずん子",
			"弦巻マキ",
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
			new ImageInfo[] // 結月ゆかり
			{
				new ImageInfo("制服_珈琲", Ground.I.Picture.結月ゆかり01),
				new ImageInfo("制服_困惑", Ground.I.Picture.結月ゆかり02),
				new ImageInfo("制服_喜", Ground.I.Picture.結月ゆかり03),
				new ImageInfo("制服_驚", Ground.I.Picture.結月ゆかり04),
				new ImageInfo("制服_鶏肉", Ground.I.Picture.結月ゆかり05),
				new ImageInfo("怒", Ground.I.Picture.結月ゆかり11),
				new ImageInfo("寝", Ground.I.Picture.結月ゆかり12),
				new ImageInfo("困惑", Ground.I.Picture.結月ゆかり13),
				new ImageInfo("喜", Ground.I.Picture.結月ゆかり14),
				new ImageInfo("衝撃", Ground.I.Picture.結月ゆかり15),
				new ImageInfo("普", Ground.I.Picture.結月ゆかり16),
			},
			new ImageInfo[] // 東北ずん子
			{
				new ImageInfo("喜", Ground.I.Picture.東北ずん子01),
				new ImageInfo("怒", Ground.I.Picture.東北ずん子02),
				new ImageInfo("困惑", Ground.I.Picture.東北ずん子03),
				new ImageInfo("餅", Ground.I.Picture.東北ずん子04),
				new ImageInfo("着物_普", Ground.I.Picture.東北ずん子05),
				new ImageInfo("着物_怒", Ground.I.Picture.東北ずん子06),
				new ImageInfo("着物_なまはげ", Ground.I.Picture.東北ずん子07),
				new ImageInfo("着物_困惑", Ground.I.Picture.東北ずん子08),
				new ImageInfo("着物_疑問", Ground.I.Picture.東北ずん子09),
				new ImageInfo("激怒", Ground.I.Picture.東北ずん子10),
				new ImageInfo("メイド", Ground.I.Picture.東北ずん子11),
			},
			new ImageInfo[] // 弦巻マキ
			{
				new ImageInfo("制服_怒", Ground.I.Picture.弦巻マキ01),
				new ImageInfo("制服_激怒", Ground.I.Picture.弦巻マキ02),
				new ImageInfo("制服_泣", Ground.I.Picture.弦巻マキ03),
				new ImageInfo("普", Ground.I.Picture.弦巻マキ04),
				new ImageInfo("泣", Ground.I.Picture.弦巻マキ05),
				new ImageInfo("喜", Ground.I.Picture.弦巻マキ06),
			},
		};

		public int Chara = 0; // 結月ゆかり
		public int Mode = 0;
		public double A = 1.0;
		public double Zoom = 1.0;

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
			const double BASIC_ZOOM = 1.0;

			DDDraw.SetAlpha(this.A);
			DDDraw.DrawBegin(this.ImageTable[(int)this.Chara][this.Mode].Image, this.X, this.Y + Math.Sin(DDEngine.ProcFrame / 67.0 + this.Draw_Rnd) * 2.0);
			DDDraw.DrawZoom(BASIC_ZOOM * this.Zoom);
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
			else if (command == "待ち")
			{
				this.Act.Add(SCommon.Supplier(this.待ち(int.Parse(arguments[c++]))));
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
				this.Act.Add(SCommon.Supplier(this.モード変更(arguments[c++])));
			}
			else if (command == "スライド")
			{
				double x = double.Parse(arguments[c++]);
				double y = double.Parse(arguments[c++]);

				this.Act.Add(SCommon.Supplier(this.スライド(x, y)));
			}
			else
			{
				ProcMain.WriteLog(command);
				throw new DDError();
			}
		}

		private IEnumerable<bool> 待ち(int frame)
		{
			foreach (DDScene scene in DDSceneUtils.Create(frame))
			{
				if (Act.IsFlush)
					yield break;

				this.P_Draw();
				yield return true;
			}
		}

		private IEnumerable<bool> フェードイン()
		{
			foreach (DDScene scene in DDSceneUtils.Create(10))
			{
				if (Act.IsFlush)
				{
					this.A = 1.0;
					yield break;
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
				if (Act.IsFlush)
				{
					this.A = 0.0;
					yield break;
				}
				this.A = 1.0 - scene.Rate;
				this.P_Draw();

				yield return true;
			}
		}

		private IEnumerable<bool> モード変更(string modeName)
		{
			int mode = SCommon.IndexOf(this.ImageTable[this.Chara], v => v.Name == modeName);

			if (mode == -1)
				throw new DDError("Bad mode: " + mode);

			int currMode = this.Mode;
			int destMode = mode;

			foreach (DDScene scene in DDSceneUtils.Create(30))
			{
				if (Act.IsFlush)
				{
					this.A = 1.0;
					this.Mode = destMode;

					yield break;
				}
				this.A = DDUtils.Parabola(scene.Rate * 0.5 + 0.5);
				this.Mode = currMode;
				this.P_Draw();

				this.A = DDUtils.Parabola(scene.Rate * 0.5 + 0.0);
				this.Mode = destMode;
				this.P_Draw();

				yield return true;
			}
		}

		private IEnumerable<bool> スライド(double x, double y)
		{
			double currX = this.X;
			double destX = x;
			double currY = this.Y;
			double destY = y;

			foreach (DDScene scene in DDSceneUtils.Create(30))
			{
				if (Act.IsFlush)
				{
					this.X = destX;
					this.Y = destY;

					yield break;
				}
				this.X = DDUtils.AToBRate(currX, destX, DDUtils.SCurve(scene.Rate));
				this.Y = DDUtils.AToBRate(currY, destY, DDUtils.SCurve(scene.Rate));
				this.P_Draw();

				yield return true;
			}
		}

		protected override string[] Serialize_02()
		{
			return new string[]
			{
				this.Chara.ToString(),
				this.Mode.ToString(),
				this.A.ToString("F9"),
				this.Zoom.ToString("F9"),
			};
		}

		protected override void Deserialize_02(string[] lines)
		{
			int c = 0;

			this.Chara = int.Parse(lines[c++]);
			this.Mode = int.Parse(lines[c++]);
			this.A = double.Parse(lines[c++]);
			this.Zoom = double.Parse(lines[c++]);
		}
	}
}
