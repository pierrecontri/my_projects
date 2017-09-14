/*
 * Created by SharpDevelop.
 * User: Pierre
 * Date: 02/02/2007
 * Time: 07:17
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

using System;
using System.Windows.Forms;
using System.ComponentModel;
using PierreBrowser;

namespace PierreBrowser
{
	partial class BrowserWeb : System.Windows.Forms.UserControl
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		
		/// <summary>
		/// Disposes resources used by the control.
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
			this.webBrowser = new System.Windows.Forms.WebBrowser();
			this.controlWeb = new PierreBrowser.ControlWeb();
			this.SuspendLayout();
			// 
			// webBrowser
			// 
			this.webBrowser.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
									| System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.webBrowser.Location = new System.Drawing.Point(3, 30);
			this.webBrowser.Name = "webBrowser";
			this.webBrowser.ScriptErrorsSuppressed = true;
			this.webBrowser.Size = new System.Drawing.Size(298, 142);
			this.webBrowser.TabIndex = 4;
			this.webBrowser.Navigated += new System.Windows.Forms.WebBrowserNavigatedEventHandler(this.WebBrowserNavigated);
			this.webBrowser.ProgressChanged += new System.Windows.Forms.WebBrowserProgressChangedEventHandler(this.WebBrowserProgressChanged);
			this.webBrowser.Navigating += new System.Windows.Forms.WebBrowserNavigatingEventHandler(this.WebBrowserNavigating);
			this.webBrowser.DocumentTitleChanged += new System.EventHandler(this.DocumentTitleChanged);
			this.webBrowser.NewWindow += new System.ComponentModel.CancelEventHandler(this.WebBrowserNewWindow);
			// 
			// controlWeb
			// 
			this.controlWeb.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.controlWeb.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.controlWeb.Location = new System.Drawing.Point(3, 3);
			this.controlWeb.MaximumSize = new System.Drawing.Size(2000, 27);
			this.controlWeb.MinimumSize = new System.Drawing.Size(50, 23);
			this.controlWeb.Name = "controlWeb";
			this.controlWeb.Size = new System.Drawing.Size(298, 27);
			this.controlWeb.TabIndex = 5;
			this.controlWeb.ChangeUrlEventHandler += new PierreBrowser.ControlWeb.ControlNavigatingEventHandler(this.ChangeUrl);
			// 
			// BrowserWeb
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.controlWeb);
			this.Controls.Add(this.webBrowser);
			this.Name = "BrowserWeb";
			this.Size = new System.Drawing.Size(304, 175);
			this.ResumeLayout(false);
		}
		private PierreBrowser.ControlWeb controlWeb;
		private System.Windows.Forms.WebBrowser webBrowser;
		public Uri newPointerUrl;
		public int progression;
		public event EventHandler OpenNewWindow;
		public event EventHandler ProgressBrowser;
		public event WebBrowserNavigatingEventHandler NavigatingBrowser;

		private void WebBrowserNewWindow(object sender, System.ComponentModel.CancelEventArgs e)
		{
			e.Cancel = true;
		   	if(OpenNewWindow != null)
		   		OpenNewWindow(this, null);
		}

		private void WebBrowserNavigated(object sender, System.Windows.Forms.WebBrowserNavigatedEventArgs e)
		{
			this.controlWeb.ChangeTextUrl(((WebBrowser) sender).Url.AbsoluteUri);
		}

		private void DocumentTitleChanged(object sender, EventArgs e)
		{
			if(!string.IsNullOrEmpty(this.webBrowser.DocumentTitle))
				this.Text = this.webBrowser.DocumentTitle;
		}

		private void ChangeUrl(object sender, ChangeNavigatingEventArgs e)
		{
			switch (e.message)
			{
				case urlMessages.ChangeUrl :
					this.webBrowser.Url = new Uri(e.url);
					break;
				case urlMessages.NextUrl :
					if(this.webBrowser.CanGoForward)
						this.webBrowser.GoForward();
					break;
				case urlMessages.PreviousUrl :
					if(this.webBrowser.CanGoBack)
						this.webBrowser.GoBack();
					break;
				default : break;
			}
		}
		
		public void ChangeUrl(string url)
		{
			this.webBrowser.Url = new Uri(url);
		}

		void WebBrowserNavigating(object sender, WebBrowserNavigatingEventArgs e)
		{
			this.newPointerUrl = e.Url;
			if(NavigatingBrowser != null)
				NavigatingBrowser(this, null);
		}
		
		void WebBrowserProgressChanged(object sender, System.Windows.Forms.WebBrowserProgressChangedEventArgs e)
		{
			this.progression = (int) ((float)(e.CurrentProgress / e.MaximumProgress) * 100);
			if(ProgressBrowser != null)
				ProgressBrowser(this, null);
		}
	}
}
