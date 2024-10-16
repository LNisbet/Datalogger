using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using System.Collections.ObjectModel;
using SQLight_Database;
using LiveChartsCore.Defaults;

namespace DataLogger.ViewModels
{
    internal class Charting_VM
    {
        public ObservableCollection<ISeries> Series { get; set; }
        public ObservableCollection<Axis> XAxes { get; set; }
        public ObservableCollection<Axis> YAxes { get; set; }
        public Charting_VM()
        {
            
            XAxes = new ObservableCollection<Axis>
            {
                new Axis
                {
                    Name = "Date",
                    Labeler = (value) => value.ToString(),
                    LabelsRotation = 15,
                }
            };
            YAxes = new ObservableCollection<Axis>
            {
                new Axis
                {
                    Name = "Value1",
                    // Now the Y axis we will display labels as currency
                    // LiveCharts provides some common formatters
                    // in this case we are using the currency formatter.
                    //Labeler = Labelers.Currency 

                    // you could also build your own currency formatter
                    // for example:
                    // Labeler = (value) => value.ToString("C")

                    // But the one that LiveCharts provides creates shorter labels when
                    // the amount is in millions or trillions
                }
            };

            Series = [];
            foreach (var exercise in ExerciseTable.Exercises)
            {
                // Map the logs into X (Date) and Y (Value1)
                var chartPoints = LogsTable.Logs
                    .Where(log => log.Exercise == exercise)  // Filter by exercise
                    .Select(log => new DateTimePoint
                    {
                        DateTime = log.Date.ToDateTime(TimeOnly.MinValue), // X-axis (date)
                        Value = (double)log.Value1 // Y-axis (value1)
                    })
                    .ToList();

                // Create a new line series for the exercise
                Series.Add(new LineSeries<DateTimePoint>
                {
                    Values = chartPoints,  // Bind the chart points to the series
                    Name = exercise.Name,   // Label the series by exercise name
                    XToolTipLabelFormatter = point => $"{exercise.Name}: {point.PrimaryValue:N2} on {point.SecondaryValue:MM/dd/yyyy}"
                });
            }
        }
    }
}
