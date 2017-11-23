using SQLAccess.model;

namespace SQLAccess
{
    class CompactConstraintModel
    {
        private ColumnModel columnModel;
        private ConstraintModel constraintModel;

        public string ColumnName { get => columnModel.ColumnName; set => columnModel.ColumnName = value; }
        public string DataType { get => columnModel.DataType; set => columnModel.DataType = value; }
        public short MaxLength { get => columnModel.MaxLength; set => columnModel.MaxLength = value; }
        public byte Precision { get => columnModel.Precision; set => columnModel.Precision = value; }
        public bool Show { get => constraintModel.Show; set => constraintModel.Show = value; }
        public SORT Sort { get => constraintModel.Sort; set => constraintModel.Sort = value; }
        public string Constraint { get => constraintModel.Constraint; set => constraintModel.Constraint = value; }
        public string Or { get => constraintModel.Or; set => constraintModel.Or = value; }

        public CompactConstraintModel(ColumnModel columnModel, ConstraintModel constraintModel)
        {
            this.columnModel = columnModel;
            this.constraintModel = constraintModel;
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
