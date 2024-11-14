using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLight_Database
{
    public class DatabaseConnectionStore
    {
        public User? CurrentUser { get; set; }

        public SQLiteConnection? SQLite_conn { get; set; }
    }
}
