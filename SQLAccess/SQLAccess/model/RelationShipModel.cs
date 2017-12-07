using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLAccess.model
{
    class RelationShipModel
    {
        private string parent;
        private string name;
        private string refrenced;

        public string Parent { get => parent; set => parent = value; }
        public string Name { get => name; set => name = value; }
        public string Refrenced { get => refrenced; set => refrenced = value; }

        public RelationShipModel(string parent, string name, string refrenced)
        {
            this.parent = parent;
            this.name = name;
            this.refrenced = refrenced;
        }
    }
}
