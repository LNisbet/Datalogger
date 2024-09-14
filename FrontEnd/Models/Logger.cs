using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using CsvHelper;

namespace DataLogger.Models
{
    public class Logger : ILogger
    {

        List<ExerciseLog> ILogger.ExerciseLogs { get { return Database.ExerciseLogs; } }

        ObservableCollection<string> ILogger.Exercises { get { return Database.Exercises; } }

        void ILogger.AddNewExercise(string exercise)
        {
            if (!Database.Exercises.Contains(exercise) && exercise != "")
            {
                Database.Exercises.Add(exercise);
                OnPropertyChanged(nameof(exercise));
            }
        }

        void ILogger.AddNewLog(ExerciseLog log)
        {
            if (LogIsValid(log))
            {
                Database.ExerciseLogs.Add(log);
                OnPropertyChanged(nameof(Database.ExerciseLogs));
            }

            else
                throw new NotImplementedException();
        }

        private bool LogIsValid(ExerciseLog log)
        {
            return log != null && Database.Exercises.Contains(log.Exercise);
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
