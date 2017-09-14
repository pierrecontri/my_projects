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

namespace PierreBrowser
{
	public enum urlMessages : int
	{
		ChangeUrl   = 0,
		PreviousUrl = 1,
		NextUrl     = 2,
		UpUrl       = 3
	}

	public class ChangeNavigatingEventArgs: EventArgs
	{
	    public ChangeNavigatingEventArgs(urlMessages message, string url)
	    {
	        this.message         = message;
	        this.url             = url;
	        this.cancelEventArgs = null;
	    }
	    public urlMessages message;
	    public string url;
	    public CancelEventArgs cancelEventArgs;
	}


	partial class ControlWeb : System.Windows.Forms.UserControl
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
			this.buttonPrevious = new System.Windows.Forms.Button();
			this.buttonNext = new System.Windows.Forms.Button();
			this.buttonUp = new System.Windows.Forms.Button();
			this.comboBox = new System.Windows.Forms.ComboBox();
			this.buttonValid = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// buttonPrevious
			// 
			this.buttonPrevious.Location = new System.Drawing.Point(3, 3);
			this.buttonPrevious.Name = "buttonPrevious";
			this.buttonPrevious.Size = new System.Drawing.Size(29, 21);
			this.buttonPrevious.TabIndex = 5;
			this.buttonPrevious.Text = "<";
			this.buttonPrevious.UseVisualStyleBackColor = true;
			this.buttonPrevious.Click += new System.EventHandler(this.ButtonPreviousClick);
			// 
			// buttonNext
			// 
			this.buttonNext.Location = new System.Drawing.Point(38, 3);
			this.buttonNext.Name = "buttonNext";
			this.buttonNext.Size = new System.Drawing.Size(29, 21);
			this.buttonNext.TabIndex = 5;
			this.buttonNext.Text = ">";
			this.buttonNext.UseVisualStyleBackColor = true;
			this.buttonNext.Click += new System.EventHandler(this.ButtonNextClick);
			// 
			// buttonUp
			// 
			this.buttonUp.Location = new System.Drawing.Point(73, 3);
			this.buttonUp.Name = "buttonUp";
			this.buttonUp.Size = new System.Drawing.Size(29, 21);
			this.buttonUp.TabIndex = 5;
			this.buttonUp.Text = "^";
			this.buttonUp.UseVisualStyleBackColor = true;
			this.buttonUp.Click += new System.EventHandler(this.ButtonUpClick);
			// 
			// comboBox
			// 
			this.comboBox.FormattingEnabled = true;
			this.comboBox.Location = new System.Drawing.Point(108, 3);
			this.comboBox.Name = "comboBox";
			this.comboBox.Size = new System.Drawing.Size(427, 21);
			this.comboBox.TabIndex = 1;
			this.comboBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ComboBoxKeyPress);
			this.comboBox.SelectedValueChanged += new System.EventHandler(this.ComboBoxSelectedValueChanged);
			// 
			// buttonValid
			// 
			this.buttonValid.Location = new System.Drawing.Point(541, 3);
			this.buttonValid.Name = "buttonValid";
			this.buttonValid.Size = new System.Drawing.Size(29, 21);
			this.buttonValid.TabIndex = 2;
			this.buttonValid.Text = "Ok";
			this.buttonValid.UseVisualStyleBackColor = true;
			this.buttonValid.Click += new System.EventHandler(this.ButtonValidClick);
			// 
			// ControlWeb
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.buttonPrevious);
			this.Controls.Add(this.buttonNext);
			this.Controls.Add(this.buttonUp);
			this.Controls.Add(this.buttonValid);
			this.Controls.Add(this.comboBox);
			this.MaximumSize = new System.Drawing.Size(1000, 27);
			this.MinimumSize = new System.Drawing.Size(150, 27);
			this.Name = "ControlWeb";
			this.Size = new System.Drawing.Size(574, 27);
			this.SizeChanged += new System.EventHandler(this.ControlWebSizeChanged);
			this.ResumeLayout(false);
		}
		private System.Windows.Forms.Button buttonValid;
		private System.Windows.Forms.ComboBox comboBox;
		private System.Windows.Forms.Button buttonUp;
		private System.Windows.Forms.Button buttonNext;
		private System.Windows.Forms.Button buttonPrevious;
		public delegate void ControlNavigatingEventHandler(Object sender, ChangeNavigatingEventArgs e);
		public event ControlNavigatingEventHandler ChangeUrlEventHandler;
	}
}
