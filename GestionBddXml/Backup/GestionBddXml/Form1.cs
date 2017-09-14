using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Configuration;

namespace GestionBddXml
{
    public partial class Form1 : Form
    {
        DataSet ds = new DataSet();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                // chargement de la base
                ds.ReadXml(Properties.Settings.Default.FileDataBaseName);
                // chargement du nom des tables
                List<string> listTables = new List<string>();
                foreach (DataTable dt in ds.Tables)
                    listTables.Add(dt.TableName);
                this.comboBoxTablesNames.DataSource = listTables;
                this.comboBoxTablesNames.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace, ex.Message, MessageBoxButtons.OK);
                this.Close();
            }
        }

        private void comboBoxTablesNames_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.dataGridView1.DataSource = ds.Tables[this.comboBoxTablesNames.SelectedValue.ToString()];
            this.Refresh();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (ds != null) ds.Dispose();
        }
    }
}