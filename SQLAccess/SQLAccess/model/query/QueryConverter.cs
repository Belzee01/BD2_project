using System;
using System.Collections.Generic;
using System.Text;

namespace SQLAccess.model.query
{
    class QueryConverter
    {
        private StringBuilder sb;

        public QueryConverter()
        {
            sb = new StringBuilder("select ");
        }

        public string ConvertToSQL(Query query) 
        {
            List<string> columnNames = new List<string>();
            foreach (var column in query.Columns)
            {
                if (column.Show == true)
                    columnNames.Add(column.ColumnName);
            }

            if (columnNames.Count < 1)
                throw new ArgumentException("Column display list cannot be empty!");

            if (columnNames.Count > 0)
            {
                for (int i = 0; i < columnNames.Count - 1; i++)
                {
                    sb.AppendFormat(" [{0}], ", columnNames[i]);
                }
                sb.AppendFormat(" [{0}] ", columnNames[columnNames.Count - 1]);
            }
            sb.AppendFormat("from {0}.{1}.{2} ", query.Database, query.Schema, query.Table);

            List<CompactConstraintModel> columnWithConstraints = new List<CompactConstraintModel>();
            foreach (var column in query.Columns)
            {
                if (column.Constraint != "")
                    columnWithConstraints.Add(column);
            }

            if (columnWithConstraints.Count > 0)
            {
                sb.Append("where ");
                for (int i = 0; i < columnWithConstraints.Count - 1; i++)
                {
                    sb.AppendFormat("[{0}] {1} and ", columnWithConstraints[i].ColumnName, columnWithConstraints[i].Constraint);
                }
                sb.AppendFormat("[{0}] {1} ", columnWithConstraints[columnWithConstraints.Count - 1].ColumnName, columnWithConstraints[columnWithConstraints.Count - 1].Constraint);
            }

            List<string> columnsWithSort = new List<string>();
            foreach (var column in query.Columns)
            {
                if (column.Sort == true)
                    columnsWithSort.Add(column.ColumnName);
            }

            if (columnsWithSort.Count > 0)
            {
                sb.Append("order by");
                for (int i = 0; i < columnsWithSort.Count - 1; i++)
                {
                    sb.AppendFormat(" [{0}], ", columnsWithSort[i]);
                }
                sb.AppendFormat(" [{0}] ", columnsWithSort[columnsWithSort.Count - 1]);
            }

            return sb.ToString();
        }

        public string ConvertToSQL(Query query, int offset)
        {
            List<string> columnNames = new List<string>();
            foreach (var column in query.Columns)
            {
                if (column.Show == true)
                    columnNames.Add(column.ColumnName);
            }

            if (columnNames.Count < 1)
                throw new ArgumentException("Column display list cannot be empty!");

            if (columnNames.Count > 0)
            {
                for (int i = 0; i < columnNames.Count - 1; i++)
                {
                    sb.AppendFormat(" [{0}], ", columnNames[i]);
                }
                sb.AppendFormat(" [{0}] ", columnNames[columnNames.Count - 1]);
            }
            sb.AppendFormat("from {0}.{1}.{2} ", query.Database, query.Schema, query.Table);

            List<CompactConstraintModel> columnWithConstraints = new List<CompactConstraintModel>();
            foreach (var column in query.Columns)
            {
                if (column.Constraint != "")
                    columnWithConstraints.Add(column);
            }

            if (columnWithConstraints.Count > 0)
            {
                sb.Append("where ");
                for (int i = 0; i < columnWithConstraints.Count - 1; i++)
                {
                    sb.AppendFormat("[{0}] {1} and ", columnWithConstraints[i].ColumnName, columnWithConstraints[i].Constraint);
                }
                sb.AppendFormat("[{0}] {1} ", columnWithConstraints[columnWithConstraints.Count - 1].ColumnName, columnWithConstraints[columnWithConstraints.Count - 1].Constraint);
            }

            List<string> columnsWithSort = new List<string>();
            foreach (var column in query.Columns)
            {
                if (column.Sort == true)
                    columnsWithSort.Add(column.ColumnName);
            }
            sb.Append("order by");

            if (columnsWithSort.Count > 0)
            {
                for (int i = 0; i < columnsWithSort.Count - 1; i++)
                {
                    sb.AppendFormat(" [{0}], ", columnsWithSort[i]);
                }
                sb.AppendFormat(" [{0}] ", columnsWithSort[columnsWithSort.Count - 1]);
            } else
            {
                sb.Append(" (SELECT NULL) ");
            }
            sb.AppendFormat("OFFSET {0} ROWS FETCH NEXT {1} ROWS ONLY", offset, offset+100);

            return sb.ToString();
        }
    }
}
