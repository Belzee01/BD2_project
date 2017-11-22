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

        public string ConvertToSQL(Query query, int offset)
        {
            AssembleColumnsString(query, sb);
            sb.AppendFormat("from {0}.{1}.{2} ", query.Database, query.Schema, query.Table);

            AssembleConstraintsString(query, sb);
            AssembleSortString(query, sb);

            sb.AppendFormat("OFFSET {0} ROWS FETCH NEXT 100 ROWS ONLY", offset);

            return sb.ToString();
        }

        #region Assemble sql string represenation of models
        private void AssembleColumnsString(Query query, StringBuilder stringBuilder)
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
                    stringBuilder.AppendFormat(" [{0}], ", columnNames[i]);
                }
                stringBuilder.AppendFormat(" [{0}] ", columnNames[columnNames.Count - 1]);
            }
        }

        private void AssembleConstraintsString(Query query, StringBuilder stringBuilder)
        {
            List<CompactConstraintModel> columnWithConstraints = new List<CompactConstraintModel>();
            foreach (var column in query.Columns)
            {
                if (column.Constraint != "")
                    columnWithConstraints.Add(column);
            }

            if (columnWithConstraints.Count > 0)
            {
                stringBuilder.Append("where ");
                for (int i = 0; i < columnWithConstraints.Count - 1; i++)
                {
                    stringBuilder.AppendFormat("[{0}] {1} and ", columnWithConstraints[i].ColumnName, columnWithConstraints[i].Constraint);
                }
                stringBuilder.AppendFormat("[{0}] {1} ", columnWithConstraints[columnWithConstraints.Count - 1].ColumnName, columnWithConstraints[columnWithConstraints.Count - 1].Constraint);
            }
        }

        private void AssembleSortString(Query query, StringBuilder stringBuilder)
        {
            List<string> columnsWithSort = new List<string>();
            foreach (var column in query.Columns)
            {
                if (column.Sort == true)
                    columnsWithSort.Add(column.ColumnName);
            }
            stringBuilder.Append("order by");

            if (columnsWithSort.Count > 0)
            {
                for (int i = 0; i < columnsWithSort.Count - 1; i++)
                {
                    stringBuilder.AppendFormat(" [{0}], ", columnsWithSort[i]);
                }
                stringBuilder.AppendFormat(" [{0}] ", columnsWithSort[columnsWithSort.Count - 1]);
            }
            else
            {
                stringBuilder.Append(" (SELECT NULL) ");
            }
        }

        #endregion
    }
}
