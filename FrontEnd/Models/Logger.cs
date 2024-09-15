using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using CsvHelper;

namespace DataLogger.Models
{
    public class Logger : IDatabase
    {
        private ObservableCollection<ExerciseLog> exerciseLogs { get { return Database.ExerciseLogs; } }
        ObservableCollection<ExerciseLog> IDatabase.ExerciseLogs { get => exerciseLogs;  }

        ObservableCollection<Exercise> exercises { get { return Database.Exercises; } }
        ObservableCollection<Exercise> IDatabase.Exercises { get => exercises; }

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
            if (LogIsValid(log) && LogIsUnique(log))
            {
                exerciseLogs.Add(log);
                OnPropertyChanged(nameof(exerciseLogs));
            }

            else
                throw new NotImplementedException();
        }

        private bool LogIsValid(ExerciseLog log)
        {
            return log != null && exercises.Contains(log.Exercise);
        }
        private bool LogIsUnique(ExerciseLog log)
        {
            return !exerciseLogs.Contains(log);
        }

        #region INotifyPropertyChanged Members
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion
       
    }
}
