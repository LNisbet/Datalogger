using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLight_Database
{
    public class ExerciseTable
    {
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
                return exercises;
            }
        }

        public static ObservableCollection<string> AllExerciseNames  => new ObservableCollection<string>(Exercises.Select(exercise => exercise.Name).Distinct());

        
        public static void AddSingleExercise(Exercise exercise)
        {
            if (!AllExerciseNames.Contains(exercise.Name))
            {
                SQL_Commands.ExecuteSQLString(DatabaseConnection.SQLite_conn, SQL_Strings.InsertData(Config.ExercieseTableName, exercise.ToSQLStringList()), Enums.CommandType.NonQuery);
                foreach (var tag in exercise.Tags)
                {
                    if (!TagsTable.AllExerciseTags.Contains(tag))
                        TagsTable.AddSingleTag(tag);
                }

                ReadAllExercises();
            }
        }

        public static void AddMultipleExercises(List<Exercise> exercises)
        {
            foreach (var exercise in exercises)
                if (!AllExerciseNames.Contains(exercise.Name))
                {
                    SQL_Commands.ExecuteSQLString(DatabaseConnection.SQLite_conn, SQL_Strings.InsertData(Config.ExercieseTableName, exercise.ToSQLStringList()), Enums.CommandType.NonQuery);
                    foreach (var tag in exercise.Tags)
                    {
                        if (!TagsTable.AllExerciseTags.Contains(tag))
                            TagsTable.AddSingleTag(tag);
                    }
                }

            ReadAllExercises();
        }

        public static void RemoveSingleExercise(Exercise exercise)
        {
            SQL_Commands.ExecuteSQLString(DatabaseConnection.SQLite_conn, SQL_Strings.DeleteFromTable(Config.ExercieseTableName, $"Name='{exercise.Name}'"), Enums.CommandType.NonQuery);
            ReadAllExercises();
        }

        public static void ReadAllExercises()
        {
            var sqlite_datareader = SQL_Commands.ExecuteSQLString(DatabaseConnection.SQLite_conn, SQL_Strings.ReadData(Config.ExercieseTableName, "*", false), Enums.CommandType.Reader) as SQLiteDataReader;
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
    }
}
