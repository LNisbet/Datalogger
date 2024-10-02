using System.Windows.Input;
using SQLight_Database;
using CSV_Exporter;
using System.Collections.ObjectModel;

namespace DataLogger.ViewModels
{
    internal class BasicStatistics_VM
    {
        ObservableCollection<BasicStatistics> BasicStatistics_AllExercises { get; set; }
        BasicStatistics_VM() 
        {
            BasicStatistics_AllExercises = [];
            foreach (var exercise in ExerciseTable.Exercises) 
            {
                BasicStatistics_AllExercises.Add(new BasicStatistics(exercise, DateOnly.MinValue, DateOnly.MaxValue));
            }
        }
    }
}
