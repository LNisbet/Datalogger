using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLight_Database.Database.Interfaces
{
    public interface IDatabaseConnectionService
    {
        void Open(User user);
        void Close();
    }
}
