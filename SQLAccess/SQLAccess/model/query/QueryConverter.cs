using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLAccess.model.query
{
    class QueryConverter
    {
        private StringBuilder sb; 

        public string ConvertToSQL(Query query)
        {
            sb = new StringBuilder();

            List<string> columnNames = new List<string>();
            foreach(var column in query.Columns)
            {
                if (column.Show == true)
                    columnNames.Add(column.ColumnName);
            }

            sb.AppendFormat("select {0} from {1}.{2}.{3} where ", columnNames, query.Database, query.Schema, query.Table);

            List<ConstraintModel>

            return sb.ToString();
        }
    }
}
