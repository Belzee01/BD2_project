﻿using System;

namespace SQLAccess.model
{
    class ColumnModel
    {
        private String columnName;
        private String dataType;
        private short maxLength;
        private byte precision;

        public string ColumnName { get => columnName; set => columnName = value; }
        public string DataType { get => dataType; set => dataType = value; }
        public short MaxLength { get => maxLength; set => maxLength = value; }
        public byte Precision { get => precision; set => precision = value; }

        public ColumnModel(string columnName, string dataType, short maxLength, byte precision)
        {
            this.columnName = columnName;
            this.dataType = dataType;
            this.maxLength = maxLength;
            this.precision = precision;
        }

        public override string ToString()
        {
            return String.Format("{0}\t|\t{1}\t|\t{2}\t|\t{3}", this.columnName, this.dataType, this.maxLength, this.precision);
        }
    }
}
