using SQLAccess.model.conditions;
using System;

namespace SQLAccess.model
{
    class ConstraintModel
    {
        private Boolean show;
        private SORT sort;
        private Condition constraint;
        private Condition or;

        public bool Show { get => show; set => show = value; }
        public SORT Sort { get => sort; set => sort = value; }
        public string AndExpression { get => constraint.Expression; set => constraint.Expression = value; }
        public object AndValue { get => constraint.Value; set => constraint.Value = value; }

        public string OrExpression { get => or.Expression; set => or.Expression = value; }
        public object OrValue { get => or.Value; set => or.Value = value; }

        public ConstraintModel()
        {
            this.show = false;
            this.sort = SORT.NONE;
            this.constraint = new Condition("", "");
            this.or = new Condition("", "");
        }

        public ConstraintModel(bool show, SORT sort, Condition constraint, Condition or)
        {
            this.show = show;
            this.sort = sort;
            this.constraint = constraint;
            this.or = or;
        }
    }

    enum SORT
    {
        NONE,
        ASC,
        DESC
    }
}
