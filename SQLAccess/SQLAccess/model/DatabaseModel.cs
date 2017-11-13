using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLAccess
{
    class DatabaseModel
    {
        private String name;
        private int databaseId;
        private String createDate;

        public string Name { get => name; set => name = value; }
        public int DatabaseId { get => databaseId; set => databaseId = value; }
        public string CreateDate { get => createDate; set => createDate = value; }

        public DatabaseModel(string name, int databaseId, string createDate)
        {
            this.name = name;
            this.databaseId = databaseId;
            this.createDate = createDate;
        }
    }
}
