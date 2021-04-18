using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DxLibDLL;

namespace Charlotte.GameCommons
{
	public class DDMusic
	{
		public DDSound Sound;
		public double Volume = 0.5; // 0.0 ～ 1.0

		public DDMusic(string file)
			: this(new DDSound(file, 1))
		{ }

		public DDMusic(Func<byte[]> getFileData)
			: this(new DDSound(getFileData, 1))
		{ }

		public DDMusic(DDSound sound_binding)
		{
			this.Sound = sound_binding;
			this.Sound.PostLoaded = () => DDSoundUtils.SetVolume(this.Sound.GetHandle(0), 0.0); // ロードしたらミュートしておく。

			DDMusicUtils.Add(this);
		}

		// memo: ループ開始・終了位置を探すツール --> C:\Dev\wb\t20201022_SoundLoop

		/// <summary>
		/// ループを設定する。
		/// ハンドルのロード前に呼び出すこと。
		/// </summary>
		/// <param name="loopStart">ループ開始位置(サンプル位置)</param>
		/// <param name="loopLength">ループの長さ(サンプル位置)</param>
		/// <returns>このインスタンス</returns>
		public DDMusic SetLoopByStEnd(int loopStart, int loopEnd)
		{
			this.Sound.PostLoaded2.Add(() =>
			{
				DX.SetLoopSamplePosSoundMem(loopStart, this.Sound.GetHandle(0)); // ループ開始位置
				DX.SetLoopStartSamplePosSoundMem(loopEnd, this.Sound.GetHandle(0)); // ループ終了位置
			});

			return this;
		}

		public DDMusic SetLoopByStLength(int loopStart, int loopLength)
		{
			return this.SetLoopByStEnd(loopStart, loopStart + loopLength);
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
