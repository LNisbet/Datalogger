using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLight_Database.Database.Interfaces
{
    public interface IDatabaseConnection
    {
        User CurrentUser { get; }
        SQLiteConnection SQLite_conn { get; }

        void Close();
    }
}
