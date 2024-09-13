using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;

namespace DataLogger.Models
{
    public class MainModel : ILogger
    {

        private List<ExerciseLog> exerciseLogs = new();
        private ObservableCollection<string> exercises = new();

        List<ExerciseLog> ILogger.ExerciseLogs { get { return exerciseLogs; } }

        ObservableCollection<string> ILogger.Exercises { get { return exercises; } }

        void ILogger.AddNewExercise(string exercise)
        {
            if (!exercises.Contains(exercise) && exercise != "")
            {
                exercises.Add(exercise);
                OnPropertyChanged(nameof(exercise));
            }
        }

        void ILogger.AddNewLog(ExerciseLog log)
        {
            if (LogIsValid(log))
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
