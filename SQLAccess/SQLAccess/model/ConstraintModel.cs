using System;

namespace SQLAccess.model
{
    class ConstraintModel
    {
        private Boolean show;
        private Boolean sort;
        private string constraint;
        private string or;

        public bool Show { get => show; set => show = value; }
        public bool Sort { get => sort; set => sort = value; }
        public string Constraint { get => constraint; set => constraint = value; }
        public string Or { get => or; set => or = value; }

        public ConstraintModel()
        {
            this.show = false;
            this.sort = false;
            this.constraint = "";
            this.or = "";
        }

        public ConstraintModel(bool show, bool sort, string constraint, string or)
        {
            this.show = show;
            this.sort = sort;
            this.constraint = constraint;
            this.or = or;
        }
    }
}
