namespace Charlotte.LevelEditors
{
	partial class LevelEditorDlg
	{
		/// <summary>
		/// 必要なデザイナー変数です。
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// 使用中のリソースをすべてクリーンアップします。
		/// </summary>
		/// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows フォーム デザイナーで生成されたコード

		/// <summary>
		/// デザイナー サポートに必要なメソッドです。このメソッドの内容を
		/// コード エディターで変更しないでください。
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LevelEditorDlg));
			this.タイルGroup = new System.Windows.Forms.GroupBox();
			this.Tile = new System.Windows.Forms.ComboBox();
			this.敵Group = new System.Windows.Forms.GroupBox();
			this.Enemy = new System.Windows.Forms.ComboBox();
			this.ShowTile = new System.Windows.Forms.CheckBox();
			this.ShowEnemy = new System.Windows.Forms.CheckBox();
			this.TileEnemySw = new System.Windows.Forms.Button();
			this.タイルGroup.SuspendLayout();
			this.敵Group.SuspendLayout();
			this.SuspendLayout();
			// 
			// タイルGroup
			// 
			this.タイルGroup.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.タイルGroup.Controls.Add(this.Tile);
			this.タイルGroup.Location = new System.Drawing.Point(12, 12);
			this.タイルGroup.Name = "タイルGroup";
			this.タイルGroup.Size = new System.Drawing.Size(360, 80);
			this.タイルGroup.TabIndex = 0;
			this.タイルGroup.TabStop = false;
			this.タイルGroup.Text = "タイル";
			this.タイルGroup.Enter += new System.EventHandler(this.タイルGroup_Enter);
			// 
			// Tile
			// 
			this.Tile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.Tile.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.Tile.FormattingEnabled = true;
			this.Tile.Location = new System.Drawing.Point(6, 26);
			this.Tile.Name = "Tile";
			this.Tile.Size = new System.Drawing.Size(348, 28);
			this.Tile.TabIndex = 0;
			this.Tile.SelectedIndexChanged += new System.EventHandler(this.Tile_SelectedIndexChanged);
			this.Tile.Click += new System.EventHandler(this.Tile_Click);
			// 
			// 敵Group
			// 
			this.敵Group.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.敵Group.Controls.Add(this.Enemy);
			this.敵Group.Location = new System.Drawing.Point(12, 98);
			this.敵Group.Name = "敵Group";
			this.敵Group.Size = new System.Drawing.Size(360, 80);
			this.敵Group.TabIndex = 1;
			this.敵Group.TabStop = false;
			this.敵Group.Text = "敵 / イベントオブジェクト";
			this.敵Group.Enter += new System.EventHandler(this.敵Group_Enter);
			// 
			// Enemy
			// 
			this.Enemy.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.Enemy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.Enemy.FormattingEnabled = true;
			this.Enemy.Location = new System.Drawing.Point(6, 26);
			this.Enemy.Name = "Enemy";
			this.Enemy.Size = new System.Drawing.Size(348, 28);
			this.Enemy.TabIndex = 0;
			this.Enemy.SelectedIndexChanged += new System.EventHandler(this.Enemy_SelectedIndexChanged);
			this.Enemy.Click += new System.EventHandler(this.Enemy_Click);
			// 
			// ShowTile
			// 
			this.ShowTile.AutoSize = true;
			this.ShowTile.Checked = true;
			this.ShowTile.CheckState = System.Windows.Forms.CheckState.Checked;
			this.ShowTile.Location = new System.Drawing.Point(18, 184);
			this.ShowTile.Name = "ShowTile";
			this.ShowTile.Size = new System.Drawing.Size(132, 24);
			this.ShowTile.TabIndex = 2;
			this.ShowTile.Text = "タイルを表示する";
			this.ShowTile.UseVisualStyleBackColor = true;
			this.ShowTile.CheckedChanged += new System.EventHandler(this.ShowTile_CheckedChanged);
			// 
			// ShowEnemy
			// 
			this.ShowEnemy.AutoSize = true;
			this.ShowEnemy.Checked = true;
			this.ShowEnemy.CheckState = System.Windows.Forms.CheckState.Checked;
			this.ShowEnemy.Location = new System.Drawing.Point(18, 214);
			this.ShowEnemy.Name = "ShowEnemy";
			this.ShowEnemy.Size = new System.Drawing.Size(250, 24);
			this.ShowEnemy.TabIndex = 3;
			this.ShowEnemy.Text = "敵 / イベントオブジェクトを表示する";
			this.ShowEnemy.UseVisualStyleBackColor = true;
			this.ShowEnemy.CheckedChanged += new System.EventHandler(this.ShowEnemy_CheckedChanged);
			// 
			// TileEnemySw
			// 
			this.TileEnemySw.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.TileEnemySw.Location = new System.Drawing.Point(12, 244);
			this.TileEnemySw.Name = "TileEnemySw";
			this.TileEnemySw.Size = new System.Drawing.Size(360, 55);
			this.TileEnemySw.TabIndex = 4;
			this.TileEnemySw.Text = "準備しています...";
			this.TileEnemySw.UseVisualStyleBackColor = true;
			this.TileEnemySw.Click += new System.EventHandler(this.TileEnemySw_Click);
			// 
			// LevelEditorDlg
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(384, 311);
			this.Controls.Add(this.TileEnemySw);
			this.Controls.Add(this.ShowEnemy);
			this.Controls.Add(this.ShowTile);
			this.Controls.Add(this.敵Group);
			this.Controls.Add(this.タイルGroup);
			this.Font = new System.Drawing.Font("メイリオ", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "LevelEditorDlg";
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.Text = "Editor";
			this.TopMost = true;
			this.Load += new System.EventHandler(this.LevelEditorDlg_Load);
			this.Shown += new System.EventHandler(this.LevelEditorDlg_Shown);
			this.タイルGroup.ResumeLayout(false);
			this.敵Group.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.GroupBox タイルGroup;
		private System.Windows.Forms.ComboBox Tile;
		private System.Windows.Forms.GroupBox 敵Group;
		private System.Windows.Forms.ComboBox Enemy;
		private System.Windows.Forms.CheckBox ShowTile;
		private System.Windows.Forms.CheckBox ShowEnemy;
		private System.Windows.Forms.Button TileEnemySw;
	}
}