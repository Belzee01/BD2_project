using System;

namespace SQLAccess.model
{
    class ConstraintModel
    {
        private Boolean show;
        private SORT sort;
        private string constraint;
        private string or;

        public bool Show { get => show; set => show = value; }
        public SORT Sort { get => sort; set => sort = value; }
        public string Constraint { get => constraint; set => constraint = value; }
        public string Or { get => or; set => or = value; }

        public ConstraintModel()
        {
            this.show = false;
            this.sort = SORT.NONE;
            this.constraint = "";
            this.or = "";
        }

        public ConstraintModel(bool show, SORT sort, string constraint, string or)
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
