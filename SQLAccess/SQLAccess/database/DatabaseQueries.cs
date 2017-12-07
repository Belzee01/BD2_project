using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLAccess
{
    class DatabaseQueries
    {
        public static string databaseList = "SELECT name, database_id, create_date FROM sys.databases";

        public static string schemaList = "SELECT TABLE_SCHEMA, TABLE_NAME FROM {0}.INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'BASE TABLE'";

        public static string tableData = "SELECT " +
                    "AC.[name] AS[column_name]," +
                    "TY.[name] AS system_data_type, AC.[max_length]," +
                    "AC.[precision] " +
                    "FROM {0}.sys.[tables] AS T " +
                      "INNER JOIN {0}.sys.[all_columns] AC ON T.[object_id] = AC.[object_id] " +
                     "INNER JOIN {0}.sys.[types] TY ON AC.[system_type_id] = TY.[system_type_id] AND AC.[user_type_id] = TY.[user_type_id] " +
                    "WHERE T.[is_ms_shipped] = 0 and T.[name] like @1 and OBJECT_SCHEMA_NAME(T.[object_id], DB_ID('{0}')) like @2 " +
                    "ORDER BY T.[name], AC.[column_id]";


        public static string selectRelationShips = "SELECT " + 
            "tp.name 'Parent', cp.name, tr.name 'Refrenced' FROM " +
            "{0}.sys.foreign_keys fk INNER JOIN {0}.sys.tables tp ON fk.parent_object_id = tp.object_id " +
            "INNER JOIN {0}.sys.tables tr ON fk.referenced_object_id = tr.object_id " +
            "INNER JOIN {0}.sys.foreign_key_columns fkc ON fkc.constraint_object_id = fk.object_id " +
            "INNER JOIN {0}.sys.columns cp ON fkc.parent_column_id = cp.column_id AND fkc.parent_object_id = cp.object_id " +
            "INNER JOIN {0}.sys.columns cr ON fkc.referenced_column_id = cr.column_id AND fkc.referenced_object_id = cr.object_id " + 
            "where OBJECT_SCHEMA_NAME(tp.[object_id], DB_ID('{0}')) like '{1}' " + 
            "and tp.name = '{2}'";

        public static string selectRelationShip = "SELECT " +
           "tp.name 'Parent', cp.name, tr.name 'Refrenced' FROM " +
           "{0}.sys.foreign_keys fk INNER JOIN {0}.sys.tables tp ON fk.parent_object_id = tp.object_id " +
           "INNER JOIN {0}.sys.tables tr ON fk.referenced_object_id = tr.object_id " +
           "INNER JOIN {0}.sys.foreign_key_columns fkc ON fkc.constraint_object_id = fk.object_id " +
           "INNER JOIN {0}.sys.columns cp ON fkc.parent_column_id = cp.column_id AND fkc.parent_object_id = cp.object_id " +
           "INNER JOIN {0}.sys.columns cr ON fkc.referenced_column_id = cr.column_id AND fkc.referenced_object_id = cr.object_id " +
           "where OBJECT_SCHEMA_NAME(tp.[object_id], DB_ID('{0}')) like '{1}' " +
           "and tp.name = '{2}' and tr.name='{3}'";
    }
}
