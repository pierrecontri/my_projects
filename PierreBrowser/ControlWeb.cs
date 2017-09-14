/*
 * Created by SharpDevelop.
 * User: Pierre
 * Date: 03/02/2007
 * Time: 18:01
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace PierreBrowser
{
	/// <summary>
	/// Description of ControlWeb.
	/// </summary>
	public partial class ControlWeb
	{
		public ControlWeb()
		{
			InitializeComponent();
		}
		
		private void ControlWebSizeChanged(object sender, System.EventArgs e)
		{
			Control tmpSender = (Control) sender;
			if(tmpSender.Width > this.MinimumSize.Width)
			{
				this.comboBox.Width = this.Width - ((this.buttonValid.Width + 8) * 4);
				this.buttonValid.Left = this.Width - (this.buttonValid.Width + 3);
			}
		}
		
		private void ButtonValidClick(object sender, System.EventArgs e)
		{
			ValidUrl();
		}
		
		private void ButtonUpClick(object sender, System.EventArgs e)
		{
			// supprimer le dernier /.../.../
			string [] address1 = this.comboBox.Text.Split(new String [] {"//"},StringSplitOptions.None);
			if(address1.Length != 2) return;
			string [] address2 = address1[1].Split('/');
			if(address2.Length <= 2) return;
			string suppressString = address2[address2.Length - 2] + "/" + address2[address2.Length - 1];
			this.comboBox.Text = this.comboBox.Text.Replace("/" + suppressString, "");
			ChangeUrlEventHandler(this.comboBox, new ChangeNavigatingEventArgs(urlMessages.ChangeUrl, this.comboBox.Text));
		}
		
		private void ButtonNextClick(object sender, System.EventArgs e)
		{
			ChangeUrlEventHandler(this.comboBox, new ChangeNavigatingEventArgs(urlMessages.NextUrl, ""));
		}
		
		private void ButtonPreviousClick(object sender, System.EventArgs e)
		{
			ChangeUrlEventHandler(this.comboBox, new ChangeNavigatingEventArgs(urlMessages.PreviousUrl, ""));
		}
		
		private void ComboBoxKeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if(((Keys)e.KeyChar == Keys.Enter) && ChangeUrlEventHandler != null)
				ValidUrl();
		}
		
		private void ComboBoxSelectedValueChanged(object sender, System.EventArgs e)
		{
			if(ChangeUrlEventHandler != null)
				ValidUrl();
		}

		private void ValidUrl()
		{
			if(this.comboBox.Text != null && this.comboBox.Text != "")
			{
				if(this.comboBox.Text.IndexOf("http://")  == -1 &&
				   this.comboBox.Text.IndexOf("https://") == -1)
					   this.comboBox.Text = "http://" + this.comboBox.Text;
				
				ChangeNavigatingEventArgs evt = new ChangeNavigatingEventArgs(urlMessages.ChangeUrl, this.comboBox.Text);
				ChangeUrlEventHandler(this.comboBox, evt);
				
				if(!this.comboBox.Items.Contains(this.comboBox.Text))
					this.comboBox.Items.Add(this.comboBox.Text);
			}
		}

		public void ChangeTextUrl(String urlText)
		{
			this.comboBox.Text = urlText;
		}
	}
}
