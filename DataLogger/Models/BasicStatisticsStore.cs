using DataLogger.Views;
using SQLight_Database.Models;
using SQLight_Database.Tables.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DataLogger.ViewModels.HandStatisticsOverview_VM;

namespace DataLogger.Models
{
    public class BasicStatisticsStore
    {
        private readonly ITable<Exercise> _exerciseTable;
        private readonly ITable<ExerciseLog> _logsTable;

        internal ObservableCollection<BasicStatistics> ListOfBasicStatistics { get; private set; } = [];

        public BasicStatisticsStore (ITable<ExerciseLog> logsTable, ITable<Exercise> exerciseTable)
        {
            _exerciseTable = exerciseTable;
            _logsTable = logsTable;
        }

        internal BasicStatistics Get(string exerciseName, DateOnly startDate, DateOnly endDate)
        {
            BasicStatistics stat = ListOfBasicStatistics
                .FirstOrDefault(st => st.Exercise.Name == exerciseName && st.StartDate == startDate && st.EndDate == endDate)
                ?? new BasicStatistics(_logsTable, _exerciseTable.SelectByName(exerciseName), startDate, endDate);

            if (!ListOfBasicStatistics.Contains(stat))
                ListOfBasicStatistics.Add(stat);

            return stat;
        }

        internal BasicStatistics Get(Exercise exercise, DateOnly startDate, DateOnly endDate)
        {
            BasicStatistics stat = ListOfBasicStatistics
                .FirstOrDefault(st => st.Exercise == exercise && st.StartDate == startDate && st.EndDate == endDate)
                ?? new BasicStatistics(_logsTable, exercise, startDate, endDate);

            if (!ListOfBasicStatistics.Contains(stat))
                ListOfBasicStatistics.Add(stat);

            return stat;
        }
    }
}
