
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace DataLogger.Models
{
    public interface IDatabase : INotifyPropertyChanged
    {
        ObservableCollection<ExerciseLog> ExerciseLogs { get; }
        ObservableCollection<Exercise> Exercises { get; }

        void AddNewExercise(Exercise exercise);
        void AddNewLog(ExerciseLog exerciseLog);
    }
}
