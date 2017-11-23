using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLAccess.model.conditions
{
    class Condition
    {
        private string expression;
        private object value;

        public string Expression { get => expression; set => expression = value; }
        public object Value { get => value; set => this.value = value; }

        public Condition(string expression, string value)
        {
            this.expression = expression;
            this.value = value;
        }

        public override string ToString()
        {
            return "";
        }
    }
}
