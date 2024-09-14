using System.Collections.ObjectModel;

namespace DataLogger.Models
{
    internal interface IDatabase
    {
        List<ExerciseLog> ExerciseLogs { get; set; }
        ObservableCollection<string> Exercises { get; set; }

    }
}
