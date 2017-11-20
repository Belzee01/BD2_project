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
                    sb.AppendFormat(" {0}, ", column.ColumnName);
            }

            sb.AppendFormat("from {1}.{2}.{3} where ", columnNames.ToString(), query.Database, query.Schema, query.Table);

            for (int i = 0; i < query.Columns.Count - 1; i++)
            {
                if (query.Columns[i].Constraint != "")
                    sb.AppendFormat("{0} {1} and ", query.Columns[i].ColumnName, query.Columns[i].Constraint);
            }

            sb.Append("order by");
            foreach (var column in query.Columns)
            {
                if (column.Sort == true)
                    sb.AppendFormat(" {0}, ", column.ColumnName);
            }

            return sb.ToString();
        }
    }
}
