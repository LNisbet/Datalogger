using SQLight_Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLogger
{
    public class BasicStatistics
    {
        public Exercise Exercise { get; }
        public DateOnly StartDate { get; }
        public DateOnly EndDate { get; }
        public ExerciseLog? MostRecent { get; }
        public ExerciseLog? Max { get; }
        public ExerciseLog? Min { get; }

        public BasicStatistics(Exercise exercise, DateOnly startDate, DateOnly endDate) 
        {
            Exercise = exercise;
            StartDate = startDate;
            EndDate = endDate;
            MostRecent = Statistics.MostRecent(exercise);
            Max = Statistics.Max(exercise, startDate, endDate);
            Min = Statistics.Min(exercise, startDate, endDate);
        }
    }
}
