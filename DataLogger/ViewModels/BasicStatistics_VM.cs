using System.Windows.Input;
using SQLight_Database;
using CSV_Exporter;
using System.Collections.ObjectModel;
using DataLogger.Models;
using DataLogger.ViewModels.HelperClasses;

namespace DataLogger.ViewModels
{
    public class BasicStatistics_VM : Base_VM
    {
        public ObservableCollection<BasicStatistics> BasicStatisticsAllExercises { get; set; }
        public BasicStatistics_VM() 
        {
            BasicStatisticsAllExercises = [];
            foreach (var exercise in ExerciseTable.Exercises) 
            {
                BasicStatisticsAllExercises.Add(BasicStatisticsList.GetStatisticsForExercise(exercise, DateOnly.MinValue, DateOnly.MaxValue));
            }
        }
    }
}
