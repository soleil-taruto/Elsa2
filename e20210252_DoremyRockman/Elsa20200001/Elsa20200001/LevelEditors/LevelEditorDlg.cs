﻿using System;
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
using Charlotte.Games;

namespace Charlotte.LevelEditors
{
	// memo:
	// xxxxxxxxxx // KeepComment:@^_ConfuserElsa // NoRename:@^_ConfuserElsa
	// という行は xxxxxxxxxx の部分について難読化による単語の置き換えを行わない。(xxxxxxxxxx は任意の文字列)

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
			this.Btn再読み込み
				.ForeColor = Color.Purple; // KeepComment:@^_ConfuserElsa // NoRename:@^_ConfuserElsa
			this.Btn保存
				.ForeColor = Color.Purple; // KeepComment:@^_ConfuserElsa // NoRename:@^_ConfuserElsa

			this.Tile_L.Items.Clear();
			this.Tile_R.Items.Clear();
			this.Enemy.Items.Clear();

			foreach (string tileName in TileCatalog.GetDisplayNames())
			{
				this.Tile_L.Items.Add(tileName);
				this.Tile_R.Items.Add(tileName);
			}
			foreach (string enemyName in EnemyCatalog.GetDisplayNames())
				this.Enemy.Items.Add(enemyName);

			this.Tile_L.SelectedIndex = 0;
			this.Tile_R.SelectedIndex = 0;
			this.Enemy.SelectedIndex = 0;

			this.SetMode(LevelEditor.Mode_e.TILE);

			this.TileEnemySw
				.Focus(); // KeepComment:@^_ConfuserElsa // NoRename:@^_ConfuserElsa
		}

		public string GetTile_L()
		{
			return TileCatalog.GetNames()[this.Tile_L.SelectedIndex];
		}

		public string GetTile_R()
		{
			return TileCatalog.GetNames()[this.Tile_R.SelectedIndex];
		}

		public string GetEnemy()
		{
			return EnemyCatalog.GetNames()[this.Enemy.SelectedIndex];
		}

		public void SetTile_L(string tileName)
		{
			int index = SCommon.IndexOf(TileCatalog.GetNames(), tileName);

			if (index == -1)
				index = 0; // 2bs

			this.Tile_L.SelectedIndex = index;
		}

		public void SetTile_R(string tileName)
		{
			int index = SCommon.IndexOf(TileCatalog.GetNames(), tileName);

			if (index == -1)
				index = 0; // 2bs

			this.Tile_R.SelectedIndex = index;
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

		private const string TEXT_MODE_TILE = "タイル";
		private const string TEXT_MODE_ENEMY = "敵 / イベントオブジェクト";

		public LevelEditor.Mode_e GetMode()
		{
			return this.TileEnemySw.Text == TEXT_MODE_TILE ? LevelEditor.Mode_e.TILE : LevelEditor.Mode_e.ENEMY;
		}

		private void SetMode(LevelEditor.Mode_e mode)
		{
			Color COLOR_SELECTED = Color
				.Blue; // KeepComment:@^_ConfuserElsa // NoRename:@^_ConfuserElsa
			Color COLOR_UNSELECTED = Color
				.Gray; // KeepComment:@^_ConfuserElsa // NoRename:@^_ConfuserElsa

			if (mode == LevelEditor.Mode_e.TILE)
			{
				this.TileEnemySw.Text = TEXT_MODE_TILE;

				this.GroupTile
					.ForeColor = // KeepComment:@^_ConfuserElsa // NoRename:@^_ConfuserElsa
						COLOR_SELECTED;
				this.GroupEnemy
					.ForeColor = // KeepComment:@^_ConfuserElsa // NoRename:@^_ConfuserElsa
						COLOR_UNSELECTED;
			}
			else
			{
				this.TileEnemySw.Text = TEXT_MODE_ENEMY;

				this.GroupTile
					.ForeColor = // KeepComment:@^_ConfuserElsa // NoRename:@^_ConfuserElsa
						COLOR_UNSELECTED;
				this.GroupEnemy
					.ForeColor = // KeepComment:@^_ConfuserElsa // NoRename:@^_ConfuserElsa
						COLOR_SELECTED;
			}
		}

		private void TileEnemySw_Click(object sender, EventArgs e)
		{
			if (this.TileEnemySw.Text == TEXT_MODE_TILE)
				this.SetMode(LevelEditor.Mode_e.ENEMY);
			else
				this.SetMode(LevelEditor.Mode_e.TILE);
		}

		private void Tile_L_Click(object sender, EventArgs e)
		{
			this.SetMode(LevelEditor.Mode_e.TILE);
		}

		private void Tile_R_Click(object sender, EventArgs e)
		{
			this.SetMode(LevelEditor.Mode_e.TILE);
		}

		private void Enemy_Click(object sender, EventArgs e)
		{
			this.SetMode(LevelEditor.Mode_e.ENEMY);
		}

		private void Btn再読み込み_Click(object sender, EventArgs e)
		{
			if (MessageBox.Show(
				"再読み込みします。",
				"確認 - 再読み込み",
				MessageBoxButtons.OKCancel, // KeepComment:@^_ConfuserElsa // NoRename:@^_ConfuserElsa
				MessageBoxIcon.Information // KeepComment:@^_ConfuserElsa // NoRename:@^_ConfuserElsa
				)
				== DialogResult.OK // KeepComment:@^_ConfuserElsa // NoRename:@^_ConfuserElsa
				)
				Game.I.Map.Load();
		}

		private void Btn保存_Click(object sender, EventArgs e)
		{
			if (MessageBox.Show(
				"保存します。",
				"確認 - 保存",
				MessageBoxButtons.OKCancel, // KeepComment:@^_ConfuserElsa // NoRename:@^_ConfuserElsa
				MessageBoxIcon.Information // KeepComment:@^_ConfuserElsa // NoRename:@^_ConfuserElsa
				)
				== DialogResult.OK // KeepComment:@^_ConfuserElsa // NoRename:@^_ConfuserElsa
				)
				Game.I.Map.Save();
		}

		private void GroupTile_Enter(object sender, EventArgs e)
		{
			// noop
		}

		private void Tile_L_SelectedIndexChanged(object sender, EventArgs e)
		{
			// noop
		}

		private void Tile_R_SelectedIndexChanged(object sender, EventArgs e)
		{
			// noop
		}

		private void GroupEnemy_Enter(object sender, EventArgs e)
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
