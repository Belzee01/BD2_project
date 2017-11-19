using SQLAccess.model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static SQLAccess.Utils;

namespace SQLAccess
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DatabaseManager databaseManager;

        private string currentDataBase = "";
        private string currentSchema = "";
        private string currentTable = "";

        private List<ColumnModel> tableData;
        public ICollectionView Customers { get; private set; }

        #region Constructor
        public MainWindow()
        {
            InitializeComponent();
            this.databaseManager = new DatabaseManager();
            this.tableData = new List<ColumnModel>();
        }
        #endregion

        #region On Loaded
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            List<DatabaseModel> databases = this.databaseManager.selectDatabaseList();

            foreach (var database in databases)
            {
                var item = new TreeViewItem()
                {
                    Header = database.Name,
                    Tag = DB_TYPE.DATABASE
                };

                item.Items.Add(null);

                //Listen for item event - expand
                item.Expanded += Database_Expanded;

                DatabaseView.Items.Add(item);
            }
        }
        #endregion

        #region Database Expanded event
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Database_Expanded(object sender, RoutedEventArgs e)
        {
            var item = (TreeViewItem)sender;

            // If the item only contains dummy data
            if (item.Items.Count != 1 && item.Items[0] != null)
                return;

            //Clear dummy data
            item.Items.Clear();

            var databaseName = (string)item.Header;
            this.currentDataBase = databaseName;

            //Get schemas and tables
            List<TableSchemaModel> schemas = this.databaseManager.selectTableSchemaList(databaseName);

            foreach (var schema in schemas)
            {
                var sbt = new TreeViewItem()
                {
                    Header = schema,
                    Tag = DB_TYPE.TABLE
                };

                //Handle double click
                sbt.MouseDoubleClick += Table_Clicked;

                item.Items.Add(sbt);
            }
        }
        #endregion

        #region Table Double Clicked
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Table_Clicked(object sender, RoutedEventArgs e)
        {
            var item = (TreeViewItem)sender;

            var tableName = (TableSchemaModel)item.Header;

            this.currentSchema = tableName.Schema;
            this.currentTable = tableName.TableName;

            TableModel tableModel = this.databaseManager.selectTableData(this.currentDataBase, this.currentSchema, this.currentTable);

            ColumnDatGrid.DataContext = tableModel.Columns;
            ColumnDatGrid2.DataContext = tableModel.Columns;

        }

        #endregion
    }
}
