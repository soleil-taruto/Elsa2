using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.GameCommons;

namespace Charlotte.Games
{
	public class SettingMenu : IDisposable
	{
		public static SettingMenu I;

		public SettingMenu()
		{
			I = this;
		}

		public void Dispose()
		{
			I = null;
		}

		private enum Mode_e
		{
			基本設定 = 1,
			拡張設定,
			画面サイズ設定,
			ボタン設定,
			キー設定,
			END,
		}

		private Action _drawWall;
		private Mode_e _mode;

		public void Perform(Action drawWall)
		{
			_drawWall = drawWall;
			_mode = Mode_e.基本設定;

			DDEngine.FreezeInput();

			do
			{
				switch (_mode)
				{
					case Mode_e.基本設定: this.基本設定(); break;
					case Mode_e.拡張設定: this.拡張設定(); break;
					case Mode_e.画面サイズ設定: this.画面サイズ設定(); break;
					case Mode_e.ボタン設定: this.ボタン設定(); break;
					case Mode_e.キー設定: this.キー設定(); break;

					default:
						throw null; // never
				}
			}
			while (_mode != Mode_e.END);

			DDEngine.FreezeInput();
		}

		private void 基本設定()
		{
			throw null; // TODO
		}

		private void 拡張設定()
		{
			throw null; // TODO
		}

		private void 画面サイズ設定()
		{
			throw null; // TODO
		}

		private void ボタン設定()
		{
			throw null; // TODO
		}

		private void キー設定()
		{
			throw null; // TODO
		}
	}
}
