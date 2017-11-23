using SQLAccess.model;
using System.Collections.Generic;

namespace SQLAccess
{
    class CompactConstraintModel
    {
        private ColumnModel columnModel;
        private ConstraintModel constraintModel;

        public string ColumnName { get => columnModel.ColumnName; }
        public string DataType { get => columnModel.DataType; }
        public short MaxLength { get => columnModel.MaxLength;}
        public byte Precision { get => columnModel.Precision; }
        public bool Show { get => constraintModel.Show; set => constraintModel.Show = value; }
        public SORT Sort { get => constraintModel.Sort; set => constraintModel.Sort = value; }

        public string AndExpression { get => constraintModel.AndExpression; set => constraintModel.AndExpression = value; }
        public string AndValue { get => constraintModel.AndValue; set => constraintModel.AndValue = value; }

        public string OrExpression { get => constraintModel.OrExpression; set => constraintModel.OrExpression = value; }
        public string OrValue { get => constraintModel.OrValue; set => constraintModel.OrValue = value; }

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
