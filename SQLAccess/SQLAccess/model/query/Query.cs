using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLAccess.model.query
{
    class Query
    {
        public static QueryBuilder Builder()
        {
            return new QueryBuilder();
        }

        private List<CompactConstraintModel> columns;
        private string database;
        private string schema;
        private string table;

        private string completeQueryString;

        internal List<CompactConstraintModel> Columns { get => columns; set => columns = value; }
        public string Database { get => database; set => database = value; }
        public string Schema { get => schema; set => schema = value; }
        public string Table { get => table; set => table = value; }
        public string CompleteQueryString { get => completeQueryString; set => completeQueryString = value; }
    }
}
