using System;

namespace SQLAccess.model
{
    class TableSchemaModel
    {
        private String schema;
        private String tableName;

        public string Schema { get => schema; set => schema = value; }
        public string TableName { get => tableName; set => tableName = value; }

        public TableSchemaModel(string schema, string tableName)
        {
            this.schema = schema;
            this.tableName = tableName;
        }

        public override string ToString()
        {
            return this.schema + "." + this.tableName;
        }
    }
}
