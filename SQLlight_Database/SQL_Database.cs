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

        public static void InititiliseDatabase(string dbName, bool newDb)
        {
            try
            {
                sqlite_conn = SQL_Commands.CreateConnection(dbName, false);
            }
            catch
            {
                sqlite_conn = SQL_Commands.CreateConnection(dbName, true);
            }
            if (newDb)
            {
                CreateTable(Config.TagsTableName, Config.TagsTableDescription); //Tags Table
                AddMultipleTags(Config.StandardTags);
                CreateTable(Config.ExercieseTableName, Config.ExerciseTableDescription); //Exercise Table
                AddMultipleExercises(Config.StandardExercises);
                CreateTable(Config.LogsTableName, Config.LogTableDescription); //Log Table
            }
        }

        #region Exerceises
        public static void AddSingleExercise(Exercise exercise)
        {
            if (!AllExerciseNames.Contains(exercise.Name))
            {
                SQL_Commands.ExecuteSQLString(sqlite_conn, SQL_Strings.InsertData(Config.ExercieseTableName, exercise.ToSQLStringList()), Enums.CommandType.NonQuery);
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
                    SQL_Commands.ExecuteSQLString(sqlite_conn, SQL_Strings.InsertData(Config.ExercieseTableName, exercise.ToSQLStringList()), Enums.CommandType.NonQuery);
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
            SQL_Commands.ExecuteSQLString(sqlite_conn, SQL_Strings.DeleteFromTable(Config.TagsTableName, $"Tags='{tag}'"), Enums.CommandType.NonQuery);
            ReadAllExerciseTags();
        }

        public static void RemoveSingleExercise(Exercise exercise)
        {
            SQL_Commands.ExecuteSQLString(sqlite_conn, SQL_Strings.DeleteFromTable(Config.ExercieseTableName,$"Name='{exercise.Name}'"), Enums.CommandType.NonQuery);
            ReadAllExercises();
        }

        public static void ReadAllExercises()
        {
            var sqlite_datareader = SQL_Commands.ExecuteSQLString(sqlite_conn, SQL_Strings.ReadData(Config.ExercieseTableName, "*", false), Enums.CommandType.Reader) as SQLiteDataReader;
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
            if (IsLogUnique(log))
            {
                SQL_Commands.ExecuteSQLString(sqlite_conn, SQL_Strings.InsertData(Config.LogsTableName, log.ToSQLStringList()), Enums.CommandType.NonQuery);
                ReadAllLogs();
            }
        }

        public static void AddMultipleLogs(List<ExerciseLog> logs)
        {
            foreach (var log in logs) if (IsLogUnique(log))
                    SQL_Commands.ExecuteSQLString(sqlite_conn, SQL_Strings.InsertData(Config.LogsTableName, log.ToSQLStringList()), Enums.CommandType.NonQuery);
            ReadAllLogs();
        }

        public static void RemoveSingleLog(ExerciseLog log)
        {
            SQL_Commands.ExecuteSQLString(sqlite_conn, SQL_Strings.DeleteFromTable(Config.LogsTableName, $"Id={log.Id}"), Enums.CommandType.NonQuery);
            ReadAllLogs();
        }

        public static void ReadAllLogs()
        {
            var sqlite_datareader = SQL_Commands.ExecuteSQLString(sqlite_conn, SQL_Strings.ReadData(Config.LogsTableName, "*", false), Enums.CommandType.Reader) as SQLiteDataReader;
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
            SQL_Commands.ExecuteSQLString(sqlite_conn, SQL_Strings.DeleteDatabase(dbName), Enums.CommandType.NonQuery);
            CloseConnection();
        }

        #region Private Methods

        private static void AddSingleTag(string tag)
        {
            if (!AllExerciseTags.Contains(tag))
            {
                SQL_Commands.ExecuteSQLString(sqlite_conn, SQL_Strings.InsertData(Config.TagsTableName, [$"'{tag}'"]), Enums.CommandType.NonQuery);
                ReadAllExerciseTags();
            }
        }

        private static void AddMultipleTags(List<string> tags)
        {
            foreach (var tag in tags)
                if (!AllExerciseTags.Contains(tag))
                    SQL_Commands.ExecuteSQLString(sqlite_conn, SQL_Strings.InsertData(Config.TagsTableName, [$"'{tag}'"]), Enums.CommandType.NonQuery);

            ReadAllExerciseTags();
        }

        private static void ReadAllExerciseTags()
        {
            var sqlite_datareader = SQL_Commands.ExecuteSQLString(sqlite_conn, SQL_Strings.ReadData(Config.TagsTableName, "*", true), Enums.CommandType.Reader) as SQLiteDataReader;

            while (sqlite_datareader != null && sqlite_datareader.Read())
            {
                string tag = sqlite_datareader.GetString(0);
                if (!allExerciseTags.Contains(tag))
                    allExerciseTags.Add(tag);
            }
        }

        private static void CreateTable(string tableName, List<ColumnDescription> tableDescription)
        {
            SQL_Commands.ExecuteSQLString(sqlite_conn, SQL_Strings.CreateTable(tableName, tableDescription), Enums.CommandType.NonQuery);
        }

        private static bool IsLogUnique(ExerciseLog log)
        {
            return !(Logs.Any(l => l.Date.Equals(log.Date)) &&
                Logs.Any(l => l.Exercise.Name.Equals(log.Exercise.Name)) &&
                Logs.Any(l => l.Value1.Equals(log.Value1)) &&
                Logs.Any(l => l.Value2.Equals(log.Value2)) &&
                Logs.Any(l => l.Value3.Equals(log.Value3)) &&
                Logs.Any(l => l.Value4.Equals(log.Value4)));
        }
        #endregion
    }
}
