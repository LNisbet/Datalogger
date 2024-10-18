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
        public ObservableCollection<string> ExerciseList => ExerciseTable.AllExerciseNames;

        public ObservableCollection<string> SelectedExercises { get; set; }

        public ObservableCollection<ISeries> Series { get; set; }
        public ObservableCollection<Axis> XAxes { get; set; }
        public ObservableCollection<Axis> YAxes { get; set; }

        private ObservableCollection<Enums.Units> yAxisLabels = []; 
        public Charting_VM()
        {
            Series = [];
            SelectedExercises = [];
            SelectedExercises.CollectionChanged += (s, e) => UpdateSeries();

            XAxes = new ObservableCollection<Axis>
            {
                new DateTimeAxis(TimeSpan.MinValue,date => DateOnly.FromDateTime(date).ToString())
                {
                    Name = "Date",
                    LabelsRotation = 15,
                }
            };

            YAxes = [];
            foreach (var unit in (Enums.Units[])Enum.GetValues(typeof(Enums.Units)))
            {
                YAxes.Add(new Axis 
                {
                    Name = unit.ToString(),
                });
            }
            UpdateSeries();
        }

        private void UpdateSeries()
        {
            Series.Clear();
            yAxisLabels.Clear();

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

                    if (!yAxisLabels.Contains(exercise.Unit1))
                        yAxisLabels.Add(exercise.Unit1);
                }
            }
            UpdateYAxis();
        }

        private void UpdateYAxis()
        {
            foreach (var axis in YAxes)
            {
                if (!Enum.TryParse(axis.Name, out Enums.Units unit) || !yAxisLabels.Contains(unit))
                    axis.IsVisible = false;
                else
                    axis.IsVisible = true;
            }
        }
    }
}
