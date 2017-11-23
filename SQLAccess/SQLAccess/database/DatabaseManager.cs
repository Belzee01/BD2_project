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

                SqlCommand command = new SqlCommand("SELECT name, database_id, create_date FROM sys.databases", conn);

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

                SqlCommand command = new SqlCommand(String.Format("SELECT TABLE_SCHEMA, TABLE_NAME FROM {0}.INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'BASE TABLE'", databaseName), conn);
                
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

                SqlCommand command = new SqlCommand(String.Format("SELECT " +
                    "AC.[name] AS[column_name]," +
                    "TY.[name] AS system_data_type, AC.[max_length]," +
                    "AC.[precision] " +
                    "FROM {0}.sys.[tables] AS T " +
                      "INNER JOIN {0}.sys.[all_columns] AC ON T.[object_id] = AC.[object_id] " +
                     "INNER JOIN {0}.sys.[types] TY ON AC.[system_type_id] = TY.[system_type_id] AND AC.[user_type_id] = TY.[user_type_id] " +
                    "WHERE T.[is_ms_shipped] = 0 and T.[name] like @1 and OBJECT_SCHEMA_NAME(T.[object_id], DB_ID('{0}')) like @2 " +
                    "ORDER BY T.[name], AC.[column_id]", databaseName), conn);

                command.Parameters.Add(new SqlParameter("1", tableName));
                command.Parameters.Add(new SqlParameter("2", tableSchema));
                
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        try
                        {
                            listOfColumns.Add(new ColumnModel((string)reader[0], (string)reader[1], (short)reader[2], (byte)reader[3]));
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
