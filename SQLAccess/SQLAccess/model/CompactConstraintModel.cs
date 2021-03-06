﻿using SQLAccess.model;
using System.Data;

namespace SQLAccess
{
    class CompactConstraintModel
    {
        private ColumnModel columnModel;
        private ConstraintModel constraintModel;

        public string ColumnName { get => columnModel.ColumnName; }
        public object DataType { get => columnModel.DataType; }
        public short MaxLength { get => columnModel.MaxLength;}
        public byte Precision { get => columnModel.Precision; }
        public bool Show { get => constraintModel.Show; set => constraintModel.Show = value; }
        public SORT Sort { get => constraintModel.Sort; set => constraintModel.Sort = value; }

        public string And { get => constraintModel.AndExpression; set => constraintModel.AndExpression = value; }
        public object AndValue { get => constraintModel.AndValue; set => constraintModel.AndValue = value; }

        public string Or { get => constraintModel.OrExpression; set => constraintModel.OrExpression = value; }
        public object OrValue { get => constraintModel.OrValue; set => constraintModel.OrValue = value; }

        public CompactConstraintModel(ColumnModel columnModel, ConstraintModel constraintModel)
        {
            this.columnModel = columnModel;
            this.constraintModel = constraintModel;
        }

        public override string ToString()
        {
            return base.ToString();
        }

        public override int GetHashCode()
        {
            return this.ColumnName.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return this.ColumnName == ((CompactConstraintModel)obj).ColumnName;
        }
    }
}
