using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLight_Database
{
    public class UsersTable
    {
        private static ObservableCollection<User> allUsers = new();
        public static ObservableCollection<User> AllUsers { get { ReadAllUsers(); return allUsers; } }

        public static ObservableCollection<string> AllUserNames { get => new(AllUsers.Select(users => users.Name).Distinct()); }

        internal static void AddSingleUser(User user)
        {
            if (!AllUserNames.Contains(user.Name))
            {
                SQL_Commands.ExecuteSQLString(DatabaseConnection.SQLite_conn, SQL_Strings.InsertData(Config.UsersTableName, user.ToSQLStringList()), Enums.CommandType.NonQuery);
                ReadAllUsers();
            }
        }

        public static void RemoveSingleUser(User user)
        {
            SQL_Commands.ExecuteSQLString(DatabaseConnection.SQLite_conn, SQL_Strings.DeleteFromTable(Config.UsersTableName, $"Name='{user.Name}'"), Enums.CommandType.NonQuery);
            ReadAllUsers();
        }

        internal static void ReadAllUsers()
        {
            var sqlite_datareader = SQL_Commands.ExecuteSQLString(DatabaseConnection.SQLite_conn, SQL_Strings.ReadData(Config.UsersTableName, "*", true), Enums.CommandType.Reader) as SQLiteDataReader;

            allUsers.Clear();

            while (sqlite_datareader != null && sqlite_datareader.Read())
            {
                allUsers.Add(new User(sqlite_datareader));
            }
        }
    }
}
