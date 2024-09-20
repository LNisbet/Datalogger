
using System.Collections.ObjectModel;
using System.ComponentModel;
using SQLight_Database;

namespace SQLight_Database
{
    public interface IDatabase : INotifyPropertyChanged
    {
        ObservableCollection<ExerciseLog> ExerciseLogs { get; set; }
        ObservableCollection<Exercise> Exercises { get; set; }

        ObservableCollection<string> AllExerciseNames { get; }
        ObservableCollection<string> AllExerciseTypes { get; }

        Exercise SelectExerciseByName(string name);

        void AddNewExerciseType(string exerciseType);
        void AddNewExercise(Exercise exercise);
        void AddNewLog(ExerciseLog exerciseLog);
    }
}
