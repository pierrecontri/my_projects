using System;
using System.Collections.Generic;
using System.Collections;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApplication1
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static string[] listInfos = { "tty", "tty2", "tty3", "tty4", "tty5", "tty6", "tty7" };
        public ObservableCollection<DataTable> _observableCollection = new ObservableCollection<DataTable>();
        DataSet _ds = new DataSet();

        public MainWindow()
        {
            InitializeComponent();

            DataTable dt1 = new DataTable("T1");
            dt1.Columns.Add("OrderID");
            dt1.Columns.Add("CustomerID");
            dt1.Columns.Add("ProductID");
            dt1.Rows.Add("test1", "test2", "test3");

            DataTable dt2 = new DataTable("T2");
            dt2.Columns.Add("OrderID");
            dt2.Columns.Add("CustomerID");
            dt2.Columns.Add("ProductID");
            dt2.Rows.Add("test4", "test5", "test6");

            _observableCollection.Add(dt1);
            _observableCollection.Add(dt2);

            this._ds.Tables.Add(dt1);
            this._ds.Tables.Add(dt2);

            this.dataGrid1.ItemsSource = this._ds.Tables[0].DefaultView;
            this.dataGrid1.DataContext = this._ds.Tables[0];
        }

        public ObservableCollection<DataTable> _Collection
        { get { return _observableCollection; } } 

        private void buttonEnd_Click(object sender, RoutedEventArgs e)
        {

            Application.Current.Shutdown();
        }
    }
}
