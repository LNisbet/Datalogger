using SQLight_Database.Database;
using SQLight_Database.Database.Interfaces;
using SQLight_Database.Tables.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLight_Database
{
    public class DatabaseConnection : IDatabaseConnection
    {

        public User CurrentUser { get; private set; }

        public SQLiteConnection SQLite_conn { get; set; }

        public DatabaseConnection(User user) 
        {
            try
            {
                SQLite_conn = SQL_Commands.CreateConnection(user.Name, false);
            }
            catch
            {
                SQLite_conn = SQL_Commands.CreateConnection(user.Name, true);
            }
            CurrentUser = user;
        }

        public void Close()
        {
            SQL_Commands.CloseConnection(SQLite_conn);
        }
    }
}
