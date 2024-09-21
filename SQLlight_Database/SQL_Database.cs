using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SQLite;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using SQLight_Database;

namespace SQLight_Database
{
    public static class SQL_Database
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

        #region Fields
        private static ObservableCollection<Exercise>? exercises = null;
        public static ObservableCollection<Exercise> Exercises 
        { 
            get{if (exercises == null)
                {
                    exercises = [];
                    ReadAllExercises();
                }
                return exercises ??= [];}}

        private static ObservableCollection<ExerciseLog>? logs = null;
        public static ObservableCollection<ExerciseLog> Logs
        {
            get
            {
                if (logs == null)
                {
                    logs = [];
                    ReadAllLogs();
                }
                return logs ??= [];
            }
        }

        public static ObservableCollection<string> AllExerciseNames { get => new ObservableCollection<string>(Exercises.Select(exercise => exercise.Name)); }

        public static ObservableCollection<string> AllExerciseTypes { get => new ObservableCollection<string>(Exercises.Select(exercise => exercise.Type)); }

        private static SQLiteConnection? sqlite_conn;
        #endregion

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

        public static void CreateTable(string tableName, List<ColumnDescription> tableDescription)
        {
            ExecuteSQLString(SQL_Strings.CreateTable(tableName, tableDescription), CommandType.NonQuery);
        }

        public static void AddNewExercise(Exercise exercise)
        {
            ExecuteSQLString(SQL_Strings.InsertData(ExercieseTableName, exercise.ToSQLStringList()), CommandType.NonQuery);
            ReadAllExercises();
        }

        public static void AddNewLog(ExerciseLog log)
        {
            ExecuteSQLString(SQL_Strings.InsertData(LogsTableName, log.ToSQLStringList()), CommandType.NonQuery);
            ReadAllLogs();
        }

        public static void ReadAllExercises()
        {
            var sqlite_datareader = ExecuteSQLString(SQL_Strings.ReadData(ExercieseTableName, "*", false), CommandType.Reader) as SQLiteDataReader;
            exercises.Clear();

            while (sqlite_datareader != null && sqlite_datareader.Read())
            {
                exercises.Add(new Exercise(sqlite_datareader));
            }
        }

        public static Exercise SelectExerciseByName(string name)
        {
            var ex = Exercises.SingleOrDefault(ex => ex.Name == name);
            return ex == null ? throw new ExerciseNotFoundException(name) : ex;
        }

        public static void ReadAllLogs()
        {
            var sqlite_datareader = ExecuteSQLString(SQL_Strings.ReadData(LogsTableName, "*", false), CommandType.Reader) as SQLiteDataReader;
            logs.Clear();

            while (sqlite_datareader != null && sqlite_datareader.Read())
            {
                logs.Add(new ExerciseLog(sqlite_datareader));
            }
        }

        public static void CloseConnection()
        {
            if (sqlite_conn == null)
                return;
            SQL_Commands.CloseConnection(sqlite_conn);
        }

        #region Private Methods
        private enum CommandType
        {
            NonQuery,
            Reader
        }
        private static object? ExecuteSQLString(string sqlString, CommandType commandType)
                {
                    if (sqlite_conn == null)
                        throw new NoOpenSQLConnection();

                    switch (commandType)
                    {
                        case CommandType.NonQuery:
                            SQL_Commands.ExecuteNonQueryCommand(sqlite_conn, sqlString);
                            return null;
                        case CommandType.Reader:
                            return SQL_Commands.ExecuteReader(sqlite_conn, sqlString);
                        default:
                            throw new NotImplementedException(commandType.ToString());

                    }
                }

        #endregion
    }
}
