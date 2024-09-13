
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace DataLogger.Models
{
    public interface ILogger : INotifyPropertyChanged
    {
        List<ExerciseLog> ExerciseLogs { get; }
        ObservableCollection<string> Exercises { get; }

        void AddNewExercise(string exercise);
        void AddNewLog(ExerciseLog log);
    }
}
