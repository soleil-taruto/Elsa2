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
			this.GroupTile = new System.Windows.Forms.GroupBox();
			this.Tile_R = new System.Windows.Forms.ComboBox();
			this.Tile_L = new System.Windows.Forms.ComboBox();
			this.GroupEnemy = new System.Windows.Forms.GroupBox();
			this.Enemy = new System.Windows.Forms.ComboBox();
			this.ShowTile = new System.Windows.Forms.CheckBox();
			this.ShowEnemy = new System.Windows.Forms.CheckBox();
			this.TileEnemySw = new System.Windows.Forms.Button();
			this.Btn再読み込み = new System.Windows.Forms.Button();
			this.Btn保存 = new System.Windows.Forms.Button();
			this.GroupTile.SuspendLayout();
			this.GroupEnemy.SuspendLayout();
			this.SuspendLayout();
			// 
			// GroupTile
			// 
			this.GroupTile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.GroupTile.Controls.Add(this.Tile_R);
			this.GroupTile.Controls.Add(this.Tile_L);
			this.GroupTile.Location = new System.Drawing.Point(12, 58);
			this.GroupTile.Name = "GroupTile";
			this.GroupTile.Size = new System.Drawing.Size(360, 100);
			this.GroupTile.TabIndex = 2;
			this.GroupTile.TabStop = false;
			this.GroupTile.Text = "タイル (左ボタン / 右ボタン)";
			this.GroupTile.Enter += new System.EventHandler(this.GroupTile_Enter);
			// 
			// Tile_R
			// 
			this.Tile_R.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.Tile_R.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.Tile_R.FormattingEnabled = true;
			this.Tile_R.Location = new System.Drawing.Point(6, 60);
			this.Tile_R.Name = "Tile_R";
			this.Tile_R.Size = new System.Drawing.Size(348, 28);
			this.Tile_R.TabIndex = 1;
			this.Tile_R.SelectedIndexChanged += new System.EventHandler(this.Tile_R_SelectedIndexChanged);
			this.Tile_R.Click += new System.EventHandler(this.Tile_R_Click);
			// 
			// Tile_L
			// 
			this.Tile_L.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.Tile_L.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.Tile_L.FormattingEnabled = true;
			this.Tile_L.Location = new System.Drawing.Point(6, 26);
			this.Tile_L.Name = "Tile_L";
			this.Tile_L.Size = new System.Drawing.Size(348, 28);
			this.Tile_L.TabIndex = 0;
			this.Tile_L.SelectedIndexChanged += new System.EventHandler(this.Tile_L_SelectedIndexChanged);
			this.Tile_L.Click += new System.EventHandler(this.Tile_L_Click);
			// 
			// GroupEnemy
			// 
			this.GroupEnemy.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.GroupEnemy.Controls.Add(this.Enemy);
			this.GroupEnemy.Location = new System.Drawing.Point(12, 164);
			this.GroupEnemy.Name = "GroupEnemy";
			this.GroupEnemy.Size = new System.Drawing.Size(360, 70);
			this.GroupEnemy.TabIndex = 3;
			this.GroupEnemy.TabStop = false;
			this.GroupEnemy.Text = "敵 / イベントオブジェクト";
			this.GroupEnemy.Enter += new System.EventHandler(this.GroupEnemy_Enter);
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
			this.ShowTile.Location = new System.Drawing.Point(18, 250);
			this.ShowTile.Name = "ShowTile";
			this.ShowTile.Size = new System.Drawing.Size(132, 24);
			this.ShowTile.TabIndex = 4;
			this.ShowTile.Text = "タイルを表示する";
			this.ShowTile.UseVisualStyleBackColor = true;
			this.ShowTile.CheckedChanged += new System.EventHandler(this.ShowTile_CheckedChanged);
			// 
			// ShowEnemy
			// 
			this.ShowEnemy.AutoSize = true;
			this.ShowEnemy.Checked = true;
			this.ShowEnemy.CheckState = System.Windows.Forms.CheckState.Checked;
			this.ShowEnemy.Location = new System.Drawing.Point(18, 280);
			this.ShowEnemy.Name = "ShowEnemy";
			this.ShowEnemy.Size = new System.Drawing.Size(250, 24);
			this.ShowEnemy.TabIndex = 5;
			this.ShowEnemy.Text = "敵 / イベントオブジェクトを表示する";
			this.ShowEnemy.UseVisualStyleBackColor = true;
			this.ShowEnemy.CheckedChanged += new System.EventHandler(this.ShowEnemy_CheckedChanged);
			// 
			// TileEnemySw
			// 
			this.TileEnemySw.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.TileEnemySw.Location = new System.Drawing.Point(12, 319);
			this.TileEnemySw.Name = "TileEnemySw";
			this.TileEnemySw.Size = new System.Drawing.Size(360, 50);
			this.TileEnemySw.TabIndex = 6;
			this.TileEnemySw.Text = "準備しています...";
			this.TileEnemySw.UseVisualStyleBackColor = true;
			this.TileEnemySw.Click += new System.EventHandler(this.TileEnemySw_Click);
			// 
			// Btn再読み込み
			// 
			this.Btn再読み込み.Location = new System.Drawing.Point(12, 12);
			this.Btn再読み込み.Name = "Btn再読み込み";
			this.Btn再読み込み.Size = new System.Drawing.Size(120, 40);
			this.Btn再読み込み.TabIndex = 0;
			this.Btn再読み込み.Text = "再読み込み";
			this.Btn再読み込み.UseVisualStyleBackColor = true;
			this.Btn再読み込み.Click += new System.EventHandler(this.Btn再読み込み_Click);
			// 
			// Btn保存
			// 
			this.Btn保存.Location = new System.Drawing.Point(138, 12);
			this.Btn保存.Name = "Btn保存";
			this.Btn保存.Size = new System.Drawing.Size(80, 40);
			this.Btn保存.TabIndex = 1;
			this.Btn保存.Text = "保存";
			this.Btn保存.UseVisualStyleBackColor = true;
			this.Btn保存.Click += new System.EventHandler(this.Btn保存_Click);
			// 
			// LevelEditorDlg
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(384, 381);
			this.Controls.Add(this.Btn保存);
			this.Controls.Add(this.Btn再読み込み);
			this.Controls.Add(this.TileEnemySw);
			this.Controls.Add(this.ShowEnemy);
			this.Controls.Add(this.ShowTile);
			this.Controls.Add(this.GroupEnemy);
			this.Controls.Add(this.GroupTile);
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
			this.GroupTile.ResumeLayout(false);
			this.GroupEnemy.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.GroupBox GroupTile;
		private System.Windows.Forms.ComboBox Tile_L;
		private System.Windows.Forms.GroupBox GroupEnemy;
		private System.Windows.Forms.ComboBox Enemy;
		private System.Windows.Forms.CheckBox ShowTile;
		private System.Windows.Forms.CheckBox ShowEnemy;
		private System.Windows.Forms.Button TileEnemySw;
		private System.Windows.Forms.Button Btn再読み込み;
		private System.Windows.Forms.Button Btn保存;
		private System.Windows.Forms.ComboBox Tile_R;
	}
}