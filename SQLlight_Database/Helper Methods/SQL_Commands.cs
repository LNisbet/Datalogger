using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Data.Entity.Infrastructure.Design.Executor;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SQLight_Database
{
    static public class SQL_Commands
    {

        public static SQLiteConnection CreateConnection(string dbName, bool DbExists)
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

        public static SQLiteConnection CloseConnection(SQLiteConnection conn)
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

        public static void ExecuteNonQueryCommand(SQLiteConnection conn, string command)
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
        public static SQLiteDataReader? ExecuteReader(SQLiteConnection conn, string command)
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
