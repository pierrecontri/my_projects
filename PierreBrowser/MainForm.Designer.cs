/*
 * Created by SharpDevelop.
 * User: Pierre
 * Date: 14/01/2007
 * Time: 18:38
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace Tests1
{
	partial class MainForm : System.Windows.Forms.Form
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		
		/// <summary>
		/// Disposes resources used by the form.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent()
		{
			this.flowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
			this.tabControlNav = new System.Windows.Forms.TabControl();
			this.tabNewWindow = new System.Windows.Forms.TabPage();
			this.statusStrip = new System.Windows.Forms.StatusStrip();
			this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
			this.toolStripProgressBar = new System.Windows.Forms.ToolStripProgressBar();
			this.flowLayoutPanel.SuspendLayout();
			this.tabControlNav.SuspendLayout();
			this.statusStrip.SuspendLayout();
			this.SuspendLayout();
			// 
			// flowLayoutPanel
			// 
			this.flowLayoutPanel.AutoSize = true;
			this.flowLayoutPanel.Controls.Add(this.tabControlNav);
			this.flowLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.flowLayoutPanel.Location = new System.Drawing.Point(0, 0);
			this.flowLayoutPanel.Name = "flowLayoutPanel";
			this.flowLayoutPanel.Size = new System.Drawing.Size(792, 566);
			this.flowLayoutPanel.TabIndex = 2;
			this.flowLayoutPanel.SizeChanged += new System.EventHandler(this.AjustePanelControls);
			// 
			// tabControlNav
			// 
			this.tabControlNav.Controls.Add(this.tabNewWindow);
			this.tabControlNav.Location = new System.Drawing.Point(3, 3);
			this.tabControlNav.Name = "tabControlNav";
			this.tabControlNav.SelectedIndex = 0;
			this.tabControlNav.Size = new System.Drawing.Size(457, 341);
			this.tabControlNav.TabIndex = 0;
			this.tabControlNav.Selected += new System.Windows.Forms.TabControlEventHandler(this.TabControlNavSelected);
			this.tabControlNav.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TabControlNavKeyPress);
			// 
			// tabNewWindow
			// 
			this.tabNewWindow.Location = new System.Drawing.Point(4, 22);
			this.tabNewWindow.Name = "tabNewWindow";
			this.tabNewWindow.Padding = new System.Windows.Forms.Padding(3);
			this.tabNewWindow.Size = new System.Drawing.Size(449, 315);
			this.tabNewWindow.TabIndex = 3;
			this.tabNewWindow.Text = "Nouv.";
			this.tabNewWindow.UseVisualStyleBackColor = true;
			// 
			// statusStrip
			// 
			this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.toolStripStatusLabel,
									this.toolStripProgressBar});
			this.statusStrip.Location = new System.Drawing.Point(0, 544);
			this.statusStrip.Name = "statusStrip";
			this.statusStrip.Size = new System.Drawing.Size(792, 22);
			this.statusStrip.TabIndex = 1;
			this.statusStrip.Text = "statusStrip1";
			// 
			// toolStripStatusLabel
			// 
			this.toolStripStatusLabel.Name = "toolStripStatusLabel";
			this.toolStripStatusLabel.Size = new System.Drawing.Size(0, 17);
			// 
			// toolStripProgressBar
			// 
			this.toolStripProgressBar.Margin = new System.Windows.Forms.Padding(300, 3, 1, 3);
			this.toolStripProgressBar.Name = "toolStripProgressBar";
			this.toolStripProgressBar.Size = new System.Drawing.Size(100, 16);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(792, 566);
			this.Controls.Add(this.statusStrip);
			this.Controls.Add(this.flowLayoutPanel);
			this.HelpButton = true;
			this.Name = "MainForm";
			this.Text = "Pierre\'s  Browser";
			this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			this.Paint += new System.Windows.Forms.PaintEventHandler(this.MainFormPaint);
			this.SizeChanged += new System.EventHandler(this.AjusteControls);
			this.Load += new System.EventHandler(this.MainFormLoad);
			this.flowLayoutPanel.ResumeLayout(false);
			this.tabControlNav.ResumeLayout(false);
			this.statusStrip.ResumeLayout(false);
			this.statusStrip.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();
		}
		private System.Windows.Forms.StatusStrip statusStrip;
		private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar;
		private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel;
		private System.Windows.Forms.TabPage tabNewWindow;
		private System.Windows.Forms.TabControl tabControlNav;
		private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel;
	}
}
