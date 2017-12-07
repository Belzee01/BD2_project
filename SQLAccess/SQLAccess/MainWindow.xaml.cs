using SQLAccess.model;
using SQLAccess.model.query;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using static SQLAccess.Utils;

namespace SQLAccess
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DatabaseManager databaseManager;

        private string currentDatabase = "";
        private string currentSchema = "";
        private string currentTable = "";

        private Int32 offsetDown = 0;

        private List<CompactConstraintModel> queryModels;

        private List<ColumnModel> tableData;
        private DataTable schemas;
        public ICollectionView Customers { get; private set; }

        #region Constructor
        public MainWindow()
        {
            InitializeComponent();
            this.databaseManager = new DatabaseManager();
            this.tableData = new List<ColumnModel>();
            this.queryModels = new List<CompactConstraintModel>();
            this.schemas = new DataTable();
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
            List<DatabaseModel> databases = this.databaseManager.SelectDatabaseList();

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
            this.currentDatabase = databaseName;

            //Get schemas and tables
            List<TableSchemaModel> schemas = this.databaseManager.SelectTableSchemaList(databaseName);

            foreach (var schema in schemas)
            {
                var sbt = new TreeViewItem()
                {
                    Header = schema,
                    Tag = DB_TYPE.TABLE
                };

                //Handle double click
                sbt.MouseDoubleClick += Table_Clicked;
                sbt.MouseRightButtonDown += Table_RightClick;

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

            TableModel tableModel = this.databaseManager.SelectTableData(this.currentDatabase, this.currentSchema, this.currentTable);
            queryModels = new List<CompactConstraintModel>();

            foreach (var column in tableModel.Columns)
            {
                queryModels.Add(new CompactConstraintModel(column, new ConstraintModel()));
            }

            ColumnDatGrid.DataContext = queryModels;

            List<RelationShipModel> currentRelationShips = databaseManager.RetrieveRelationShips(
                this.currentDatabase,
                this.currentSchema,
                this.currentTable);

            var p = (TreeViewItem)item.Parent;
            foreach(TreeViewItem i in p.Items)
            {
                i.FontWeight = FontWeights.Normal;
                foreach(var r in currentRelationShips)
                {
                    if(i.Header.ToString().Split('.')[1] == r.Refrenced)
                    {
                        Console.WriteLine(r.Refrenced);
                        i.FontWeight = FontWeights.ExtraBold;
                    }
                }
            }
        }

        private void Table_RightClick(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("JOINING");
        }

        #endregion

        private void RunQueryButton_Click(object sender, RoutedEventArgs e)
        {
            this.offsetDown = 0;
            this.schemas.Clear();
            Query query = Query.Builder()
                .Database(this.currentDatabase)
                .Schema(this.currentSchema)
                .Table(this.currentTable)
                .Columns(this.queryModels)
                .Build();

            this.schemas = this.databaseManager.RetrieveDataByQuery(query, this.offsetDown);

            QueryBlock.Text = String.Format("SQL Query: \n{0}", query.CompleteQueryString);

            this.offsetDown = this.offsetDown + 100;

            ColumnDatGrid2.ItemsSource = schemas.DefaultView;
            instScroll2.ScrollToVerticalOffset(0.0);
        }

        private void instScroll_Loaded(object sender, RoutedEventArgs e)
        {
            ColumnDatGrid.AddHandler(MouseWheelEvent, new RoutedEventHandler(MyMouseWheelGrid1), true);
            ColumnDatGrid2.AddHandler(MouseWheelEvent, new RoutedEventHandler(MyMouseWheelGrid2), true);
        }

        private void MyMouseWheelGrid2(object sender, RoutedEventArgs e)
        {
            MouseWheelEventArgs eargs = (MouseWheelEventArgs)e;

            double x = (double)eargs.Delta;
            double y = instScroll2.VerticalOffset;

            //// Optimalization mechanism TODO
            if (((y / instScroll2.ScrollableHeight) * 100.0) == 100.0)
                LoadDynamicDataOffsetDown();
            /////

            instScroll2.ScrollToVerticalOffset(y - x);
        }

        private void MyMouseWheelGrid1(object sender, RoutedEventArgs e)
        {
            MouseWheelEventArgs eargs = (MouseWheelEventArgs)e;

            double x = (double)eargs.Delta;
            double y = instScroll1.VerticalOffset;

            instScroll1.ScrollToVerticalOffset(y - x);
        }

        private void LoadDynamicDataOffsetDown()
        {
            Query query = Query.Builder()
                .Database(this.currentDatabase)
                .Schema(this.currentSchema)
                .Table(this.currentTable)
                .Columns(this.queryModels)
                .Build();

            DataTable temp = this.databaseManager.RetrieveDataByQuery(query, this.offsetDown);
            QueryBlock.Text = String.Format("SQL Query: \n{0}", query.CompleteQueryString);

            this.offsetDown = this.offsetDown + 100;
            this.schemas.Merge(temp);
        }
    }
}
