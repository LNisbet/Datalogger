using SQLight_Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLogger
{
    class BasicStatistics
    {
        public ExerciseLog? MostRecent { get; }
        public ExerciseLog? Max { get; }
        public ExerciseLog? Min { get; }

        public BasicStatistics(Exercise exercise, DateOnly startDate, DateOnly endDate) 
        {
            MostRecent = Statistics.MostRecent(exercise);
            Max = Statistics.Max(exercise, startDate, endDate);
            Min = Statistics.Min(exercise, startDate, endDate);
        }
    }
}
