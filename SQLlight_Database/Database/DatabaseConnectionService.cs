using SQLight_Database.Database.Interfaces;
using SQLight_Database.HelperMethods;
using SQLight_Database.Models;

namespace SQLight_Database.Database
{
    public class DatabaseConnectionService : IDatabaseConnectionService
    {
        private readonly DatabaseConnectionStore _connectionStore;

        public DatabaseConnectionService(DatabaseConnectionStore databaseConnectionStore)
        {
            _connectionStore = databaseConnectionStore;
        }
        public void Open(User user)
        {
            try
            {
                _connectionStore.SQLite_conn = SQL_Commands.CreateConnection(user.Name, false);
            }
            catch
            {
                _connectionStore.SQLite_conn = SQL_Commands.CreateConnection(user.Name, true);
            }
            _connectionStore.CurrentUser = user;
        }

        public void Close()
        {
            if (_connectionStore?.SQLite_conn == null)
            {
                return;
            }
            SQL_Commands.CloseConnection(_connectionStore.SQLite_conn);
            _connectionStore.CurrentUser = null;
        }
    }
}
