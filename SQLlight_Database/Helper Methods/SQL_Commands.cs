using System.Data.SQLite;

namespace SQLight_Database
{
    static internal class SQL_Commands
    {

        internal static SQLiteConnection CreateConnection(string dbName, bool DbExists)
        {
            string newConnectionString = SQL_Strings.CreateConnection(dbName, 3, true, true);
            string ExistingConnectionString = SQL_Strings.CreateConnection(dbName, 3, false, true);

            // Create a new database connection
            SQLiteConnection conn;

            if (DbExists)
                conn = new SQLiteConnection(newConnectionString);
            else
                conn = new SQLiteConnection(ExistingConnectionString);

            // Open the connection:
            try
            {
                conn.Open();
            }
            catch
            {
                conn = new SQLiteConnection(ExistingConnectionString);
                conn.Open();
                throw;
            }
            return conn;
        }

        internal static SQLiteConnection CloseConnection(SQLiteConnection conn)
        {
            // close the connection:
            conn.Close();
            return conn;
        }

        internal static object? ExecuteSQLString(SQLiteConnection? sqlite_conn, string sqlString, Enums.CommandType commandType)
        {
            if (sqlite_conn == null)
                throw new NoOpenSQLConnection();

            switch (commandType)
            {
                case Enums.CommandType.NonQuery:
                    ExecuteNonQueryCommand(sqlite_conn, sqlString);
                    return null;
                case Enums.CommandType.Reader:
                    return ExecuteReader(sqlite_conn, sqlString);
                default:
                    throw new NotImplementedException(commandType.ToString());

            }
        }

        private static void ExecuteNonQueryCommand(SQLiteConnection conn, string command)
        {
            SQLiteCommand sqlite_cmd = conn.CreateCommand();
            sqlite_cmd.CommandText = command;
            sqlite_cmd.ExecuteNonQuery();
        }
        private static SQLiteDataReader? ExecuteReader(SQLiteConnection conn, string command)
        {
            SQLiteDataReader? sqlite_datareader = null;
            SQLiteCommand sqlite_cmd = conn.CreateCommand();
            sqlite_cmd.CommandText = command;
            sqlite_datareader = sqlite_cmd.ExecuteReader();

            return sqlite_datareader;
        }
    }
}
