using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLAccess.model
{
    class RelationShipModel
    {
        private string parentSchema;
        private string parent;
        private string name;
        private string refrencedSchema;
        private string refrenced;

        public string Parent { get => parent; set => parent = value; }
        public string Name { get => name; set => name = value; }
        public string Refrenced { get => refrenced; set => refrenced = value; }
        public string RefrencedSchema { get => refrencedSchema; set => refrencedSchema = value; }
        public string ParentSchema { get => parentSchema; set => parentSchema = value; }

        public RelationShipModel(string parent, string name, string refrenced)
        {
            this.parent = parent;
            this.name = name;
            this.refrenced = refrenced;
        }

        public RelationShipModel(string parentSchema, string parent, string name, string refrencedSchema, string refrenced) : this(parentSchema, parent, name)
        {
            this.parentSchema = parentSchema;
            this.parent = parent;
            this.name = name;
            this.refrencedSchema = refrencedSchema;
            this.refrenced = refrenced;
        }

        public RelationShipModel()
        {
        }
    }
}
