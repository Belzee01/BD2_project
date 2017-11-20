using System.Collections.Generic;

namespace SQLAccess.model.query
{
    class QueryBuilder
    {
        private Query query;

        public QueryBuilder()
        {
            this.query = new Query();
        }

        public Query Build()
        {
            return query;
        }

        public QueryBuilder Columns(List<CompactConstraintModel> columns)
        {
            this.query.Columns = columns;
            return this;
        }
        public QueryBuilder Database(string database)
        {
            this.query.Database = database;
            return this;
        }
        public QueryBuilder Schema(string schema)
        {
            this.query.Schema = schema;
            return this;
        }
        public QueryBuilder Table(string table)
        {
            this.query.Table = table;
            return this;
        }
    }
}
