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
        public static User CurrentUser { get => currentUser; set { currentUser = value; } }

        private static SQLiteConnection? sqlite_conn = null;
        internal static SQLiteConnection SQLite_conn 
        { 
            get 
            {
                if (sqlite_conn == null)
                {
                    CreateConnection(CurrentUser);
                    if (sqlite_conn == null) { throw new NullReferenceException(); }
                    return sqlite_conn;
                }
                else
                {
                    return sqlite_conn;
                }
            } 
        }

        private static void CreateConnection(User user)
        {
            try
            {
                sqlite_conn = SQL_Commands.CreateConnection(user.Name, false);
            }
            catch
            {
                sqlite_conn = SQL_Commands.CreateConnection(user.Name, true);
            }

            if (!Users.AllUserNames.Contains(user.Name)) 
            { 
                SQL_Database.InititiliseDatabase(sqlite_conn);
                user.Initilised = true;
                Users.Add(user);
            }
            else if(!Users.AllUsers.FirstOrDefault(u => u.Name == user.Name).Initilised) 
            {
                SQL_Database.InititiliseDatabase(sqlite_conn);
                user.Initilised = true;
                Users.Modify(user);
            }

            CurrentUser = user;
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
