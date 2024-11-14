using System.Windows.Input;
using SQLight_Database;
using CSV_Exporter;
using System.Collections.ObjectModel;
using DataLogger.Models;
using DataLogger.ViewModels.HelperClasses;
using SQLight_Database.Tables.Interfaces;

namespace DataLogger.ViewModels
{
    public class BasicStatistics_VM : Base_VM
    {
        public ObservableCollection<BasicStatistics> BasicStatisticsAllExercises { get; set; }
        public BasicStatistics_VM(IExerciseTable exerciseTable, BasicStatisticsList basicStatisticsList) 
        {
            BasicStatisticsAllExercises = [];
            foreach (var exercise in exerciseTable.Exercises) 
            {
                BasicStatisticsAllExercises.Add(basicStatisticsList.GetStatisticsForExercise(exercise, DateOnly.MinValue, DateOnly.MaxValue));
            }
        }
    }
}
