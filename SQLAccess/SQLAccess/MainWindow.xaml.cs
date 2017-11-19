using SQLAccess.model;
using System;
using System.Collections.Generic;
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

namespace SQLAccess
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DatabaseManager databaseManager;


        #region Constructor
        public MainWindow()
        {
            InitializeComponent();
            this.databaseManager = new DatabaseManager();
        }
        #endregion

        #region On Loaded
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            List<DatabaseModel> databases = this.databaseManager.selectDatabaseList();

            foreach (var database in databases)
            {
                var item = new TreeViewItem()
                {
                    Header = database.Name,
                    Tag = database.Name
                };

                item.Items.Add(null);

                //Listen for item event - expand
                item.Expanded += Database_Expanded;

                DatabaseView.Items.Add(item);
            }
        }

        private void Database_Expanded(object sender, RoutedEventArgs e)
        {
            var item = (TreeViewItem)sender;

            // If the item only contains dummy data
            if (item.Items.Count != 1 && item.Items[0] != null)
                return;

            //Clear dummy data
            item.Items.Clear();

            var databaseName = (string)item.Tag;

            //Get schemas and tables
            List<TableSchemaModel> schemas = this.databaseManager.selectTableSchemaList(databaseName);

            foreach(var schema in schemas)
            {
                var sbt = new TreeViewItem()
                {
                    Header = schema.Schema + "." + schema.TableName,
                    Tag = schema.TableName
                };
               
                //Handle expanding
                sbt.MouseDoubleClick += Table_Clicked;

                item.Items.Add(sbt);
            }
        }

        private void Table_Clicked(object sender, RoutedEventArgs e)
        {
            
        }

        #endregion
    }
}
