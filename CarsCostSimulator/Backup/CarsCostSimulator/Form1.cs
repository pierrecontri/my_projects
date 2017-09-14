using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CarsCostSimulator
{
    public partial class CarCostsSimulator : Form
    {
        private DataSet _ds = null;
        private string _dsFileName = "CarDatabase.xml";

        public CarCostsSimulator()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this._ds = new DataSet();
            this._ds.ReadXml(this._dsFileName);
            DataTableCollection tables = this._ds.Tables;
            ArrayList al = new ArrayList();
            al.AddRange(tables);
            this.objcombobox.DataSource = al;
            this.objcombobox.DisplayMember = "TableName";

            DataRow [] wearparts = tables["Car"].Rows[0].GetChildRows("Car_Wearpart");
            DataRelation rel = tables["Car"].ChildRelations[0];
            //MessageBox.Show(wearparts[0][0].ToString());
        }

        private void objcombobox_TextChanged(object sender, EventArgs e)
        {
            ComboBox objCombo = (ComboBox)sender;
            this.dataGridView1.DataSource = objCombo.SelectedItem;
        }

        private void savebutton_Click(object sender, EventArgs e)
        {
            modules.Modelizing.Car tty = new modules.Modelizing.Car("Skoda Fabia");
            MessageBox.Show(tty.XmlExport(0, -1));
 
            //this._ds.WriteXml(this._dsFileName);
        }

        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBoxAutor aboutBox = new AboutBoxAutor();
            aboutBox.ShowDialog();
        }

    }
}
