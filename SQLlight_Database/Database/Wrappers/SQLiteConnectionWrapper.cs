using System.Data.SQLite;
using System.Data;

namespace SQLight_Database.Database.Wrappers
{
    public class SQLiteConnectionWrapper : IDbConnectionWrapper
    {
        private readonly SQLiteConnection _connection;

        public SQLiteConnectionWrapper(string connectionString)
        {
            _connection = new SQLiteConnection(connectionString);
        }

        public void Open() => _connection.Open();

        public void Close() => _connection.Close();

        public IDbCommand CreateCommand() => _connection.CreateCommand();
    }
}

