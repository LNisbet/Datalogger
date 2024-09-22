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
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                conn = new SQLiteConnection(ExistingConnectionString);
                conn.Open();
            }
            return conn;
        }

        internal static SQLiteConnection CloseConnection(SQLiteConnection conn)
        {
            // close the connection:
            try
            {
                conn.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
            }
            return conn;
        }

        internal static void ExecuteNonQueryCommand(SQLiteConnection conn, string command)
        {
            SQLiteCommand sqlite_cmd = conn.CreateCommand();
            sqlite_cmd.CommandText = command;
            try
            {
                Console.WriteLine($"Command: {command}");
                sqlite_cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
            }
        }
        internal static SQLiteDataReader? ExecuteReader(SQLiteConnection conn, string command)
        {
            SQLiteDataReader? sqlite_datareader = null;
            SQLiteCommand sqlite_cmd = conn.CreateCommand();
            sqlite_cmd.CommandText = command;
            try
            {
                Console.WriteLine($"Command: {command}");
                sqlite_datareader = sqlite_cmd.ExecuteReader();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
            }

            return sqlite_datareader;
        }
    }
}
