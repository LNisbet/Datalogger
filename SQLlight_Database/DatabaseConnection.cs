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
        private static User currentUser = new("User1",false);
        public static User CurrentUser { get => currentUser; set { currentUser = value; CreateConnection(currentUser); } }

        private static SQLiteConnection? sqlite_conn = null;
        internal static SQLiteConnection SQLite_conn { get { return sqlite_conn ?? (sqlite_conn = CreateConnection(CurrentUser)); } }

        private static SQLiteConnection CreateConnection(User user)
        {
            SQLiteConnection conn;
            try
            {
                conn = SQL_Commands.CreateConnection(user.Name, false);
            }
            catch
            {
                conn = SQL_Commands.CreateConnection(user.Name, true);
            }
            if (!user.Initilised) 
            { 
                SQL_Database.InititiliseDatabase();
            }

            return conn;
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
