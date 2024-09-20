using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLight_Database
{
    public class NoOpenSQLConnection : Exception
    {
        public NoOpenSQLConnection() { }

        public NoOpenSQLConnection(string message) : base(message) { }
    }
}
