using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace TestsUnitairesGraphique
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            ds.Tables.Add("Tests");
            ds.Tables["Tests"].Columns.Add("id",typeof(int));
            ds.Tables["Tests"].Columns["id"].AutoIncrement = true;
            ds.Tables["Tests"].Columns["id"].AutoIncrementStep= 1;
            ds.Tables["Tests"].Columns.Add("name", typeof(string));
            DataRow dr = null;
            dr = ds.Tables["Tests"].NewRow();
            dr["name"] = "test1";
            ds.Tables["Tests"].Rows.Add(dr);
            dr = ds.Tables["Tests"].NewRow();
            dr["name"] = "test2";
            ds.Tables["Tests"].Rows.Add(dr);

            this.comboBox1.DataSource = ds.Tables["Tests"];
            this.comboBox1.DisplayMember = ds.Tables["Tests"].Columns["name"].ColumnName;
        }
    }
}