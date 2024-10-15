using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using System.Collections.ObjectModel;
using SQLight_Database;

namespace DataLogger.ViewModels
{
    internal class Charting_VM
    {
        public ObservableCollection<ISeries> Series { get; set; }
        public Charting_VM()
        {
            // Map data to series
            var chartPoints = LogsTable.Logs
                .Select(log => new { X = log.Date.ToDateTime(TimeOnly.MinValue).ToOADate(), Y = (double)log.Value1 })
                .ToList();

            Series = new ObservableCollection<ISeries>
            {
                new LineSeries<double>
                {
                    Values = chartPoints.Select(p => p.Y).ToList(), // Y-values (Value1)
                    // Mapping is not necessary since we are using basic data types
                }
            };
        }
    }
}
