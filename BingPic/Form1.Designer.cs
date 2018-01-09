namespace BingPic
{
	partial class Form1
	{
		/// <summary>
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// 清理所有正在使用的资源。
		/// </summary>
		/// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows 窗体设计器生成的代码

		/// <summary>
		/// 设计器支持所需的方法 - 不要修改
		/// 使用代码编辑器修改此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
			this.button_Start = new System.Windows.Forms.Button();
			this.btn_toolTip = new System.Windows.Forms.ToolTip(this.components);
			this.Bing_notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
			this.notifyIcon_contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.hide_toolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.show_toolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.exit_toolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.changeTime_toolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.label_Version = new System.Windows.Forms.Label();
			this.label_Version_Value = new System.Windows.Forms.Label();
			this.button_Stop = new System.Windows.Forms.Button();
			this.notifyIcon_contextMenuStrip.SuspendLayout();
			this.SuspendLayout();
			// 
			// button_Start
			// 
			this.button_Start.AutoSize = true;
			this.button_Start.Location = new System.Drawing.Point(35, 75);
			this.button_Start.Name = "button_Start";
			this.button_Start.Size = new System.Drawing.Size(98, 51);
			this.button_Start.TabIndex = 0;
			this.button_Start.Text = "开始壁纸切换";
			this.btn_toolTip.SetToolTip(this.button_Start, "自动下载Bing壁纸，并自动每10分钟切换一次桌面背景");
			this.button_Start.UseVisualStyleBackColor = true;
			this.button_Start.Click += new System.EventHandler(this.button_Start_Click);
			// 
			// btn_toolTip
			// 
			this.btn_toolTip.IsBalloon = true;
			// 
			// Bing_notifyIcon
			// 
			this.Bing_notifyIcon.ContextMenuStrip = this.notifyIcon_contextMenuStrip;
			this.Bing_notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("Bing_notifyIcon.Icon")));
			this.Bing_notifyIcon.Text = "Bing壁纸";
			this.Bing_notifyIcon.Visible = true;
			this.Bing_notifyIcon.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Bing_notifyIcon_MouseClick);
			// 
			// notifyIcon_contextMenuStrip
			// 
			this.notifyIcon_contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.hide_toolStripMenuItem,
            this.show_toolStripMenuItem,
            this.changeTime_toolStripMenuItem,
            this.exit_toolStripMenuItem});
			this.notifyIcon_contextMenuStrip.Name = "notifyIcon_contextMenuStrip";
			this.notifyIcon_contextMenuStrip.Size = new System.Drawing.Size(153, 114);
			// 
			// hide_toolStripMenuItem
			// 
			this.hide_toolStripMenuItem.Name = "hide_toolStripMenuItem";
			this.hide_toolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.hide_toolStripMenuItem.Text = "隐藏界面";
			this.hide_toolStripMenuItem.Click += new System.EventHandler(this.hide_toolStripMenuItem_Click);
			// 
			// show_toolStripMenuItem
			// 
			this.show_toolStripMenuItem.Name = "show_toolStripMenuItem";
			this.show_toolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.show_toolStripMenuItem.Text = "显示界面";
			this.show_toolStripMenuItem.Click += new System.EventHandler(this.show_toolStripMenuItem_Click);
			// 
			// exit_toolStripMenuItem
			// 
			this.exit_toolStripMenuItem.Name = "exit_toolStripMenuItem";
			this.exit_toolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.exit_toolStripMenuItem.Text = "退出程序";
			this.exit_toolStripMenuItem.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
			this.exit_toolStripMenuItem.Click += new System.EventHandler(this.exit_toolStripMenuItem_Click);
			// 
			// changeTime_toolStripMenuItem
			// 
			this.changeTime_toolStripMenuItem.Name = "changeTime_toolStripMenuItem";
			this.changeTime_toolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.changeTime_toolStripMenuItem.Text = "设置切换时间";
			this.changeTime_toolStripMenuItem.Click += new System.EventHandler(this.changeTime_toolStripMenuItem_Click);
			// 
			// label_Version
			// 
			this.label_Version.AutoSize = true;
			this.label_Version.Location = new System.Drawing.Point(0, 264);
			this.label_Version.Name = "label_Version";
			this.label_Version.Size = new System.Drawing.Size(77, 12);
			this.label_Version.TabIndex = 1;
			this.label_Version.Text = "当前版本号：";
			// 
			// label_Version_Value
			// 
			this.label_Version_Value.AutoSize = true;
			this.label_Version_Value.Location = new System.Drawing.Point(69, 264);
			this.label_Version_Value.Name = "label_Version_Value";
			this.label_Version_Value.Size = new System.Drawing.Size(0, 12);
			this.label_Version_Value.TabIndex = 2;
			// 
			// button_Stop
			// 
			this.button_Stop.Location = new System.Drawing.Point(186, 75);
			this.button_Stop.Name = "button_Stop";
			this.button_Stop.Size = new System.Drawing.Size(100, 51);
			this.button_Stop.TabIndex = 3;
			this.button_Stop.Text = "停止壁纸切换";
			this.button_Stop.UseVisualStyleBackColor = true;
			this.button_Stop.Click += new System.EventHandler(this.button_Stop_Click);
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(384, 285);
			this.Controls.Add(this.button_Stop);
			this.Controls.Add(this.label_Version_Value);
			this.Controls.Add(this.label_Version);
			this.Controls.Add(this.button_Start);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.Name = "Form1";
			this.Text = "Bing壁纸";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
			this.notifyIcon_contextMenuStrip.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button button_Start;
		private System.Windows.Forms.ToolTip btn_toolTip;
		private System.Windows.Forms.NotifyIcon Bing_notifyIcon;
		private System.Windows.Forms.ContextMenuStrip notifyIcon_contextMenuStrip;
		private System.Windows.Forms.ToolStripMenuItem hide_toolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem show_toolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem exit_toolStripMenuItem;
		private System.Windows.Forms.Label label_Version;
		private System.Windows.Forms.Label label_Version_Value;
		private System.Windows.Forms.Button button_Stop;
		private System.Windows.Forms.ToolStripMenuItem changeTime_toolStripMenuItem;
	}
}

