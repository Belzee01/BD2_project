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

        public String Name
        {
            get { return name; }
            set { name = value; }
        }

        public int DatabaseId
        {
            get { return databaseId; }
            set { databaseId = value; }
        }

        public String CreateDate
        {
            get { return createDate; }
            set { createDate = value; }
        }
    }
}
