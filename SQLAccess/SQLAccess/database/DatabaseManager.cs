using SQLAccess.model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                    Console.WriteLine("\t\tselectDatabaseList ---------------------------------------------------");
                    while (reader.Read())
                    {
                        listOfDatabases.Add(new DatabaseModel((string)reader[0], (int)reader[1], (DateTime)reader[2]));

                        Console.WriteLine(String.Format("{0} \t | {1} \t | {2}", reader[0], reader[1], reader[2]));
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

                Console.WriteLine("\t\tselectSchemas -------------------------------------------------------");
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        listOfTableSchemas.Add(new TableSchemaModel((string)reader[0], (string)reader[1]));

                        Console.WriteLine(String.Format("{0} \t | {1} \t", reader[0], reader[1]));
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

                Console.WriteLine("\t\tselectColumns -------------------------------------------------------");
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        listOfColumns.Add(new ColumnModel((string)reader[0], (string)reader[1], (short)reader[2], (byte)reader[3]));

                        Console.WriteLine(String.Format("{0} \t | {1} \t {2} \t | {3}", reader[0], reader[1], reader[2], reader[3]));
                    }
                }
                return new TableModel(new TableSchemaModel(tableSchema, tableName), listOfColumns);
            }
        }

        public DataTable RetrieveDataByQuery(string databaseName, string tableSchema, string tableName)
        {
            string queryString = String.Format("SELECT * FROM {0}.{1}.{2}", databaseName, tableSchema, tableName);
            DataTable data = new DataTable();

            using (SqlConnection conn = new SqlConnection(SQLAccess.Properties.Settings.Default.masterConnectionString))
            {
                SqlDataAdapter adapter = new SqlDataAdapter(queryString, conn);

                adapter.Fill(data);
            }

            return data;
        }

    }
}
