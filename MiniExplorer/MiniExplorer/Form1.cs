using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MiniExplorer
{
    public partial class MiniExplorer : Form
    {
        public MiniExplorer()
        {
            InitializeComponent();
        }

        private void MiniExplorer_Load(object sender, EventArgs e)
        {
            textBox1.Text = "C:\\";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.ShowDialog();
            textBox1.Text = folderBrowserDialog1.SelectedPath;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (webBrowser1 != null)
                webBrowser1.Url = new System.Uri(textBox1.Text);
        }
    }
}
