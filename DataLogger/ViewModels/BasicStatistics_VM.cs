using System.Windows.Input;
using SQLight_Database;
using CSV_Exporter;
using System.Collections.ObjectModel;
using DataLogger.Models;
using DataLogger.ViewModels.HelperClasses;
using SQLight_Database.Tables.Interfaces;
using SQLight_Database.Models;

namespace DataLogger.ViewModels
{
    public class BasicStatistics_VM : Base_VM
    {
        private readonly BasicStatisticsStore _basicStatisticsStore;
        public ObservableCollection<BasicStatistics> BasicStatisticsAllExercises => _basicStatisticsStore.ListOfBasicStatistics;
        public BasicStatistics_VM(BasicStatisticsStore basicStatisticsStore, ITable<Exercise> exerciseTable) 
        {
            _basicStatisticsStore = basicStatisticsStore;
            foreach (var exercise in exerciseTable.Values) 
            {
                basicStatisticsStore.Get(exercise, DateOnly.MinValue, DateOnly.MaxValue);
            }
        }
    }
}
