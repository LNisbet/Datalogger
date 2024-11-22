using SQLight_Database.Database.Wrappers;
using SQLight_Database.Exceptions;
using System.Data;

namespace SQLight_Database.HelperMethods
{
    static internal class SQL_Commands
    {
        internal static IDbConnectionWrapper CreateConnection(string dbName, bool DbExists)
        {
            string connectionString = SQL_Strings.CreateConnection(dbName, 3, DbExists, true);
            var conn = new SQLiteConnectionWrapper(connectionString);
            conn.Open();
            return conn;
        }

        internal static IDbConnectionWrapper CloseConnection(IDbConnectionWrapper conn)
        {
            conn.Close();
            return conn;
        }

        internal static object? ExecuteSQLString(IDbConnectionWrapper? connWrapper, string sqlString, CommandType commandType)
        {
            if (connWrapper == null)
                throw new NoOpenSQLConnection();

            switch (commandType)
            {
                case CommandType.NonQuery:
                    ExecuteNonQueryCommand(connWrapper, sqlString);
                    return null;
                case CommandType.Reader:
                    return ExecuteReader(connWrapper, sqlString);
                default:
                    throw new NotImplementedException(commandType.ToString());
            }
        }

        private static void ExecuteNonQueryCommand(IDbConnectionWrapper connWrapper, string command)
        {
            var sqlite_cmd = connWrapper.CreateCommand();
            sqlite_cmd.CommandText = command;
            sqlite_cmd.ExecuteNonQuery();
        }

        private static IDataReader? ExecuteReader(IDbConnectionWrapper connWrapper, string command)
        {
            var sqlite_cmd = connWrapper.CreateCommand();
            sqlite_cmd.CommandText = command;
            return sqlite_cmd.ExecuteReader();
        }

        internal enum CommandType
        {
            NonQuery,
            Reader
        }
    }
}
