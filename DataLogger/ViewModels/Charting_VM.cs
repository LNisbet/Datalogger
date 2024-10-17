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
using System.Windows.Input.Manipulations;
using LiveChartsCore.Drawing;
using LiveChartsCore.SkiaSharpView.Painting;

namespace DataLogger.ViewModels
{
    internal class Charting_VM : Base_VM
    {
        public ObservableCollection<string> SelectedExercises { get; set; }
        public ObservableCollection<ISeries> Series { get; set; }
        public ObservableCollection<Axis> XAxes { get; set; }
        public ObservableCollection<Axis> YAxes { get; set; }
        public Charting_VM()
        {
            
            XAxes = new ObservableCollection<Axis>
            {
                new DateTimeAxis(TimeSpan.MinValue,date => DateOnly.FromDateTime(date).ToString())
                {
                    Name = "Date",
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
                    .OrderByDescending(log => log.Date)
                    .Select(log => new DateTimePoint
                    {
                        DateTime = log.Date.ToDateTime(TimeOnly.MinValue, DateTimeKind.Utc), // X-axis (date)
                        Value = Math.Round((double)log.Value1,2) // Y-axis (value1)
                    })
                    .ToList();

                // Create a new line series for the exercise
                Series.Add(new LineSeries<DateTimePoint>
                {
                    Values = chartPoints,  // Bind the chart points to the series
                    Name = exercise.Name,   // Label the series by exercise name
                    Fill = new SolidColorPaint(),
                    XToolTipLabelFormatter = point => $"{exercise.Name}: {point.Coordinate.PrimaryValue} on {point.Coordinate.SecondaryValue}"
                });
            }
        }
        public void UpdateSeries()
        {
            Series.Clear(); // Clear existing series

            foreach (var exercise in ExerciseTable.Exercises)
            {
                // Only include selected exercises
                if (SelectedExercises.Contains(exercise.Name))
                {
                    // Map the logs into X (Date) and Y (Value1)
                    var chartPoints = LogsTable.Logs
                        .Where(log => log.Exercise == exercise)  // Filter by exercise
                        .OrderByDescending(log => log.Date)
                        .Select(log => new DateTimePoint
                        {
                            DateTime = log.Date.ToDateTime(TimeOnly.MinValue, DateTimeKind.Utc), // X-axis (date)
                            Value = Math.Round((double)log.Value1, 2) // Y-axis (value1)
                        })
                        .ToList();

                    // Create a new line series for the exercise
                    Series.Add(new LineSeries<DateTimePoint>
                    {
                        Values = chartPoints,  // Bind the chart points to the series
                        Name = exercise.Name,   // Label the series by exercise name
                        Fill = new SolidColorPaint(),
                        XToolTipLabelFormatter = point => $"{exercise.Name}: {point.Coordinate.PrimaryValue} on {point.Coordinate.SecondaryValue}"
                    });
                }
            }
        }
}
