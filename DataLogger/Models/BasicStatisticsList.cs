using DataLogger.Views;
using SQLight_Database;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DataLogger.ViewModels.FingerStatistics_VM;

namespace DataLogger.Models
{
    static internal class BasicStatisticsList
    {
        private static ObservableCollection<BasicStatistics> ListOfBasicStatistics { get; set; } = [];

        internal static BasicStatistics GetStatisticsForExercise(string exerciseName, DateOnly startDate, DateOnly endDate)
        {
            BasicStatistics stat = ListOfBasicStatistics
                .FirstOrDefault(st => st.Exercise.Name == exerciseName && st.StartDate == startDate && st.EndDate == endDate)
                ?? new BasicStatistics(ExerciseTable.SelectExerciseByName(exerciseName), startDate, endDate);

            if (!ListOfBasicStatistics.Contains(stat))
                ListOfBasicStatistics.Add(stat);

            return stat;
        }

        internal static BasicStatistics GetStatisticsForExercise(Exercise exercise, DateOnly startDate, DateOnly endDate)
        {
            BasicStatistics stat = ListOfBasicStatistics
                .FirstOrDefault(st => st.Exercise == exercise && st.StartDate == startDate && st.EndDate == endDate)
                ?? new BasicStatistics(exercise, startDate, endDate);

            if (!ListOfBasicStatistics.Contains(stat))
                ListOfBasicStatistics.Add(stat);

            return stat;
        }
    }
}
