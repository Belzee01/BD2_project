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
                if (column.AndExpression != null && column.AndValue != null && column.AndExpression != "" && column.AndValue != "")
                    columnWithConstraints.Add(column);
            }

            if (columnWithConstraints.Count > 0)
            {
                stringBuilder.Append("where ");
                for (int i = 0; i < columnWithConstraints.Count - 1; i++)
                {
                    stringBuilder.AppendFormat("([{0}] {1} {2} ", 
                        columnWithConstraints[i].ColumnName, 
                        columnWithConstraints[i].AndExpression,
                        ValidateValue(columnWithConstraints[i].AndValue));

                    if (columnWithConstraints[i].OrExpression != "" && columnWithConstraints[i].OrValue != "")
                    {
                        stringBuilder.AppendFormat("or [{0}] {1} {2}) and ", 
                            columnWithConstraints[i].ColumnName, 
                            columnWithConstraints[i].OrExpression, 
                            ValidateValue(columnWithConstraints[i].OrValue));
                    }
                    else
                        stringBuilder.Append(") and ");
                }
                stringBuilder.AppendFormat("([{0}] {1} {2} ", 
                    columnWithConstraints[columnWithConstraints.Count - 1].ColumnName,
                    columnWithConstraints[columnWithConstraints.Count - 1].AndExpression,
                    ValidateValue(columnWithConstraints[columnWithConstraints.Count - 1].AndValue));

                if (columnWithConstraints[columnWithConstraints.Count - 1].OrExpression != "" && columnWithConstraints[columnWithConstraints.Count - 1].OrValue != "")
                {
                    stringBuilder.AppendFormat("or [{0}] {1} {2}) ", 
                        columnWithConstraints[columnWithConstraints.Count - 1].ColumnName,
                        columnWithConstraints[columnWithConstraints.Count - 1].OrExpression,
                        ValidateValue(columnWithConstraints[columnWithConstraints.Count - 1].OrValue));
                }
                else
                    sb.Append(" ) ");
            }
        }

        private void AssembleSortString(Query query, StringBuilder stringBuilder)
        {
            List<KeyValuePair<string, SORT>> columnsWithSort = new List<KeyValuePair<string, SORT>>();
            foreach (var column in query.Columns)
            {
                if (column.Sort != SORT.NONE)
                    columnsWithSort.Add(new KeyValuePair<string, SORT>(column.ColumnName, column.Sort));
            }
            stringBuilder.Append("order by");

            if (columnsWithSort.Count > 0)
            {
                for (int i = 0; i < columnsWithSort.Count - 1; i++)
                {
                    stringBuilder.AppendFormat(" [{0}] {1}, ", columnsWithSort[i].Key, columnsWithSort[i].Value);
                }
                stringBuilder.AppendFormat(" [{0}] {1} ", columnsWithSort[columnsWithSort.Count - 1].Key, columnsWithSort[columnsWithSort.Count - 1].Value);
            }
            else
            {
                stringBuilder.Append(" (SELECT NULL) ");
            }
        }

        private object ValidateValue(object val)
        {
            if (val.GetType() == typeof(string))
            {
                string newVal = val.ToString();
                newVal = newVal.Replace("'", "''");

                return "'" + newVal + "'";
            }

            return val;
        }

        #endregion
    }
}
