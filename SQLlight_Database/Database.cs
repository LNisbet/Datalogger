using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLight_Database;

namespace SQLight_Database
{
    public static class Database
    {
        static List<string> ColumnNames => throw new NotImplementedException();

        static SQLiteConnection? sqlite_conn;
        
        public static SQLiteConnection CreateConnection(string dBName)
        {
            string newConnectionString = SQL_Helper.CreatConnectionString(dBName,3,true,true);
            string ExistingConnectionString = SQL_Helper.CreatConnectionString(dBName, 3, false, true);

            // Create a new database connection
            if (sqlite_conn == null)
                sqlite_conn = new SQLiteConnection(newConnectionString);
            else
                sqlite_conn = new SQLiteConnection(ExistingConnectionString);
            // Open the connection:
            try
            {
                sqlite_conn.Open();
            }
            catch (Exception ex)
            {
                sqlite_conn = new SQLiteConnection(ExistingConnectionString);
                sqlite_conn.Open();
            }
            return sqlite_conn;
        }

        public static SQLiteConnection CloseConnection(string dBName)
        {
            // close the connection:
            try
            {
                sqlite_conn.Close();
            }
            catch (Exception ex)
            {

            }
            return sqlite_conn;
        }

        public static void DeleteDatabase(string dBName)
        {
            throw new NotImplementedException();
        }

        public static void CreateTable(string tableName, List<ColumnDescription> columns)
        {
            SQLiteCommand sqlite_cmd = sqlite_conn.CreateCommand();
            sqlite_cmd.CommandText = SQL_Helper.CreatTableString(tableName, columns);
            sqlite_cmd.ExecuteNonQuery();
        }

        public static void AddRow(string tableName, List<string> columnNames, List<string> values)
        {
            SQLiteCommand sqlite_cmd;
            sqlite_cmd = sqlite_conn.CreateCommand();

            sqlite_cmd.CommandText = SQL_Helper.CreatInsertDataString(tableName, new DataDescription(columnNames, values));
            sqlite_cmd.ExecuteNonQuery();
        }

        public static void DeleteColumn(string tableName, string columnName)
        {
            throw new NotImplementedException();
        }

        public static void DeleteRow(string tableName, int id)
        {
            throw new NotImplementedException();
        }

        public static List<string> GetRowById(string tableName, int id)
        {
            throw new NotImplementedException();
        }

        public static List<List<string>> GetRowsByDataMatch(string tableName, string columnName, string value)
        {
            throw new NotImplementedException();
        }
    }
}
