using SQLAccess.model;
using SQLAccess.model.query;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SQLAccess
{
    class DatabaseManager
    {

        public List<DatabaseModel> SelectDatabaseList()
        {
            List<DatabaseModel> listOfDatabases = new List<DatabaseModel>();
            using (SqlConnection conn = new SqlConnection(SQLAccess.Properties.Settings.Default.masterConnectionString))
            {
                conn.Open();

                SqlCommand command = new SqlCommand(DatabaseQueries.databaseList, conn);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        listOfDatabases.Add(new DatabaseModel((string)reader[0], (int)reader[1], (DateTime)reader[2]));
                    }
                }
            }
            return listOfDatabases;
        }

        // Returns table schema and table names from given database
        public List<TableSchemaModel> SelectTableSchemaList(string databaseName)
        {
            List<TableSchemaModel> listOfTableSchemas = new List<TableSchemaModel>();
            using (SqlConnection conn = new SqlConnection(SQLAccess.Properties.Settings.Default.masterConnectionString))
            {
                conn.Open();

                SqlCommand command = new SqlCommand(String.Format(DatabaseQueries.schemaList, databaseName), conn);
                
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        listOfTableSchemas.Add(new TableSchemaModel((string)reader[0], (string)reader[1]));
                    }
                }
            }
            return listOfTableSchemas;
        }

        public TableModel SelectTableData(string databaseName, string tableSchema, string tableName)
        {
            List<ColumnModel> listOfColumns = new List<ColumnModel>();
            using (SqlConnection conn = new SqlConnection(SQLAccess.Properties.Settings.Default.masterConnectionString))
            {
                conn.Open();

                SqlCommand command = new SqlCommand(String.Format(DatabaseQueries.tableData, databaseName), conn);

                command.Parameters.Add(new SqlParameter("1", tableName));
                command.Parameters.Add(new SqlParameter("2", tableSchema));
                
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        try
                        {
                            listOfColumns.Add(new ColumnModel(tableName + "." + (string)reader[0], (string)reader[1], (short)reader[2], (byte)reader[3]));
                        }
                        catch (System.ArgumentException e)
                        {
                            MessageBox.Show("Type not found : " + e.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);

                        }
                    }
                }
                return new TableModel(new TableSchemaModel(tableSchema, tableName), listOfColumns);
            }
        }

        public List<RelationShipModel> RetrieveRelationShips(string database, string schema, string table)
        {
            List<RelationShipModel> listOfRelationShips = new List<RelationShipModel>();
            using (SqlConnection conn = new SqlConnection(SQLAccess.Properties.Settings.Default.masterConnectionString))
            {
                conn.Open();

                SqlCommand command = new SqlCommand(String.Format(DatabaseQueries.selectRelationShips, database, schema, table), conn);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        listOfRelationShips.Add(new RelationShipModel((string)reader[0], (string)reader[1], (string)reader[2], (string)reader[3], (string)reader[4]));
                    }
                }
            }

            return listOfRelationShips;
        }

        public RelationShipModel RetrieveRelationShip(string database, string schema, string table, string reference)
        {
            RelationShipModel relation = null;
            using (SqlConnection conn = new SqlConnection(SQLAccess.Properties.Settings.Default.masterConnectionString))
            {
                conn.Open();

                SqlCommand command = new SqlCommand(String.Format(DatabaseQueries.selectRelationShip, database, schema, table, reference), conn);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        relation = new RelationShipModel((string)reader[0], (string)reader[1], (string)reader[2], (string)reader[3], (string)reader[4]);
                    }
                }
            }

            return relation;
        }

        public DataTable RetrieveDataByQuery(Query query, int offset)
        {
            string queryString = null;
            DataTable data = new DataTable();

            try
            {
                queryString = new QueryConverter().ConvertToSQL(query, offset);

                using (SqlConnection conn = new SqlConnection(SQLAccess.Properties.Settings.Default.masterConnectionString))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(queryString, conn);

                    try
                    {
                        adapter.Fill(data);
                    } catch(SqlException e)
                    {
                        MessageBox.Show(e.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Warning);

                    }
                }
            }
            catch (ArgumentException e)
            {
                MessageBox.Show(e.Message, "Confirmation", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

            return data;
        }

    }
}
