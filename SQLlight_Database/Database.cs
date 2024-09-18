using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLight_Database;
using System.Data.SQLite;

namespace SQLight_Database
{
    public class Database : IDatabase
    {
        const string connectionString = "Data Source=database.db; Version = 3; New = True; Compress = True; ";

        List<string> IDatabase.columnNames => throw new NotImplementedException();

        SQLiteConnection sqlite_conn;

        Database()
        {
            sqlite_conn = CreateConnection();
        }

        private SQLiteConnection CreateConnection()
        {
            // Create a new database connection
            if (sqlite_conn == null)
                sqlite_conn = new SQLiteConnection(connectionString);
            // Open the connection:
            try
            {
                sqlite_conn.Open();
            }
            catch (Exception ex)
            {

            }
            return sqlite_conn;
        }

        private SQLiteConnection CloseConnection()
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

        void IDatabase.AddColumn(string columnName, string variableType)
        {
            SQLiteCommand sqlite_cmd;

            sqlite_cmd = sqlite_conn.CreateCommand();
            sqlite_cmd.CommandText = SQL_Helper.CreatTableString(tableName, columns);
            sqlite_cmd.ExecuteNonQuery();
        }

        void IDatabase.AddRow(List<string> values)
        {
            throw new NotImplementedException();
        }

        void IDatabase.CreateDatabase()
        {
            throw new NotImplementedException();
        }

        void IDatabase.DeleteColumn(string columnName)
        {
            throw new NotImplementedException();
        }

        void IDatabase.DeleteDatabase()
        {
            throw new NotImplementedException();
        }

        void IDatabase.DeleteRow(int id)
        {
            throw new NotImplementedException();
        }

        List<string> IDatabase.GetRowById(int id)
        {
            throw new NotImplementedException();
        }

        List<List<string>> IDatabase.GetRowsByDataMatch(string columnName, string value)
        {
            throw new NotImplementedException();
        }
    }
}
