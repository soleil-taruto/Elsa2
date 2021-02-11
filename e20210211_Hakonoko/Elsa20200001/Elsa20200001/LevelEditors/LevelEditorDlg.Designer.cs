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
			this.Kind = new System.Windows.Forms.ComboBox();
			this.タイルGroup.SuspendLayout();
			this.SuspendLayout();
			// 
			// タイルGroup
			// 
			this.タイルGroup.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.タイルGroup.Controls.Add(this.Kind);
			this.タイルGroup.Location = new System.Drawing.Point(12, 12);
			this.タイルGroup.Name = "タイルGroup";
			this.タイルGroup.Size = new System.Drawing.Size(360, 80);
			this.タイルGroup.TabIndex = 0;
			this.タイルGroup.TabStop = false;
			this.タイルGroup.Text = "タイル";
			this.タイルGroup.Enter += new System.EventHandler(this.タイルGroup_Enter);
			// 
			// Kind
			// 
			this.Kind.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.Kind.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.Kind.FormattingEnabled = true;
			this.Kind.Location = new System.Drawing.Point(6, 26);
			this.Kind.Name = "Kind";
			this.Kind.Size = new System.Drawing.Size(348, 28);
			this.Kind.TabIndex = 0;
			this.Kind.SelectedIndexChanged += new System.EventHandler(this.Kind_SelectedIndexChanged);
			// 
			// LevelEditorDlg
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(384, 121);
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
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.GroupBox タイルGroup;
		private System.Windows.Forms.ComboBox Kind;
	}
}