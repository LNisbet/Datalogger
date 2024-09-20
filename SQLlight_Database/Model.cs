using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLight_Database;

namespace SQLight_Database
{
    public static class Model
    {
        #region Constants

        static readonly string ExercieseTableName = "Exercises";
        static readonly List<ColumnDescription> ExerciseTableDescription =
            [
            new ColumnDescription("Name", "VARCHAR(20)", "PRIMARY KEY"),
            new ColumnDescription("Type", "VARCHAR(20)", "NOT NULL"),
            new ColumnDescription("Description", "TEXT")
            ];

        static readonly string LogsTableName = "ExerciseLogs";
        static readonly List<ColumnDescription> LogTableDescription =
            [
            new ColumnDescription("Date", "DATE", "NOT NULL"),
            new ColumnDescription("Exercise", "VARCHAR(20)", "NOT NULL"),
            new ColumnDescription("Value", "FLOAT", "NOT NULL"),
            new ColumnDescription("Note", "TEXT")
            ];
        #endregion

        public static IDatabase InternalDatabase = new InternalDatabase();

        public static SQLiteConnection? sqlite_conn;

        public static void InititiliseDatabase(string dbName)
        {
            try
            {
                sqlite_conn = SQL_Commands.CreateConnection(dbName, false);
            }
            catch
            {
                sqlite_conn = SQL_Commands.CreateConnection(dbName, true);
            }
            CreateTable(ExercieseTableName, ExerciseTableDescription); //Exercise Table
            CreateTable(LogsTableName, LogTableDescription); //Log Table
        }

        private static void ExecuteSQLString(string sqlString)
        {
            if (sqlite_conn == null)
            {
                throw new NoOpenSQLConnection();
            }
            SQL_Commands.ExecuteNonQueryCommand(sqlite_conn, sqlString);
        }

        static void CreateTable(string tableName, List<ColumnDescription> tableDescription)
        {
            ExecuteSQLString(SQL_Strings.CreateTable(tableName, tableDescription));
        }

        public static void AddNewExercise(Exercise exercise)
        {
            List<string> dataDescription = [exercise.Name, exercise.Type, exercise.Description];

            ExecuteSQLString(SQL_Strings.InsertData(ExercieseTableName, dataDescription));
        }

        public static void AddNewLog(ExerciseLog log)
        {
            ExecuteSQLString(SQL_Strings.InsertData(LogsTableName, ExerciseLog_SQLConverter.ConvertToStringList(log)));
        }

        public static void ReadExerciseData()
        {
            SQLiteDataReader sqlite_datareader;
            SQLiteCommand sqlite_cmd;
            sqlite_cmd = sqlite_conn.CreateCommand();
            sqlite_cmd.CommandText = SQL_Strings.ReadData(ExercieseTableName, "*", false);

            sqlite_datareader = sqlite_cmd.ExecuteReader();
            while (sqlite_datareader.Read())
            {
                string myreader = sqlite_datareader.GetString(0);
                Console.WriteLine(myreader);
            }
        }
        public static void ReadLogData()
        {
            SQLiteDataReader sqlite_datareader;
            SQLiteCommand sqlite_cmd;
            sqlite_cmd = sqlite_conn.CreateCommand();
            sqlite_cmd.CommandText = SQL_Strings.ReadData(LogsTableName, "*", false);

            sqlite_datareader = sqlite_cmd.ExecuteReader();
            while (sqlite_datareader.Read())
            {
                string myreader = sqlite_datareader.GetString(0);
                Console.WriteLine(myreader);
            }
        }

        public static void CloseConnection()
        {
            if (sqlite_conn == null)
                return;
            SQL_Commands.CloseConnection(sqlite_conn);
        }
    }
}
