using SQLight_Database.Config;
using SQLight_Database.Database.Interfaces;
using SQLight_Database.HelperMethods;
using SQLight_Database.Models;
using SQLight_Database.Tables.Interfaces;

namespace SQLight_Database.Database
{
    public class SQL_Database : ISQL_Database
    {
        private readonly DatabaseConnectionStore _databaseConnectionStore;
        private readonly IUsersTable _usersTable;

        public SQL_Database(IUsersTable usersTable, DatabaseConnectionStore databaseConnectionStore)
        {
            _databaseConnectionStore = databaseConnectionStore;
            _usersTable = usersTable;

            if (String.IsNullOrEmpty(databaseConnectionStore.CurrentUser?.Name))
                return;

        }

        public void DeleteDatabase()
        {
            if (_databaseConnectionStore.CurrentUser == null)
                return;

            SQL_Commands.ExecuteSQLString(_databaseConnectionStore.SQLite_conn, SQL_Strings.DeleteDatabase(_databaseConnectionStore.CurrentUser.Name), SQL_Commands.CommandType.NonQuery);
            _usersTable.Remove(_databaseConnectionStore.CurrentUser);

            if (File.Exists($"{_databaseConnectionStore.CurrentUser.Name}.db"))
            {
                File.Delete($"{_databaseConnectionStore.CurrentUser.Name}.db");
            }
        }
    }
}
