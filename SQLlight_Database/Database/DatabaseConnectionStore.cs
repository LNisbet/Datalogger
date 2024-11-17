using SQLight_Database.Models;
using System.Data.SQLite;

namespace SQLight_Database.Database
{
    public class DatabaseConnectionStore
    {
        public User? CurrentUser { get; set; }
        public SQLiteConnection? SQLite_conn { get; set; }
    }
}
