using SQLight_Database;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLite_Database
{
    class Program
    {
        const string tableName = "SampleTable";
        const string tableName1 = "SampleTable1";
        
        static void Main(string[] args)
        {
            SQLiteConnection sqlite_conn;
            sqlite_conn = CreateConnection();
            CreateTable(sqlite_conn);
            InsertData(sqlite_conn);
            ReadData(sqlite_conn);
        }

        static SQLiteConnection CreateConnection()
        {

            SQLiteConnection sqlite_conn;
            // Create a new database connection:
            sqlite_conn = new SQLiteConnection("Data Source=database.db; Version = 3; New = True; Compress = True; ");
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

        static void CreateTable(SQLiteConnection conn)
        {
            SQL_Helper sqlHelper = new();
            SQLiteCommand sqlite_cmd;
            List<ColumnDescription> columns = [new ColumnDescription("col1", "VARCHAR(20)"), new ColumnDescription("col2", "INT")];
            sqlite_cmd = conn.CreateCommand();
            sqlite_cmd.CommandText = sqlHelper.CreatTableString(tableName, columns);
            sqlite_cmd.ExecuteNonQuery();
            sqlite_cmd.CommandText = sqlHelper.CreatTableString(tableName1, columns);
            sqlite_cmd.ExecuteNonQuery();

        }

        static void InsertData(SQLiteConnection conn)
        {
            SQL_Helper sqlHelper = new();
            SQLiteCommand sqlite_cmd;
            sqlite_cmd = conn.CreateCommand();

            List<string> columnNames = ["Col1", "Col2"];
            List<string> values1 = ["'Test Text '", "1"];
            List<string> values2 = ["'Test1 Text1 '", "2"];
            List<string> values3 = ["'Test2 Text2 '", "3"];
            List<string> values4 = ["'Test3 Text3 '", "3"];

            sqlite_cmd.CommandText = sqlHelper.CreatInsertDataString(tableName, new DataDescription(columnNames,values1));
           sqlite_cmd.ExecuteNonQuery();
            sqlite_cmd.CommandText = sqlHelper.CreatInsertDataString(tableName, new DataDescription(columnNames, values2));
           sqlite_cmd.ExecuteNonQuery();
            sqlite_cmd.CommandText = sqlHelper.CreatInsertDataString(tableName, new DataDescription(columnNames, values3));
           sqlite_cmd.ExecuteNonQuery();
            sqlite_cmd.CommandText = sqlHelper.CreatInsertDataString(tableName, new DataDescription(columnNames, values4));
           sqlite_cmd.ExecuteNonQuery();

        }

        static void ReadData(SQLiteConnection conn)
        {
            SQL_Helper sqlHelper = new();
            SQLiteDataReader sqlite_datareader;
            SQLiteCommand sqlite_cmd;
            sqlite_cmd = conn.CreateCommand();
            sqlite_cmd.CommandText = sqlHelper.CreatReadDataString(tableName,"*");

            sqlite_datareader = sqlite_cmd.ExecuteReader();
            while (sqlite_datareader.Read())
            {
                string myreader = sqlite_datareader.GetString(0);
                Console.WriteLine(myreader);
            }
            conn.Close();
        }
    }
}
