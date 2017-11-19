    SELECT AC.[name] AS [column_name],   
                TY.[name] AS system_data_type, AC.[max_length],  
                AC.[precision]
        FROM AdventureWorks.sys.[tables] AS T   
          INNER JOIN AdventureWorks.sys.[all_columns] AC ON T.[object_id] = AC.[object_id]  
         INNER JOIN AdventureWorks.sys.[types] TY ON AC.[system_type_id] = TY.[system_type_id] AND AC.[user_type_id] = TY.[user_type_id]   
        WHERE T.[is_ms_shipped] = 0 and T.[name] like 'ErrorLog' and OBJECT_SCHEMA_NAME(T.[object_id],DB_ID('AdventureWorks')) like 'dbo'
        ORDER BY T.[name], AC.[column_id]
        GO

		     SELECT
          *
        FROM
          AdventureWorks.INFORMATION_SCHEMA.TABLES
        WHERE
          TABLE_TYPE = 'BASE TABLE';
        GO