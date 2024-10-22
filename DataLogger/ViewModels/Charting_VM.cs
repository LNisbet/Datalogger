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
using LiveChartsCore.Kernel;
using Newtonsoft.Json.Linq;

namespace DataLogger.ViewModels
{
    internal class Charting_VM : Base_VM
    {
        public ObservableCollection<string> ExerciseList => ExerciseTable.AllExerciseNames;

        public ObservableCollection<string> SelectedExercises { get; set; }

        public ObservableCollection<ISeries> Series { get; set; }
        public List<Axis> XAxes { get; set; }
        public List<Axis> YAxes { get; set; }

        private ObservableCollection<Enums.Units> yAxisLabels = []; 
        public Charting_VM()
        {
            Series = [];
            SelectedExercises = [];
            SelectedExercises.CollectionChanged += (s, e) => UpdateSeries();

            XAxes = [
                new DateTimeAxis(TimeSpan.MinValue, date => date.ToString("dd/MM/yyyy"))
                {
                    Name = "Date",
                    LabelsRotation = 15,
                    UnitWidth = TimeSpan.FromDays(1).TotalSeconds,  // Set the unit width to 1 day
                    MinStep = TimeSpan.FromDays(1).TotalSeconds,    // Minimum step size as 1 day
                }];

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
                            Value = (double)log.Value1 // Y-axis (value1)
                        })
                        .ToList();

                    // Create a new line series for the exercise
                    Series.Add(new LineSeries<DateTimePoint>
                    {
                        Values = chartPoints,  // Bind the chart points to the series
                        Name = exercise.Name,   // Label the series by exercise name
                        Fill = new SolidColorPaint(),
                        YToolTipLabelFormatter = point => $": {point.Coordinate.PrimaryValue:N2}{exercise.Unit1}",
                        ScalesYAt = YAxes.FindIndex(axis => axis.Name == exercise.Unit1.ToString())
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
