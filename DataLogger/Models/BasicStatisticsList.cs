using DataLogger.Views;
using SQLight_Database;
using SQLight_Database.Tables.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DataLogger.ViewModels.FingerStatistics_VM;

namespace DataLogger.Models
{
    public class BasicStatisticsList
    {
        private readonly IExerciseTable _exerciseTable;
        private readonly ILogsTable _logsTable;

        private ObservableCollection<BasicStatistics> ListOfBasicStatistics { get; set; } = [];

        public BasicStatisticsList (ILogsTable logsTable, IExerciseTable exerciseTable)
        {
            _exerciseTable = exerciseTable;
            _logsTable = logsTable;
        }

        internal BasicStatistics GetStatisticsForExercise(string exerciseName, DateOnly startDate, DateOnly endDate)
        {
            BasicStatistics stat = ListOfBasicStatistics
                .FirstOrDefault(st => st.Exercise.Name == exerciseName && st.StartDate == startDate && st.EndDate == endDate)
                ?? new BasicStatistics(_logsTable, _exerciseTable.SelectExerciseByName(exerciseName), startDate, endDate);

            if (!ListOfBasicStatistics.Contains(stat))
                ListOfBasicStatistics.Add(stat);

            return stat;
        }

        internal BasicStatistics GetStatisticsForExercise(Exercise exercise, DateOnly startDate, DateOnly endDate)
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
