﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Security.Permissions;
using System.Windows.Forms;
using Charlotte.GameCommons;

namespace Charlotte
{
	public partial class LiteStatusDlg : Form
	{
		#region ALT_F4 抑止

		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		protected override void WndProc(ref Message m)
		{
			const int WM_SYSCOMMAND = 0x112;
			const long SC_CLOSE = 0xF060L;

			if (m.Msg == WM_SYSCOMMAND && (m.WParam.ToInt64() & 0xFFF0L) == SC_CLOSE)
				return;

			base.WndProc(ref m);
		}

		#endregion

		private static LiteStatusDlg Dlg = null;

		public static void StartDisplay(string message)
		{
			EndDisplay();

			MainWin.I.BeginInvoke((MethodInvoker)delegate
			{
				Dlg = new LiteStatusDlg();
				Dlg.Prm_StatusMessage = message;
				Dlg.Show();
			});
		}

		public static void EndDisplayDelay()
		{
			const int DELAY_FRAME = 30;
			int endFrame = DDEngine.ProcFrame + DELAY_FRAME;

			DDGround.SystemTasks.Add(() =>
			{
				if (DDEngine.ProcFrame < endFrame)
					return true;

				EndDisplay();
				return false;
			});
		}

		public static void EndDisplay()
		{
			MainWin.I.BeginInvoke((MethodInvoker)delegate
			{
				if (Dlg != null)
				{
					Dlg.Close();
					Dlg.Dispose(); // 2bs -- .Show() の場合は Dispose 不要である。
					Dlg = null;
				}
			});
		}

		private string Prm_StatusMessage;

		public LiteStatusDlg()
		{
			InitializeComponent();

			float fontSize;

			if (
				DDGround.MonitorRect.W < 1920 ||
				DDGround.MonitorRect.H < 1080
				)
				fontSize = 24f;
			else
				fontSize = 48f;

			this.BackColor = Color.FromArgb(64, 0, 0); // KeepComment:@^_ConfuserElsa // NoRename:@^_ConfuserElsa
			this.FormBorderStyle = FormBorderStyle.None; // KeepComment:@^_ConfuserElsa // NoRename:@^_ConfuserElsa
			this.StatusMessage
				.Font = new Font( // KeepComment:@^_ConfuserElsa // NoRename:@^_ConfuserElsa
					"メイリオ",
					fontSize
					);
			this.StatusMessage
				.ForeColor = Color.White; // KeepComment:@^_ConfuserElsa // NoRename:@^_ConfuserElsa
		}

		private void LiteStatusDlg_Load(object sender, EventArgs e)
		{
			// noop
		}

		private void LiteStatusDlg_Shown(object sender, EventArgs e)
		{
			this.StatusMessage.Text = this.Prm_StatusMessage;

			const int MARGIN = 30;

			this.Width = // KeepComment:@^_ConfuserElsa // NoRename:@^_ConfuserElsa
				DDGround.MonitorRect.W;
			this.Height = // KeepComment:@^_ConfuserElsa // NoRename:@^_ConfuserElsa
				MARGIN + this.StatusMessage
				.Height + // KeepComment:@^_ConfuserElsa // NoRename:@^_ConfuserElsa
				MARGIN;
			this.Left = // KeepComment:@^_ConfuserElsa // NoRename:@^_ConfuserElsa
				DDGround.MonitorRect.L;
			this.Top = // KeepComment:@^_ConfuserElsa // NoRename:@^_ConfuserElsa
				(DDGround.MonitorRect.H -
				this.Height) / 2; // KeepComment:@^_ConfuserElsa // NoRename:@^_ConfuserElsa
			this.StatusMessage
				.Left = (this.Width - this // KeepComment:@^_ConfuserElsa // NoRename:@^_ConfuserElsa
				.StatusMessage
				.Width) / 2; // KeepComment:@^_ConfuserElsa // NoRename:@^_ConfuserElsa
			this.StatusMessage
				.Top = // KeepComment:@^_ConfuserElsa // NoRename:@^_ConfuserElsa
					MARGIN;
		}

		private void LiteStatusDlg_FormClosing(object sender, FormClosingEventArgs e)
		{
			// noop
		}

		private void LiteStatusDlg_FormClosed(object sender, FormClosedEventArgs e)
		{
			// noop
		}
	}
}
