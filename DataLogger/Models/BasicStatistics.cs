using SQLight_Database;
using SQLight_Database.Tables.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLogger.Models
{
    public class BasicStatistics
    {
        public Exercise Exercise { get; }
        public DateOnly StartDate { get; }
        public DateOnly EndDate { get; }
        public ExerciseLog? MostRecent { get; }
        public ExerciseLog? Max { get; }
        public ExerciseLog? Min { get; }

        public BasicStatistics(ILogsTable logsTable, Exercise exercise, DateOnly startDate, DateOnly endDate) 
        {
            Exercise = exercise;
            StartDate = startDate;
            EndDate = endDate;
            var filteredLogs = logsTable.Logs.Where(log => log.Exercise == exercise && log.Date >= startDate && log.Date <= endDate);
            if (filteredLogs.Any())
            {
                MostRecent = filteredLogs.Aggregate((log1, log2) => log1.Date > log2.Date ? log1 : log2);
                Max = filteredLogs.Aggregate((log1, log2) => log1.Value1 > log2.Value1 ? log1 : log2);
                Min = filteredLogs.Aggregate((log1, log2) => log1.Value1 < log2.Value1 ? log1 : log2);
            }
            else
            {
                MostRecent = null;
                Max = null;
                Min = null;
            }
        }

        internal float? SelectetStatistic(Options option)
        {
            return option switch
            {
                Options.MostRecent => MostRecent?.Value1,
                Options.Max => Max?.Value1,
                Options.Min => Min?.Value1,
                _ => throw new NotImplementedException(),
            };
        }

        public enum Options
        {
            [Description("Most Recent")]
            MostRecent,
            [Description("Maximum")]
            Max,
            [Description("Minimum")]
            Min
        }
    }
}
