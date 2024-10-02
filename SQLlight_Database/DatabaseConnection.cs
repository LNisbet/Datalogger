using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLight_Database
{
    public static class DatabaseConnection
    {
        private static string dbName = "User1";
        public static string DBName { get => dbName; set { dbName = value; CreateConnection(dbName); } }

        private static SQLiteConnection? sqlite_conn = null;
        internal static SQLiteConnection SQLite_conn { get { return sqlite_conn ?? (sqlite_conn = CreateConnection(DBName));  } }

        private static SQLiteConnection CreateConnection(string name)
        {
            try
            {
                return SQL_Commands.CreateConnection(name, false);
            }
            catch
            {
                return SQL_Commands.CreateConnection(name, true);
            }
        }

        public static void CloseConnection()
        {
            if (sqlite_conn == null)
                return;
            SQL_Commands.CloseConnection(sqlite_conn);
            sqlite_conn = null;
        }
    }
}
