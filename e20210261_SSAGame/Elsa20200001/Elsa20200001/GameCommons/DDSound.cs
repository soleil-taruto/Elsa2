using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using DxLibDLL;
using Charlotte.Commons;

namespace Charlotte.GameCommons
{
	public class DDSound
	{
		private Func<byte[]> Func_GetFileData;
		public int HandleCount;
		private int[] Handles = null; // null == Unloaded

		public Action PostLoaded = () => { };
		public List<Action> PostLoaded2 = new List<Action>();

		public DDSound(string file, int handleCount)
			: this(() => DDResource.Load(file), handleCount)
		{ }

		public DDSound(Func<byte[]> getFileData, int initHandleCount)
		{
			this.Func_GetFileData = getFileData;
			this.HandleCount = initHandleCount;

			DDSoundUtils.Add(this);
		}

		public void Unload()
		{
			if (this.Handles != null)
			{
				foreach (int handle in this.Handles.Reverse()) // DuplicateSoundMem したハンドルから削除する。
					if (DX.DeleteSoundMem(handle) != 0) // ? 失敗
						throw new DDError();

				this.Handles = null;
			}
		}

		public bool IsLoaded()
		{
			return this.Handles != null;
		}

		public int GetHandle(int handleIndex)
		{
			if (this.Handles == null)
			{
				this.Handles = new int[this.HandleCount];

				{
					byte[] fileData = this.Func_GetFileData();
					int handle = -1;

#if false // SetLoop*SamplePosSoundMem が正常に動作しない。@ 2021.4.30
					using (WorkingDir wd = new WorkingDir())
					{
						string file = wd.MakePath();
						File.WriteAllBytes(file, fileData);
						handle = DX.LoadSoundMem(file);
					}
#else
					DDSystem.PinOn(fileData, p => handle = DX.LoadSoundMemByMemImage(p, fileData.Length)); // DxLibDotNet3_22c で正常に動作しない。@ 2021.4.18
#endif

					if (handle == -1) // ? 失敗
						throw new DDError("Sound File SHA-512: " + SCommon.Hex.ToString(SCommon.GetSHA512(fileData)));

					this.Handles[0] = handle;
				}

				for (int index = 1; index < this.HandleCount; index++)
				{
					int handle = DX.DuplicateSoundMem(this.Handles[0]);

					if (handle == -1) // ? 失敗
						throw new DDError();

					this.Handles[index] = handle;
				}

				this.PostLoaded();

				foreach (Action routine in this.PostLoaded2)
					routine();
			}
			return this.Handles[handleIndex];
		}

		public void Duplicate()
		{
			int handle = DX.DuplicateSoundMem(this.Handles[0]);

			if (handle == -1) // ? 失敗
				throw new DDError();

			this.HandleCount++;
			this.Handles = this.Handles.Concat(new int[] { handle }).ToArray();
		}

		private static bool IsPlaying(int handle)
		{
			switch (DX.CheckSoundMem(handle))
			{
				case 1: // ? 再生中
					return true;

				case 0: // ? 停止
					return false;

				default:
					throw new DDError();
			}
		}

		public bool IsPlaying()
		{
			if (this.Handles != null)
				for (int index = 0; index < this.HandleCount; index++)
					if (IsPlaying(this.Handles[index]))
						return true;

			return false;
		}
	}
}
