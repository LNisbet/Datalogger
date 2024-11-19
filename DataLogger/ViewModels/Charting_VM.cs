using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using System.Collections.ObjectModel;
using LiveChartsCore.Defaults;
using System.Windows.Input.Manipulations;
using LiveChartsCore.Drawing;
using LiveChartsCore.SkiaSharpView.Painting;
using LiveChartsCore.Kernel;
using DataLogger.ViewModels.HelperClasses;
using SQLight_Database.Tables.Interfaces;
using SQLight_Database.Models;
using DataLogger.Models;

namespace DataLogger.ViewModels
{
    internal class Charting_VM : Base_VM
    {
        #region Fields
        private readonly ITable<string> _tagsTable;
        private readonly ITable<Exercise> _exerciseTable;
        private readonly ITable<ExerciseLog> _logsTable;

        public ObservableCollection<SelectableObject<Exercise>> AllSelectableExercises { get; set; }

        public ObservableCollection<string>? ExerciseTags { get; }
        private string? selectedExerciseTag = null;
        public string? SelectedExerciseTag 
        {  
            get => selectedExerciseTag; 
            set 
            { 
                selectedExerciseTag = value; 
                OnPropertyChanged(nameof(ExerciseList)); 
            } 
        }


        public List<string> ExerciseList =>
            AllSelectableExercises
            .Where(e => SelectedExerciseTag == null || e.Object.Tags.Contains(SelectedExerciseTag))
            .Select(e => e.Object.Name)
            .ToList();

        

        public List<string> SelectedExercises => AllSelectableExercises.Where(e => e.IsSelected == true).Select(e => e.Object.Name).ToList();

        public ObservableCollection<ISeries> Series { get; set; }
        public List<Axis> XAxes { get; set; }
        public List<Axis> YAxes { get; set; }

        private readonly ObservableCollection<Exercise.Units> yAxisLabels = [];
        #endregion

        public Charting_VM(ITable<string> tagsTable, ITable<Exercise> exerciseTable, ITable<ExerciseLog> logsTable)
        {
            _tagsTable = tagsTable;
            _exerciseTable = exerciseTable;
            _logsTable = logsTable;
            ExerciseTags = tagsTable.Values;
            Series = [];

            AllSelectableExercises = [];
            foreach (var value in exerciseTable.Values)
                AllSelectableExercises.Add(new(value, false));

            AllSelectableExercises.CollectionChanged += (s, e) => UpdateSeries();

            XAxes = [
                new DateTimeAxis(TimeSpan.MinValue, date => date.ToString("dd/MM/yyyy"))
                {
                    Name = "Date",
                    LabelsRotation = 15,
                    UnitWidth = TimeSpan.FromDays(1).TotalSeconds,  // Set the unit width to 1 day
                    MinStep = TimeSpan.FromDays(1).TotalSeconds,    // Minimum step size as 1 day
                }];

            YAxes = [];
            foreach (var unit in (Exercise.Units[])Enum.GetValues(typeof(Exercise.Units)))
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

            foreach (var exercise in _exerciseTable.Values)
            {
                // Only include selected exercises
                if (SelectedExercises.Contains(exercise.Name))
                {
                    // Map the logs into X (Date) and Y (Value1)
                    var chartPoints = _logsTable.Values
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
                if (!Enum.TryParse(axis.Name, out Exercise.Units unit) || !yAxisLabels.Contains(unit))
                    axis.IsVisible = false;
                else
                    axis.IsVisible = true;
            }
        }
    }
}
