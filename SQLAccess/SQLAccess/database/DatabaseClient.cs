using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLAccess
{
    class DatabaseClient
    {
        /**
        SELECT name, database_id, create_date
        FROM sys.databases ;
        GO

        SELECT
          *
        FROM
          AdventureWorks.INFORMATION_SCHEMA.TABLES
        WHERE
          TABLE_TYPE = 'BASE TABLE';
        GO

        SELECT OBJECT_SCHEMA_NAME(T.[object_id],DB_ID('AdventureWorks')) AS [Schema],   
                T.[name] AS [table_name], AC.[name] AS [column_name],   
                TY.[name] AS system_data_type, AC.[max_length],  
                AC.[precision], AC.[scale], AC.[is_nullable], AC.[is_ansi_padded]  
        FROM AdventureWorks.sys.[tables] AS T   
          INNER JOIN AdventureWorks.sys.[all_columns] AC ON T.[object_id] = AC.[object_id]  
         INNER JOIN AdventureWorks.sys.[types] TY ON AC.[system_type_id] = TY.[system_type_id] AND AC.[user_type_id] = TY.[user_type_id]   
        WHERE T.[is_ms_shipped] = 0 and T.[name] like '%%' and OBJECT_SCHEMA_NAME(T.[object_id],DB_ID('AdventureWorks')) = 'Person'
        ORDER BY T.[name], AC.[column_id]

        */
    }
}
