using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DxLibDLL;

namespace Charlotte.GameCommons
{
	public class DDMusic
	{
		public bool Globally;
		public bool Locally { get { return !this.Globally; } }
		public DDSound Sound;
		public double Volume = 0.5; // 0.0 ～ 1.0

		public DDMusic(bool globally, string file)
			: this(globally, new DDSound(file, 1))
		{ }

		public DDMusic(bool globally, Func<byte[]> getFileData)
			: this(globally, new DDSound(getFileData, 1))
		{ }

		public DDMusic(bool globally, DDSound sound_binding)
		{
			this.Globally = globally;
			this.Sound = sound_binding;
			this.Sound.PostLoaded = () => DDSoundUtils.SetVolume(this.Sound.GetHandle(0), 0.0); // ロードしたらミュートしておく。

			DDMusicUtils.Add(this);
		}

		/// <summary>
		/// ループを設定する。
		/// </summary>
		/// <param name="loopStart">ループ開始位置(サンプリング値)</param>
		/// <param name="loopLength">ループの長さ(サンプリング値)</param>
		/// <returns>このインスタンス</returns>
		public DDMusic SetLoop(int loopStart, int loopLength)
		{
			DX.SetLoopSamplePosSoundMem(loopStart, this.Sound.GetHandle(0)); // ループ開始位置
			DX.SetLoopStartSamplePosSoundMem(loopStart + loopLength, this.Sound.GetHandle(0)); // ループ終了位置

			return this;
		}

		public void Play(bool once = false, bool resume = false, double volume = 1.0, int fadeFrameMax = 30)
		{
			DDMusicUtils.Play(this, once, resume, volume, fadeFrameMax);
		}

		public void Touch()
		{
			this.Sound.GetHandle(0);
		}
	}
}
