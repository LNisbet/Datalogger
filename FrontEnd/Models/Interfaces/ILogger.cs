
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace DataLogger.Models
{
    public interface ILogger : INotifyPropertyChanged
    {
        List<ExerciseLog> ExerciseLogs { get; }
        ObservableCollection<Exercise> Exercises { get; }

        void AddNewExercise(Exercise exercise);
        void AddNewLog(ExerciseLog log);
    }
}
