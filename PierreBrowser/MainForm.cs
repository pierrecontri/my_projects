/*
 * Created by SharpDevelop.
 * User: Pierre
 * Date: 14/01/2007
 * Time: 18:38
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Tests1.FeuilleWebs;
using PierreBrowser;

namespace Tests1
{
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	public partial class MainForm
	{
		[STAThread]
		public static void Main(string[] args)
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new MainForm());
		}
		
		public MainForm()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
		}
		
		void AjusteControls(object sender, System.EventArgs e)
		{
			//Control tmpSender = (Control) sender;
			//this.flowLayoutPanel.Size = new Size(tmpSender.Width - 3, tmpSender.Height - this.statusStrip1.Height);
		}

		void AjustePanelControls(object sender, System.EventArgs e)
		{
			Control tmpSender = (Control) sender;
			this.tabControlNav.Size = new Size(tmpSender.Width - 10, tmpSender.Height - 25);
		}
		
		void WebBrowserNewWindow(object sender, System.EventArgs e)
		{
			/*MainForm popup = new MainForm();
		   	BrowserWeb oldWebBrowser = (BrowserWeb) sender;
		   	MessageBox.Show("WebBrowserNewWindow : " + oldWebBrowser.newPointerUrl.AbsoluteUri);
		   	//((BrowserWeb) popup.tabControlNav.Controls[0].Controls[0]).ChangeUrl(oldWebBrowser.Url.AbsoluteUri);
		   	popup.CreateNewWindow(oldWebBrowser.newPointerUrl.OriginalString);
		   	popup.Show();*/
		}
		
		void TabControlNavSelected(object sender, System.Windows.Forms.TabControlEventArgs e)
		{
			if(((TabControl)sender).SelectedTab.Name.Equals("tabNewWindow")) 
			{
				// création d'un nouveau onglet
				this.CreateNewWindow();
			}
		}
		
		void CreateNewWindow()
		{
			this.CreateNewWindow("http://google.fr");
		}

		void CreateNewWindow(string url)
		{
			// creer une table page en avant dernière position
			TabPage tmpTable = new TabPage();
			int tabIndex = this.tabControlNav.Controls.Count - 1;
			
			this.tabControlNav.SuspendLayout();
			tmpTable.SuspendLayout();
			this.SuspendLayout();
			
			this.tabControlNav.TabPages.Insert(tabIndex,tmpTable);
	
			// 
			// tabPage
			// 
			tmpTable.Location = new System.Drawing.Point(4, 22);
			tmpTable.Name = "tabPage" + tabIndex.ToString();
			tmpTable.Text = "Feuille " + (tabIndex + 1).ToString();
			//tmpTable.Padding = new System.Windows.Forms.Padding(3);
			tmpTable.Size = this.tabControlNav.Size;
			tmpTable.TabIndex = tabIndex;
			tmpTable.UseVisualStyleBackColor = true;
			
			//
			// navigateur
			//
			this.tabControlNav.SelectTab(tabIndex);

			// 
			// browserWeb
			// 
			BrowserWeb browserWeb = new BrowserWeb();
			tmpTable.Controls.Add(browserWeb);
			browserWeb.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
									| System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			browserWeb.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			browserWeb.Location = new System.Drawing.Point(0,0);
			browserWeb.Name = "browserWeb1";
			browserWeb.Size = tmpTable.Size;
			browserWeb.TabIndex = 0;
			browserWeb.OpenNewWindow += new EventHandler(this.WebBrowserNewWindow);
			browserWeb.TextChanged += new EventHandler(this.RenamePagesWebBrowser);
			browserWeb.ProgressBrowser += new EventHandler(this.AfficheProgression);
			//browserWeb.NavigatingBrowser += new EventHandler(this.AfficheNavigation);
			browserWeb.ChangeUrl(url);
			
			// rafraichissement
			this.tabControlNav.ResumeLayout(false);
			tmpTable.ResumeLayout(false);
			tmpTable.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();
			
			// la sélectionner
			this.tabControlNav.SelectedIndex = tabIndex;
		}
		
		void TabControlNavKeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			//MessageBox.Show((Convert.ToInt32(e.KeyChar)).ToString());
			// Ctrl + w
			if(e.KeyChar == 23)
			{
				int tabIndex = this.tabControlNav.SelectedIndex;
				this.tabControlNav.TabPages[tabIndex].Dispose();
				tabIndex = (tabIndex > 0)?tabIndex - 1:0;
				this.tabControlNav.SelectedIndex = tabIndex;
			}
			// Ctrl + n
			else if(e.KeyChar == 14)
			{
				// Nouvelle fenêtre
				this.CreateNewWindow();
			}
		}
		
		void MainFormLoad(object sender, System.EventArgs e)
		{
			this.CreateNewWindow();
		}
		
		void MainFormPaint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
		}

		void RenamePagesWebBrowser(object sender, System.EventArgs e)
		{
			Control tmpSender = (Control) sender;
			tmpSender.Parent.Text = tmpSender.Text;
		}

		void AfficheProgression(object sender, System.EventArgs e)
		{
			this.toolStripProgressBar.Value = ((BrowserWeb)sender).progression;
		}

		void AfficheNavigation(object sender, EventArgs e)
		{
			this.toolStripStatusLabel.Text = ((WebBrowserNavigatedEventArgs)e).Url.AbsoluteUri;
		}
	}
}
