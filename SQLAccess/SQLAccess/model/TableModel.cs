using SQLAccess.model;
using System.Collections.Generic;

namespace SQLAccess
{
    class TableModel
    {

        private List<ColumnModel> columns;

        private TableSchemaModel tableSchemaModel;

        public List<ColumnModel> Columns { get => columns; set => columns = value; }

        internal TableSchemaModel TableSchemaModel { get => tableSchemaModel; set => tableSchemaModel = value; }

        public TableModel(TableSchemaModel tableSchemaModel, List<ColumnModel> columnNames)
        {
            this.TableSchemaModel = tableSchemaModel;
            this.columns = columnNames;
        }
    }
}
