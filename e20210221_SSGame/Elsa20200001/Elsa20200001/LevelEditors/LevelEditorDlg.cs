using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Security.Permissions;
using Charlotte.Commons;
using Charlotte.Games.Tiles;
using Charlotte.Games.Enemies;

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
			this.Tile.Items.Clear();
			this.Enemy.Items.Clear();

			foreach (string tileName in TileCatalog.GetNames())
				this.Tile.Items.Add(tileName);

			foreach (string enemyName in EnemyCatalog.GetNames())
				this.Enemy.Items.Add(enemyName);

			this.Tile.SelectedIndex = 0;
			this.Enemy.SelectedIndex = 0;

			this.TileEnemySw.Text = TEXT_MODE_TILE;
		}

		public string GetTile()
		{
			return TileCatalog.GetNames()[this.Tile.SelectedIndex];
		}

		public string GetEnemy()
		{
			return EnemyCatalog.GetNames()[this.Enemy.SelectedIndex];
		}

		public void SetTile(string tileName)
		{
			int index = SCommon.IndexOf(TileCatalog.GetNames(), tileName);

			if (index == -1)
				index = 0; // 2bs

			this.Tile.SelectedIndex = index;
		}

		public void SetEnemy(string enemyName)
		{
			int index = SCommon.IndexOf(EnemyCatalog.GetNames(), enemyName);

			if (index == -1)
				index = 0; // 2bs

			this.Enemy.SelectedIndex = index;
		}

		public bool IsShowTile()
		{
			return this.ShowTile.Checked;
		}

		public bool IsShowEnemy()
		{
			return this.ShowEnemy.Checked;
		}

		public LevelEditor.Mode_e GetMode()
		{
			return this.TileEnemySw.Text == TEXT_MODE_TILE ? LevelEditor.Mode_e.TILE : LevelEditor.Mode_e.ENEMY;
		}

		private const string TEXT_MODE_TILE = "タイル";
		private const string TEXT_MODE_ENEMY = "敵 / イベントオブジェクト";

		private void TileEnemySw_Click(object sender, EventArgs e)
		{
			if (this.TileEnemySw.Text == TEXT_MODE_TILE)
				this.TileEnemySw.Text = TEXT_MODE_ENEMY;
			else
				this.TileEnemySw.Text = TEXT_MODE_TILE;
		}

		private void Tile_Click(object sender, EventArgs e)
		{
			this.TileEnemySw.Text = TEXT_MODE_TILE;
		}

		private void Enemy_Click(object sender, EventArgs e)
		{
			this.TileEnemySw.Text = TEXT_MODE_ENEMY;
		}

		private void タイルGroup_Enter(object sender, EventArgs e)
		{
			// noop
		}

		private void Tile_SelectedIndexChanged(object sender, EventArgs e)
		{
			// noop
		}

		private void 敵Group_Enter(object sender, EventArgs e)
		{
			// noop
		}

		private void Enemy_SelectedIndexChanged(object sender, EventArgs e)
		{
			// noop
		}

		private void ShowTile_CheckedChanged(object sender, EventArgs e)
		{
			// noop
		}

		private void ShowEnemy_CheckedChanged(object sender, EventArgs e)
		{
			// noop
		}
	}
}
