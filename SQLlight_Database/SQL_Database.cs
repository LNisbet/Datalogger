using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data.SQLite;

namespace SQLight_Database
{
    public static class SQL_Database
    {
        #region Fields
        private static ObservableCollection<Exercise>? exercises = null;
        public static ObservableCollection<Exercise> Exercises 
        { 
            get
            {
                if (exercises == null)
                {
                    exercises = [];
                    ReadAllExercises();
                }
                return exercises ??= [];
            }
        }

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

        public static ObservableCollection<string> AllExerciseNames {get => new ObservableCollection<string>(Exercises.Select(exercise => exercise.Name).Distinct()); }

        private static ObservableCollection<string> allExerciseTags  = new();
        public static ObservableCollection<string> AllExerciseTags { get { ReadAllExerciseTags(); return allExerciseTags; } }

        private static SQLiteConnection? sqlite_conn;

        //private static SQLiteTransaction? sqlite_transaction; Find out what this is for
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
            CreateTable(Config.TagsTableName, Config.TagsTableDescription); //Tags Table
            AddMultipleTags(Config.StandardTags);
            CreateTable(Config.ExercieseTableName, Config.ExerciseTableDescription); //Exercise Table
            AddMultipleExercises(Config.StandardExercises);
            CreateTable(Config.LogsTableName, Config.LogTableDescription); //Log Table
        }

        #region Exerceises
        public static void AddSingleExercise(Exercise exercise)
        {
            if (!AllExerciseNames.Contains(exercise.Name))
            {
                ExecuteSQLString(SQL_Strings.InsertData(Config.ExercieseTableName, exercise.ToSQLStringList()), CommandType.NonQuery);
                foreach(var tag in exercise.Tags)
                {
                    if (!allExerciseTags.Contains(tag))
                        AddSingleTag(tag);
                }

                ReadAllExercises();
            }
        }

        public static void AddMultipleExercises(List<Exercise> exercises)
        {
            foreach (var exercise in exercises) 
                if (!AllExerciseNames.Contains(exercise.Name))
                {
                    ExecuteSQLString(SQL_Strings.InsertData(Config.ExercieseTableName, exercise.ToSQLStringList()), CommandType.NonQuery);
                    foreach (var tag in exercise.Tags)
                    {
                        if (!allExerciseTags.Contains(tag))
                            AddSingleTag(tag);
                    }
                }
                
            ReadAllExercises();
        }

        public static void RemoveSingleTag(string tag)
        {
            ExecuteSQLString(SQL_Strings.DeleteFromTable(Config.TagsTableName, $"Tags='{tag}'"), CommandType.NonQuery);
            ReadAllExerciseTags();
        }

        public static void RemoveSingleExercise(Exercise exercise)
        {
            ExecuteSQLString(SQL_Strings.DeleteFromTable(Config.ExercieseTableName,$"Name='{exercise.Name}'"),CommandType.NonQuery);
            ReadAllExercises();
        }

        public static void ReadAllExercises()
        {
            var sqlite_datareader = ExecuteSQLString(SQL_Strings.ReadData(Config.ExercieseTableName, "*", false), CommandType.Reader) as SQLiteDataReader;
            exercises.Clear();

            while (sqlite_datareader != null && sqlite_datareader.Read())
            {
                exercises.Add(new Exercise(sqlite_datareader));
            }
        }

        public static Exercise SelectExerciseByName(string name)
        {
            var ex = Exercises.SingleOrDefault(ex => ex.Name == name);
            return ex ?? throw new ExerciseNotFoundException(name);
        }
        #endregion

        #region Logs
        public static void AddSingleLog(ExerciseLog log)
        {
            if (!Logs.Contains(log))
            {
                ExecuteSQLString(SQL_Strings.InsertData(Config.LogsTableName, log.ToSQLStringList()), CommandType.NonQuery);
                ReadAllLogs();
            }
        }

        public static void AddMultipleLogs(List<ExerciseLog> logs)
        {
            foreach (var log in logs) if (!Logs.Contains(log))
                    ExecuteSQLString(SQL_Strings.InsertData(Config.LogsTableName, log.ToSQLStringList()), CommandType.NonQuery);
            ReadAllLogs();
        }

        public static void RemoveSingleLog(ExerciseLog log)
        {
            ExecuteSQLString(SQL_Strings.DeleteFromTable(Config.LogsTableName, $"Id={log.Id}"), CommandType.NonQuery);
            ReadAllLogs();
        }

        public static void ReadAllLogs()
        {
            var sqlite_datareader = ExecuteSQLString(SQL_Strings.ReadData(Config.LogsTableName, "*", false), CommandType.Reader) as SQLiteDataReader;
            logs.Clear();

            while (sqlite_datareader != null && sqlite_datareader.Read())
            {
                logs.Add(new ExerciseLog(sqlite_datareader));
            }
        }
        #endregion

        public static void CloseConnection()
        {
            if (sqlite_conn == null)
                return;
            SQL_Commands.CloseConnection(sqlite_conn);
        }

        public static void DeleteDatabase(string dbName)
        {
            ExecuteSQLString(SQL_Strings.DeleteDatabase(dbName), CommandType.NonQuery);
            CloseConnection();
        }

        #region Private Methods
        private enum CommandType
        {
            NonQuery,
            Reader
        }

        private static void AddSingleTag(string tag)
        {
            if (!AllExerciseTags.Contains(tag))
            {
                ExecuteSQLString(SQL_Strings.InsertData(Config.TagsTableName, [$"'{tag}'"]), CommandType.NonQuery);
                ReadAllExerciseTags();
            }
        }

        private static void AddMultipleTags(List<string> tags)
        {
            foreach (var tag in tags)
                if (!AllExerciseTags.Contains(tag))
                    ExecuteSQLString(SQL_Strings.InsertData(Config.TagsTableName, [$"'{tag}'"]), CommandType.NonQuery);

            ReadAllExerciseTags();
        }

        private static void ReadAllExerciseTags()
        {
            var sqlite_datareader = ExecuteSQLString(SQL_Strings.ReadData(Config.TagsTableName, "*", true), CommandType.Reader) as SQLiteDataReader;

            while (sqlite_datareader != null && sqlite_datareader.Read())
            {
                string tag = sqlite_datareader.GetString(0);
                if (!allExerciseTags.Contains(tag))
                    allExerciseTags.Add(tag);
            }
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

        private static void CreateTable(string tableName, List<ColumnDescription> tableDescription)
        {
            ExecuteSQLString(SQL_Strings.CreateTable(tableName, tableDescription), CommandType.NonQuery);
        }
        #endregion
    }
}
