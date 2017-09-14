using System;

using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using CarsCostSimulator.modules;
using CarsCostSimulator.modules.Modelizing;
using CarsCostSimulator.modules.Modelizing.CarModelizing;

namespace CarsCostSimulator
{
    public partial class CarCostsSimulator : Form
    {
        private static readonly String _assemblyTypeModelized = "CarsCostSimulator.modules.Modelizing";
        private static readonly String _assemblyTypeCarModelized = "CarsCostSimulator.modules.Modelizing.CarModelizing";
        private DataSet _ds = null;
        private string _dsFileName = String.Empty;
        private BindingSource masterBindingSource = new BindingSource();
        private BindingSource detailsBindingSource = new BindingSource();

        public CarCostsSimulator(string [] args)
        {
            InitializeComponent();
            
            // initialize the combo box type by using the scheme
            this._ds = new DataSet();

            try
            {
                string dbFileScheme = Properties.Settings.Default.dbScheme;
                if (File.Exists(dbFileScheme))
                    this._ds.ReadXmlSchema(dbFileScheme);
            }
            catch (Exception /*ex*/) { }
            this.InitComboBoxModelType();

            if (args != null && args.Length > 0)
                this.dsFileName = args[0];
        }

        protected String dsFileName
        {
            get
            {
                return this._dsFileName;
            }
            set
            {
                if (File.Exists(value))
                {
                    this._dsFileName = Path.GetFullPath(value);
                    this.toolStripStatusLabel1.Text = this._dsFileName;
                }
            }
        }

        private void InitComboBoxModelType()
        {
            Assembly thisAsm = Assembly.GetExecutingAssembly();
            List<Type> types = thisAsm.GetTypes().Where(t => t.IsClass && !t.IsAbstract && _assemblyTypeModelized.Equals(t.Namespace)).ToList();

            this.objcombobox.DataSource = types;
            this.objcombobox.DisplayMember = "Name";

            if (this.objcombobox.Items.Count > 0)
                this.objcombobox.SelectedIndex = 0;
        }

        private void InitListViewTypeModel()
        {
            try
            {
                Type typeModel = (Type)this.objcombobox.SelectedItem;
                // clear list view
                this.listViewTypeModel.Columns.Clear();

                // Create dynamics columns
                foreach (PropertyInfo propInfo in typeModel.GetProperties())
                {
                    if (propInfo.GetType() is IList) continue;

                    if ("name".Equals(propInfo.Name))
                    {
                        this.listViewTypeModel.Columns.Insert(0, propInfo.Name, System.Globalization.CultureInfo.CurrentUICulture.TextInfo.ToTitleCase(propInfo.Name));
                    }
                    else
                    {
                        this.listViewTypeModel.Columns.Add(propInfo.Name, System.Globalization.CultureInfo.CurrentUICulture.TextInfo.ToTitleCase(propInfo.Name));
                    }
                }
            }
            catch (Exception /*exp*/) { }

            this.fillListViewTypeModel();
        }

        private void fillListViewTypeModel()
        {
            // clear items befor inserting news
            this.listViewTypeModel.Items.Clear();
            // get type of model which selected
            String ModelType = ((Type)this.objcombobox.SelectedItem).Name;
            Dictionary<String, ObjectModelized> dicItms = ObjectModelized.getListObjectModelizedByType(ModelType);
            foreach (String itmName in dicItms.Keys)
            {
                ListViewItem itm = new ListViewItem(itmName);
                this.listViewTypeModel.Items.Add(itm);
                ObjectModelized objContent = dicItms[itmName];
                foreach(PropertyInfo propInfo in ((Type)objContent.GetType()).GetProperties())
                {
                    ListViewItem.ListViewSubItem subItm = new ListViewItem.ListViewSubItem();
                    subItm.Name = propInfo.Name;
                    var subObj = propInfo.GetValue(objContent, null);
                    subItm.Text = (subObj == null )?"N/A":subObj.ToString();
                    itm.SubItems.Add(subItm);
                }
            }
        }

