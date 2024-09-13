using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DataLogger
{
    public class MainModel : ILogger , INotifyPropertyChanged
    {

        private List<ExerciseLog> exerciseLogs = new();
        private List<string> exercises = new();

        List<ExerciseLog> ILogger.ExerciseLogs {  get { return exerciseLogs; } }

        List<string> ILogger.Exercises { get { return exercises; } }

        void ILogger.AddNewExercise(string exercise)
        {
            if (!exercises.Contains(exercise) && exercise != "")
            {
                exercises.Add(exercise);
                MessageBox.Show(exercise);
                OnPropertyChanged("exercises");
            }
                
        }

        void ILogger.AddNewLog(ExerciseLog log)
        {
            if (LogIsValid(log))
            {
                exerciseLogs.Add(log);
                MessageBox.Show(log.Exercise + log.Date + log.Value);
                OnPropertyChanged("exerciseLogs");
            }
                
            else
                throw new NotImplementedException();
        }

        private bool LogIsValid(ExerciseLog log)
        {
            return (log != null && exercises.Contains(log.Exercise));
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
