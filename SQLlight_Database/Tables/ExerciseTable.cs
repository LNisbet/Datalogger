using SQLight_Database.Database.Interfaces;
using SQLight_Database.Tables.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLight_Database
{
    public class ExerciseTable : IExerciseTable
    {
        private readonly ITagsTable _tagsTable;
        private readonly DatabaseConnectionStore _databaseConnectionStore;

        private ObservableCollection<Exercise>? exercises;
        public ObservableCollection<Exercise> Exercises
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

        public ObservableCollection<string> AllExerciseNames  => new ObservableCollection<string>(Exercises.Select(exercise => exercise.Name).Distinct());

        public ExerciseTable(ITagsTable tagsTable, DatabaseConnectionStore databaseConnectionStore)
        {
            _tagsTable = tagsTable;
            _databaseConnectionStore = databaseConnectionStore;
        }
        
        public void AddSingleExercise(Exercise exercise)
        {
            if (!AllExerciseNames.Contains(exercise.Name))
            {
                SQL_Commands.ExecuteSQLString(_databaseConnectionStore.SQLite_conn, SQL_Strings.InsertData(Config.ExercieseTableName, exercise.ToSQLStringList()), SQL_Commands.CommandType.NonQuery);
                foreach (var tag in exercise.Tags)
                {
                    if (!_tagsTable.AllExerciseTags.Contains(tag))
                        _tagsTable.AddSingleTag(tag);
                }

                ReadAllExercises();
            }
        }

        public void AddMultipleExercises(List<Exercise> exercises)
        {
            foreach (var exercise in exercises)
                if (!AllExerciseNames.Contains(exercise.Name))
                {
                    SQL_Commands.ExecuteSQLString(_databaseConnectionStore.SQLite_conn, SQL_Strings.InsertData(Config.ExercieseTableName, exercise.ToSQLStringList()), SQL_Commands.CommandType.NonQuery);
                    foreach (var tag in exercise.Tags)
                    {
                        if (!_tagsTable.AllExerciseTags.Contains(tag))
                            _tagsTable.AddSingleTag(tag);
                    }
                }

            ReadAllExercises();
        }

        public void RemoveSingleExercise(Exercise exercise)
        {
            SQL_Commands.ExecuteSQLString(_databaseConnectionStore.SQLite_conn, SQL_Strings.DeleteFromTable(Config.ExercieseTableName, $"Name='{exercise.Name}'"), SQL_Commands.CommandType.NonQuery);
            ReadAllExercises();
        }

        public void RemoveMultipleExercises(List<Exercise> exercises)
        {
            throw new NotImplementedException();
        }

        public Exercise SelectExerciseByName(string name)
        {
            var ex = Exercises.SingleOrDefault(ex => ex.Name == name);
            return ex ?? throw new ExerciseNotFoundException(name);
        }

        private void ReadAllExercises()
        {
            var sqlite_datareader = SQL_Commands.ExecuteSQLString(_databaseConnectionStore.SQLite_conn, SQL_Strings.ReadData(Config.ExercieseTableName, "*", false), SQL_Commands.CommandType.Reader) as SQLiteDataReader;
            exercises.Clear();

            while (sqlite_datareader != null && sqlite_datareader.Read())
            {
                exercises.Add(new Exercise(sqlite_datareader));
            }
        }
    }
}
