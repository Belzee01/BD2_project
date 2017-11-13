using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLAccess
{
    class TableModel
    {
        private String schema;
        private String tableName;
        private List<String> columnNames;
        private String dataType;
        private int maxLength;
        private int precision;

        public string Schema { get => schema; set => schema = value; }
        public string TableName { get => tableName; set => tableName = value; }
        public List<string> ColumnNames { get => columnNames; set => columnNames = value; }
        public string DataType { get => dataType; set => dataType = value; }
        public int MaxLength { get => maxLength; set => maxLength = value; }
        public int Precision { get => precision; set => precision = value; }
    }
}