        private void FormCarCostSimulator_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.dsFileName))
                this.showValues();
        }

        private void showValues()
        {
            this._ds.Clear();
            this._ds.ReadXml(this.dsFileName, XmlReadMode.IgnoreSchema);

            this.loadDictionnaryModelTypes();

            //this.dataGridView1.DataSource = masterBindingSource;
            //this.dataGridViewWearParts.DataSource = detailsBindingSource;

            masterBindingSource.DataSource = _ds;
            masterBindingSource.DataMemberChanged += new EventHandler(masterBindingSource_DataMemberChanged);

            // initialize the model type
            this.InitListViewTypeModel();
        }

        private void loadDictionnaryModelTypes()
        {
            Assembly thisAsm = Assembly.GetExecutingAssembly();
            List<Type> types = thisAsm.GetTypes().Where(t => t.IsClass && !t.IsAbstract && _assemblyTypeModelized.Equals(t.Namespace)).ToList();
            IEnumerable<Type> lstTypes = types.Where(t => this._ds.Tables.Contains(t.Name));
            String[] lstModelType = lstTypes.Select(ty => ty.Name).ToArray();
            String[] lstModelTypeFullName = lstTypes.Select(ty => ty.FullName).ToArray();
            String[] lstAssembliesWithClass = new String[] { _assemblyTypeModelized, _assemblyTypeCarModelized };

            foreach (DataTable tmpTable in this._ds.Tables)
            {
                if (!lstModelType.Contains(tmpTable.TableName)) continue;

                // get columns list of tables
                IEnumerable<String> colsListName = (tmpTable.Columns.OfType<DataColumn>()).Select(t => t.ColumnName);

                Dictionary<String, ObjectModelized> tmpDictObjectModelized = ObjectModelized.getListObjectModelizedByType(tmpTable.TableName);
                tmpDictObjectModelized.Clear();
                foreach (DataRow dr in tmpTable.Rows)
                {
                    if (dr["name"] == null) continue;

                    Type typObj = null;
                    foreach (String tmpClassTest in lstAssembliesWithClass)
                    {
                        typObj = Type.GetType(tmpClassTest + "." + tmpTable.TableName, false);
                        if (typObj != null) break;
                    }
                    if (typObj == null) continue;

                    var objCreated = thisAsm.CreateInstance(typObj.FullName);
                    if (objCreated == null) continue;
                    foreach (PropertyInfo propInfo in (objCreated.GetType()).GetProperties())
                    {
                        if (colsListName.Contains(propInfo.Name))
                        {
                            try
                            {
                                propInfo.SetValue(objCreated, dr[propInfo.Name], null);
                            }
                            catch (Exception /*ex*/) { }
                        }
                    }
                    tmpDictObjectModelized.Add(((ObjectModelized)objCreated).name, (ObjectModelized)objCreated);
                }
            }
        }

        private void test_courbe()
        {
            // test trace courbe
            Point[] pts = new Point[] { new Point(0, 0), new Point(6, 3), new Point(6, 5), new Point(7, 7), new Point(90, 70) };
            Graphics graph = this.pictureBox1.CreateGraphics();
            graph.DrawLines(new Pen(Color.Blue), pts);

            modules.Modelizing.Car tty = new modules.Modelizing.Car("Skoda Fabia");
            tty.price = 14800.0;
            Wearpart wt1 = new Wearpart("t1");
            wt1.price = 45.0;
            tty.Wearparts.Add(wt1);
            MessageBox.Show(tty.XmlExport(0, -1));

            //XmlCarManage tty2 = new XmlCarManage(this.dsFileName, true);
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

        private void masterBindingSource_DataMemberChanged(object sender, EventArgs e)
        {
            BindingSource bindingSource = (BindingSource)sender;
            DataSet dsTmp = (DataSet)bindingSource.DataSource;
            bool viewingDetails = dsTmp.Tables.Contains(bindingSource.DataMember) && dsTmp.Tables[bindingSource.DataMember].ChildRelations.Count > 0;
            this.groupBox2.Visible = viewingDetails;
            if (viewingDetails)
            {
                detailsBindingSource.DataSource = masterBindingSource;
                detailsBindingSource.DataMember = dsTmp.Tables[bindingSource.DataMember].ChildRelations[0].RelationName;
            }
            else
                detailsBindingSource.DataSource = null;
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this._ds.WriteXml(this.dsFileName);
            this._ds.WriteXmlSchema(this.dsFileName + ".schema.xsl");
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            DataGridView tty = (DataGridView)sender;
            string tty2 = tty.SelectedRows.Count.ToString();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.openFileDialogCarSimulate.Filter = "Xml Files |*.xml|All Files (*.*)|*.*";
            this.openFileDialogCarSimulate.FilterIndex = 1;
            this.openFileDialogCarSimulate.ShowDialog();
        }

        private void openFileDialogCarSimulate_FileOk(object sender, CancelEventArgs e)
        {
            this.dsFileName = this.openFileDialogCarSimulate.FileName;
            this.showValues();
        }

        private void saveFileDialogCarSimulate_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void objcombobox_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ComboBox tmpCombo = (ComboBox)sender;
                Assembly asm = Assembly.GetExecutingAssembly();
                Type objType = asm.GetType(_assemblyTypeModelized + "." + tmpCombo.SelectedItem.ToString());
                PropertyInfo[] prop = objType.GetProperties();
            }
            catch (Exception /*ex*/) { }
            this.InitListViewTypeModel();
        }

        private void viewGraphToolStripMenuItem_Click(object sender, EventArgs e)
        {
            test_courbe();
        }

        private void objcombobox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            //if(this._ds.Tables.Contains(((Type)((ComboBox)sender).SelectedItem).Name))
            //    this.dataGridView1.DataMember = ((Type)((ComboBox)sender).SelectedItem).Name;
        }

        private void fillListViewCarCost(List<CostKm> costList)
        {
            this.listViewCarCost.Items.Clear();

            // create items list
            foreach (CostKm tmpCostKm in costList)
            {
                ListViewItem itm = new ListViewItem();
                itm.Text = tmpCostKm.km.ToString();
                ListViewItem.ListViewSubItem subItm = new ListViewItem.ListViewSubItem();
                subItm.Text = tmpCostKm.price.ToString();
                itm.SubItems.Add(subItm);
                this.listViewCarCost.Items.Add(itm);
            }
        }

        private void fillListWearPartsCar(List<Wearpart> wearparts)
        {
            this.dataGridViewWearParts.DataSource = wearparts;
        }

        private void refreshDataButtonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.showValues();
        }

        private void listViewTypeModel_ItemActivate(object sender, EventArgs e)
        {

        }

        private void listViewTypeModel_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Show the differents properties for a car
            try
            {
                ListView tmpListView = (ListView)sender;
                string tmpCarName = tmpListView.SelectedItems[0].Text;
                if ("Car".Equals(((Type)this.objcombobox.SelectedItem).Name))
                {
                    Car tmpCar = (Car) Car.ListObjectModelized[tmpCarName];
                   
                    // fill listViewCarCost
                    this.fillListViewCarCost(tmpCar.Costs);

                    // fill the wearparts list

                    //TODO: error of import wearparts
                    Wearpart wt1 = new Wearpart("t1");
                    wt1.price = 45.0;
                    tmpCar.Wearparts.Add(wt1);


                    this.fillListWearPartsCar(tmpCar.Wearparts);
                }
            }
            catch (Exception /* ex */) { }
        }
    }
}
