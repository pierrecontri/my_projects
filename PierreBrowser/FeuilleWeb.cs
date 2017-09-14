/*
 * Created by SharpDevelop.
 * User: Pierre
 * Date: 16/01/2007
 * Time: 21:48
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

using System;
using System.Windows.Forms;
using System.Drawing;
using System.Collections;
using System.ComponentModel;

namespace Tests1.FeuilleWebs
{

	/// <summary>
	/// Description of FeuilleWeb.
	/// </summary>
	public class FeuilleWeb : Control
	{
		private ArrayList listUrls = null;
		private ArrayList listAddress = null;
		private WebBrowser webBrowser = null;
		private ComboBox comboBox = null;
		private Button buttonValid = null;
		private FlowLayoutPanel flowLayoutPanel = null;
		public event CancelEventHandler OpenNewWindow;

		public FeuilleWeb()
		{
			this.listUrls        = new ArrayList();
			this.listAddress     = new ArrayList();
			this.comboBox        = new ComboBox();
			this.buttonValid     = new Button();
			this.webBrowser      = new WebBrowser();
			this.flowLayoutPanel = new FlowLayoutPanel();

			//
			// Me
			//
			this.Controls.Add(this.flowLayoutPanel);
			this.SizeChanged += new EventHandler(this.AjusteControls);

			//
			// flowLayoutPanel
			//
			this.flowLayoutPanel.Anchor = ((System.Windows.Forms.AnchorStyles)
			          ((((System.Windows.Forms.AnchorStyles.Top
			            | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left) 
						| System.Windows.Forms.AnchorStyles.Right)));
			this.flowLayoutPanel.Name = "flowLayoutPanel";
			this.flowLayoutPanel.Controls.Add(this.comboBox);
			this.flowLayoutPanel.Controls.Add(this.buttonValid);
			this.flowLayoutPanel.Controls.Add(this.webBrowser);
			
			//
			// listBox
			//
			this.comboBox.Location = new System.Drawing.Point(3, 3);
			this.comboBox.Name = "textBox";
			this.comboBox.Size = new System.Drawing.Size(335, 21);
			this.comboBox.TabIndex = 0;
			this.comboBox.KeyPress += new KeyPressEventHandler(this.TbUrlKeyPress);
			this.comboBox.SelectedValueChanged += new EventHandler(this.ChargeUrl);

			// 
			// buttonValid
			// 
			this.buttonValid.Location = new System.Drawing.Point(344, 3);
			this.buttonValid.Name = "buttonValid";
			this.buttonValid.Size = new System.Drawing.Size(50, 23);
			this.buttonValid.TabIndex = 1;
			this.buttonValid.Text = "Ok";
			this.buttonValid.UseVisualStyleBackColor = true;
			this.buttonValid.Click += new EventHandler(this.ValidUrl);

			// 
			// webBrowser
			// 
			this.webBrowser.Location               = new System.Drawing.Point(3, 32);
			this.webBrowser.MinimumSize            = new System.Drawing.Size(20, 20);
			this.webBrowser.Name                   = "webBrowser";
			this.webBrowser.Size                   = new System.Drawing.Size(416, 208);
			this.webBrowser.TabIndex               = 2;
			this.webBrowser.Navigated             += new System.Windows.Forms.WebBrowserNavigatedEventHandler(this.WebBrowserNavigated);
			//this.webBrowser.ProgressChanged       += new System.Windows.Forms.WebBrowserProgressChangedEventHandler(this.WebBrowserProgressChanged);
			this.webBrowser.Navigating            += new System.Windows.Forms.WebBrowserNavigatingEventHandler(this.WebBrowserNavigating);
			this.webBrowser.NewWindow             += new System.ComponentModel.CancelEventHandler(this.WebBrowserNewWindow);
			this.webBrowser.DocumentTitleChanged  += new EventHandler(this.DocumentTitleChanged);
			this.webBrowser.ScriptErrorsSuppressed = true;
			// google mis par defaut
			this.webBrowser.Url = new Uri("http://www.google.fr");
		}

		/*protected virtual void OnChanged(System.EventArgs e)
        {
            if (Changed != null)
            {
                Changed(this, e);
            }
        }*/

		private void ValidUrl(object sender, System.EventArgs e)
		{
			if(this.comboBox.Text != null && this.comboBox.Text != "")
			{
				if(this.comboBox.Text.IndexOf("http://")  == -1 &&
				   this.comboBox.Text.IndexOf("https://") == -1)
					   this.comboBox.Text = "http://" + this.comboBox.Text;
				
				this.ChargeUrl(this.comboBox, null);
				
				if(!this.comboBox.Items.Contains(this.comboBox.Text))
					this.comboBox.Items.Add(this.comboBox.Text);
			}
		}
		
		private void ChargeUrl(object sender, System.EventArgs e)
		{
			this.webBrowser.Url = new Uri(((Control)sender).Text);
			this.webBrowser.Focus();
		}

		private void TbUrlKeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if(e.KeyChar==13) ValidUrl(sender, null);
		}

		public void AjusteControls(object sender, System.EventArgs e)
		{
			System.Drawing.Size tailleParent = ((Control) sender).Size;
			// agrandir les tab
			//this.Size = tailleParent;
			//this.flowLayoutPanel.Size = this.Size;
			this.comboBox.Width = tailleParent.Width - 15 - this.buttonValid.Width;
			this.buttonValid.Left = this.comboBox.Width - this.buttonValid.Width;
			this.webBrowser.Width = tailleParent.Width - 6;
			this.webBrowser.Height = tailleParent.Height - this.comboBox.Height - 60;
		}

		private void WebBrowserNewWindow(object sender, System.ComponentModel.CancelEventArgs e)
		{
			System.Console.WriteLine(sender.ToString());
			//MainForm popup = new MainForm();
		   	WebBrowser oldWebBrowser = (WebBrowser) sender;
		   	MessageBox.Show(oldWebBrowser.WindowTarget.ToString());
		   	//popup.webBrowser.Url = oldWebBrowser.Url;
			e.Cancel = true;
		   	//popup.Show();
		   	//this.OpenNewWindow(sender,null);
		   	//OnChanged(System.EventArgs.Empty);
		   	if(OpenNewWindow != null)
		   		OpenNewWindow(this,e);
		}

		private void WebBrowserNavigated(object sender, System.Windows.Forms.WebBrowserNavigatedEventArgs e)
		{
			this.comboBox.Text = ((WebBrowser) sender).Url.AbsoluteUri;
		}

		private void WebBrowserNavigating(object sender, System.Windows.Forms.WebBrowserNavigatingEventArgs e)
		{
			//this.toolStripStatusLabel1.Text = e.Url.AbsoluteUri.ToString();
		}
		
		private void DocumentTitleChanged(object sender, EventArgs e)
		{
			this.Text = this.webBrowser.DocumentTitle;
		}

		public void ShowWeb()
		{
			this.webBrowser.Focus();
		}
	}
}
