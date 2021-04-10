using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Security.Permissions;
using Charlotte.Games.Enemies;
using Charlotte.Games;

namespace Charlotte.LevelEditors
{
	public partial class LevelEditorDlg : Form
	{
		#region ALT_F4 抑止

		public bool XPressed = false;

		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		protected override void WndProc(ref Message m)
		{
			const int WM_SYSCOMMAND = 0x112;
			const long SC_CLOSE = 0xF060L;

			if (m.Msg == WM_SYSCOMMAND && (m.WParam.ToInt64() & 0xFFF0L) == SC_CLOSE)
			{
				this.XPressed = true;
				return;
			}
			base.WndProc(ref m);
		}

		#endregion

		public LevelEditorDlg()
		{
			InitializeComponent();

			this.MinimumSize = this.Size;
		}

		private void LevelEditorDlg_Load(object sender, EventArgs e)
		{
			// noop
		}

		private void LevelEditorDlg_Shown(object sender, EventArgs e)
		{
			this.Kind.Items.Clear();

			foreach (string name in MapCell.Kine_e_Names)
				this.Kind.Items.Add(name);

			this.Kind.SelectedIndex = 0;
			this.Kind.MaxDropDownItems = this.Kind.Items.Count;
		}

		public MapCell.Kind_e GetKind()
		{
			return (MapCell.Kind_e)this.Kind.SelectedIndex;
		}

		public void SetKind(MapCell.Kind_e kind)
		{
			this.Kind.SelectedIndex = (int)kind;
		}

		private void タイルGroup_Enter(object sender, EventArgs e)
		{
			// noop
		}

		private void Kind_SelectedIndexChanged(object sender, EventArgs e)
		{
			// noop
		}
	}
}
