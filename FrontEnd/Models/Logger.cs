using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using CsvHelper;

namespace DataLogger.Models
{
    public class Logger : NotifyPropertyChanged, IDatabase
    {
        private ObservableCollection<ExerciseLog> exerciseLogs { get { return Database.ExerciseLogs; } }
        ObservableCollection<ExerciseLog> IDatabase.ExerciseLogs { get => exerciseLogs;  }

        ObservableCollection<Exercise> exercises { get { return Database.Exercises; } }
        ObservableCollection<Exercise> IDatabase.Exercises { get => exercises; }


        Exercise IDatabase.SelectExerciseByName(string name)
        {
            return exercises.SingleOrDefault(ex => ex.Name == name);
        }

        void IDatabase.AddNewExercise(Exercise exercise)
        {
            if (!exercises.Contains(exercise) && exercise.Name != "")
            {
                exercises.Add(exercise);
                OnPropertyChanged(nameof(exercise));
            }
        }

        void IDatabase.AddNewLog(ExerciseLog log)
        {
            if (!LogIsValid(log))
                throw new FailedToAddLogException("Log Is Not Valid");
            if (!LogIsUnique(log))
                return;

            exerciseLogs.Add(log);
            OnPropertyChanged(nameof(exerciseLogs));
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
