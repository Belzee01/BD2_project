using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLAccess.model
{
    class ColumnModel
    {
        private String columnName;
        private String dataType;
        private int maxLength;
        private int precision;

        public string DataType { get => dataType; set => dataType = value; }
        public int MaxLength { get => maxLength; set => maxLength = value; }
        public int Precision { get => precision; set => precision = value; }
        public string ColumnName { get => columnName; set => columnName = value; }

        public ColumnModel(string columnName, string dataType, int maxLength, int precision)
        {
            this.columnName = columnName;
            this.dataType = dataType;
            this.maxLength = maxLength;
            this.precision = precision;
        }
    }
}
