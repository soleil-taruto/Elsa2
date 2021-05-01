using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.GameCommons
{
	public class DDSE
	{
		public const int INIT_HANDLE_COUNT = 4;

		public bool Globally = true;
		public bool Locally { get { return !this.Globally; } }
		public DDSound Sound;
		public double Volume = 0.5; // 0.0 ～ 1.0
		public int HandleIndex = 0;

		public DDSE(string file)
			: this(new DDSound(file, INIT_HANDLE_COUNT))
		{ }

		public DDSE(Func<byte[]> getFileData)
			: this(new DDSound(getFileData, INIT_HANDLE_COUNT))
		{ }

		public DDSE(DDSound sound_binding)
		{
			this.Sound = sound_binding;
			this.Sound.PostLoaded = this.UpdateVolume_NoCheck;

			DDSEUtils.Add(this);
		}

		/// <summary>
		/// ローカル化する。
		/// 初期化時に呼び出すこと。
		/// -- 例：DDSE xxx = new DDSE("xxx.mp3").SetLocally();
		/// </summary>
		/// <returns>このインスタンス</returns>
		public DDSE SetLocally()
		{
			this.Globally = false;
			return this;
		}

		public void Play(bool once = true)
		{
			if (once)
				DDSEUtils.Play(this);
			else
				DDSEUtils.PlayLoop(this);
		}

		//public void Fade(int frameMax = 30)
		//{
		//    throw null; // 未実装
		//}

		public void Stop()
		{
			DDSEUtils.Stop(this);
		}

		public void SetVolume(double volume)
		{
			this.Volume = volume;
			this.UpdateVolume();
		}

		public void UpdateVolume()
		{
			if (this.Sound.IsLoaded())
				this.UpdateVolume_NoCheck();
		}

		public void UpdateVolume_NoCheck()
		{
			double mixedVolume = DDSoundUtils.MixVolume(DDGround.SEVolume, this.Volume);

			for (int index = 0; index < INIT_HANDLE_COUNT; index++)
				DDSoundUtils.SetVolume(this.Sound.GetHandle(index), mixedVolume);
		}

		public void Touch()
		{
			this.Sound.GetHandle(0);
		}
	}
}
