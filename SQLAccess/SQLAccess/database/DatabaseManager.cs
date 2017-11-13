using SQLAccess.model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLAccess
{
    class DatabaseManager
    {

        protected void selectDatabaseList()
        {
            List<DatabaseModel> listOfDatabases = new List<DatabaseModel>();
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = SQLAccess.Properties.Settings.Default.masterConnectionString;

                SqlCommand command = new SqlCommand("SELECT name, database_id, create_date FROM sys.databases", conn);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    Console.WriteLine("\t\tselectDatabaseList ---------------------------------------------------");
                    while (reader.Read())
                    {
                        listOfDatabases.Add(new DatabaseModel((string)reader[0], (int)reader[1], (string)reader[2]));

                        Console.WriteLine(String.Format("{0} \t | {1} \t | {2}", reader[0], reader[1], reader[2]));
                    }
                }
            }
        }

        protected void selectTableSchemaList(string databaseName)
        {
            List<TableSchemaModel> listOfTableSchemas = new List<TableSchemaModel>();
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = SQLAccess.Properties.Settings.Default.masterConnectionString;

                SqlCommand command = new SqlCommand("SELECT TABLE_SCHEMA, TABLE_NAME FROM @0.INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'BASE TABLE'", conn);
                command.Parameters.Add(new SqlParameter("0", databaseName));

                Console.WriteLine("\t\tselectTables -------------------------------------------------------");
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    listOfTableSchemas.Add(new TableSchemaModel((string)reader[0], (string)reader[1]));

                    Console.WriteLine(String.Format("{0} \t | {1} \t", reader[0], reader[1], reader[2]));
                }
            }
        }

        protected void selectTableDataList(string databaseName, string tableSchema, string tableName)
        {
            List<TableSchemaModel> listOfTableSchemas = new List<TableSchemaModel>();
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = SQLAccess.Properties.Settings.Default.masterConnectionString;

                SqlCommand command = new SqlCommand("SELECT " +
                    "AC.[name] AS[column_name]," +
                    "TY.[name] AS system_data_type, AC.[max_length]," +
                    "AC.[precision]" +
                    "FROM @0.sys.[tables] AS T " +
                      "INNER JOIN @0.sys.[all_columns] AC ON T.[object_id] = AC.[object_id] " +
                     "INNER JOIN @0.sys.[types] TY ON AC.[system_type_id] = TY.[system_type_id] AND AC.[user_type_id] = TY.[user_type_id] " +
                    "WHERE T.[is_ms_shipped] = 0 and T.[name] like '@1' and OBJECT_SCHEMA_NAME(T.[object_id], DB_ID('@0')) like '@2' " +
                    "ORDER BY T.[name], AC.[column_id]", conn);

                command.Parameters.Add(new SqlParameter("0", databaseName));
                command.Parameters.Add(new SqlParameter("1", tableName));
                command.Parameters.Add(new SqlParameter("2", tableSchema));

                Console.WriteLine("\t\tselectTables -------------------------------------------------------");
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    listOfTableSchemas.Add(new TableModel((new TableSchemaModel(tableSchema, tableName), (string)reader[1]));

                    Console.WriteLine(String.Format("{0} \t | {1} \t", reader[0], reader[1], reader[2]));
                }
            }
        }

    }
}
