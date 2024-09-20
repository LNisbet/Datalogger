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
        const string ExerciesesTableName = "Exercises";
        const string LogsTableName = "ExerciseLogs";
        
        const string connectionString = "Data Source=database.db; Version = 3; New = True; Compress = True; ";
        
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

        static void CreateTable(SQLiteConnection conn)
        {
            SQLiteCommand sqlite_cmd;
            List<ColumnDescription> columns = [new ColumnDescription("col1", "VARCHAR(20)"), new ColumnDescription("col2", "INT")];
            sqlite_cmd = conn.CreateCommand();
            sqlite_cmd.CommandText = SQL_Commands.CreatTableString(LogsTableName, columns);
            sqlite_cmd.ExecuteNonQuery();
            sqlite_cmd.CommandText = SQL_Commands.CreatTableString(ExerciesesTableName, columns);
            sqlite_cmd.ExecuteNonQuery();

        }

        static void InsertData(SQLiteConnection conn)
        {
            SQLiteCommand sqlite_cmd;
            sqlite_cmd = conn.CreateCommand();

            List<string> columnNames = ["Col1", "Col2"];
            List<string> values1 = ["'Test Text '", "1"];
            List<string> values2 = ["'Test1 Text1 '", "2"];
            List<string> values3 = ["'Test2 Text2 '", "3"];
            List<string> values4 = ["'Test3 Text3 '", "3"];

            sqlite_cmd.CommandText = SQL_Commands.CreatInsertDataString(LogsTableName, new DataDescription(columnNames,values1));
           sqlite_cmd.ExecuteNonQuery();
            sqlite_cmd.CommandText = SQL_Commands.CreatInsertDataString(LogsTableName, new DataDescription(columnNames, values2));
           sqlite_cmd.ExecuteNonQuery();
            sqlite_cmd.CommandText = SQL_Commands.CreatInsertDataString(LogsTableName, new DataDescription(columnNames, values3));
           sqlite_cmd.ExecuteNonQuery();
            sqlite_cmd.CommandText = SQL_Commands.CreatInsertDataString(LogsTableName, new DataDescription(columnNames, values4));
           sqlite_cmd.ExecuteNonQuery();

        }

        static void ReadData(SQLiteConnection conn)
        {
            SQLiteDataReader sqlite_datareader;
            SQLiteCommand sqlite_cmd;
            sqlite_cmd = conn.CreateCommand();
            sqlite_cmd.CommandText = SQL_Commands.CreatReadDataString(LogsTableName,"*");

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
