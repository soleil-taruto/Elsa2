﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Charlotte.Commons;
using Charlotte.Games;
using Charlotte.GameCommons;

namespace Charlotte
{
	public static class Common
	{
		// ====
		// BMP ここから
		// ====

		#region BMP

		private static byte[] RBF_FileData;
		private static int RBF_Reader;

		private static uint RBF_ReadValue(int width)
		{
			uint value = 0u;

			for (int index = 0; index < width; index++)
				value |= (uint)RBF_FileData[RBF_Reader++] << (index * 8);

			return value;
		}

		public static int RBF_LastColorBitCount = -1;

		public static I3Color[,] ReadBmpFile(byte[] fileData, out int xSize, out int ySize)
		{
			if (fileData == null)
				throw new ArgumentException();

			RBF_FileData = fileData;
			RBF_Reader = 0;

			// bfh
			UInt16 bfhType = (UInt16)RBF_ReadValue(2);
			RBF_ReadValue(4);
			RBF_ReadValue(4);
			RBF_ReadValue(4);

			// bfi
			RBF_ReadValue(4);
			uint bfiWidth = RBF_ReadValue(4);
			uint bfiHeight = RBF_ReadValue(4);
			RBF_ReadValue(2);
			UInt16 bfiBitCount = (UInt16)RBF_ReadValue(2);
			RBF_ReadValue(4);
			RBF_ReadValue(4);
			RBF_ReadValue(4);
			RBF_ReadValue(4);
			RBF_ReadValue(4);
			RBF_ReadValue(4);

			const uint BMP_SIGNATURE = 0x4d42; // 'B' | 'M' << 8

			if (bfhType != BMP_SIGNATURE)
				throw new Exception("Bad BMP");

			bool hiSign = (bfiHeight & 0x80000000u) != 0u;

			if (hiSign)
				throw new Exception("Bad BMP, Unsupported Y-Reverse");

			if (bfiWidth == 0u)
				throw new Exception("Bad BMP");

			if (bfiHeight == 0u)
				throw new Exception("Bad BMP");

			if ((uint)SCommon.IMAX / bfiWidth < bfiHeight)
				throw new Exception("Bad BMP");

			switch (bfiBitCount)
			{
				case 1:
				case 4:
				case 8:
					throw new Exception("Bad BMP, Unsupported Color-Pallet");

				case 24:
				case 32:
					break;

				default:
					throw new Exception("Bad BMP, Unsupported Color-Bit-Count: " + bfiBitCount);
			}

			I3Color[,] bmp = new I3Color[(int)bfiWidth, (int)bfiHeight];

			for (int y = (int)bfiHeight - 1; 0 <= y; y--)
			{
				for (int x = 0; x < (int)bfiWidth; x++)
				{
					uint cR;
					uint cG;
					uint cB;

					// BGR 注意
					cB = RBF_ReadValue(1);
					cG = RBF_ReadValue(1);
					cR = RBF_ReadValue(1);

					if (bfiBitCount == 32)
						RBF_ReadValue(1);

					bmp[x, y] = new I3Color((int)cR, (int)cG, (int)cB);
				}
				if (bfiBitCount == 24)
					RBF_ReadValue((int)(bfiWidth % 4u));
			}

			// clear
			RBF_FileData = null;
			RBF_Reader = -1;

			RBF_LastColorBitCount = bfiBitCount;

			xSize = (int)bfiWidth;
			ySize = (int)bfiHeight;

			return bmp;
		}

		private static List<byte> WBF_FileData = new List<byte>();

		private static void WBF_WriteValue(uint value)
		{
			WBF_FileData.Add((byte)(value & 0xff));
			value >>= 8;
			WBF_FileData.Add((byte)(value & 0xff));
			value >>= 8;
			WBF_FileData.Add((byte)(value & 0xff));
			value >>= 8;
			WBF_FileData.Add((byte)(value & 0xff));
		}

		public static byte[] WriteBmpFile(I3Color[,] bmp, int xSize, int ySize)
		{
			WBF_FileData = new List<byte>();

			if (bmp == null)
				throw new ArgumentException();

			if (xSize < 1)
				throw new ArgumentException();

			if (ySize < 1)
				throw new ArgumentException();

			const int PIXEL_NUM_MAX = 0x2a000000; // (int.MaxValue / 3) あたり -- sizeOfImage のため

			if (PIXEL_NUM_MAX / xSize < ySize)
				throw new ArgumentException();

			int sizeOfImage = ((xSize * 3 + 3) / 4) * 4 * ySize;

			// bfh
			WBF_FileData.Add(0x42); // 'B'
			WBF_FileData.Add(0x4d); // 'M'
			WBF_WriteValue((uint)sizeOfImage + 0x36);
			WBF_WriteValue(0);
			WBF_WriteValue(0x36);

			// bfi
			WBF_WriteValue(0x28);
			WBF_WriteValue((uint)xSize);
			WBF_WriteValue((uint)ySize);
			WBF_WriteValue(0x00180001); // Planes + BitCount
			WBF_WriteValue(0);
			WBF_WriteValue((uint)sizeOfImage);
			WBF_WriteValue(0);
			WBF_WriteValue(0);
			WBF_WriteValue(0);
			WBF_WriteValue(0);

			for (int y = ySize - 1; 0 <= y; y--)
			{
				for (int x = 0; x < xSize; x++)
				{
					I3Color color = bmp[x, y];

					// BGR 注意
					WBF_FileData.Add((byte)color.B);
					WBF_FileData.Add((byte)color.G);
					WBF_FileData.Add((byte)color.R);
				}
				for (int x = xSize % 4; 0 < x; x--)
				{
					WBF_FileData.Add(0);
				}
			}

			byte[] fileData = WBF_FileData.ToArray();
			WBF_FileData = null; // clear
			return fileData;
		}

		#endregion

		// ====
		// BMP ここまで
		// ====

		public static T GetElement<T>(T[] arr, int index, T defval)
		{
			if (index < arr.Length)
			{
				return arr[index];
			}
			else
			{
				return defval;
			}
		}

		public class ValueWrapper<T>
		{
			private Func<T> Getter;
			private Action<T> Setter;

			public ValueWrapper(Func<T> getter, Action<T> setter)
			{
				this.Getter = getter;
				this.Setter = setter;
			}

			public T Value
			{
				get
				{
					return this.Getter();
				}

				set
				{
					this.Setter(value);
				}
			}
		}

		public static void DebugPrint(object message)
		{
#if DEBUG
			string line = "_" + message;

			DDGround.EL.Add(() =>
			{
				DDPrint.Print(line);
				return false;
			});
#endif
		}

		public class OnceHandler
		{
			public bool Entered = false;

			public bool Once()
			{
				if (this.Entered)
					return false;

				this.Entered = true;
				return true;
			}
		}
	}
}
