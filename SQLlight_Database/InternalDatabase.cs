using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using CsvHelper;
using SQLight_Database;

namespace SQLight_Database
{
    public class InternalDatabase : NotifyPropertyChanged, IDatabase
    {
        #region Fields
        private ObservableCollection<ExerciseLog> exerciseLogs = [];
        ObservableCollection<ExerciseLog> IDatabase.ExerciseLogs { get => exerciseLogs; set => exerciseLogs = value; }

        private ObservableCollection<Exercise> exercises = [];
        ObservableCollection<Exercise> IDatabase.Exercises { get => exercises; set => exercises = value; }

        private ObservableCollection<string> allExerciseNames { get => new ObservableCollection<string>(exercises.Select(exercise => exercise.Name)); }
        ObservableCollection<string> IDatabase.AllExerciseNames { get => allExerciseNames; }

        private ObservableCollection<string> allExerciseTypes = [];
        ObservableCollection<string> IDatabase.AllExerciseTypes { get => allExerciseTypes; }
        #endregion

        Exercise IDatabase.SelectExerciseByName(string name)
        {
            var ex = exercises.SingleOrDefault(ex => ex.Name == name);
            if (ex == null)
                throw new ExerciseNotFoundException(name);
            return ex;
        }

        void IDatabase.AddNewExerciseType(string exerciseType)
        {
            if (!allExerciseTypes.Contains(exerciseType) && exerciseType != "")
            {
                allExerciseTypes.Add(exerciseType);
            }
        }

        void IDatabase.AddNewExercise(Exercise exercise)
        {
            if (!allExerciseNames.Contains(exercise.Name) && exercise.Name != "")
            {
                exercises.Add(exercise);
            }
        }

        void IDatabase.AddNewLog(ExerciseLog log)
        {
            if (!LogIsValid(log))
                throw new FailedToAddLogException("Log Is Not Valid");
            if (!LogIsUnique(log))
                return;

            exerciseLogs.Add(log);
        }

        private bool LogIsValid(ExerciseLog log)
        {
            return log != null && exercises.Contains(log.Exercise);
        }
        private bool LogIsUnique(ExerciseLog log)
        {
            return !exerciseLogs.Contains(log);
        }

    }
}
