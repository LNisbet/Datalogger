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
using SQLight_Database.Tables;
using System.Windows.Input;

namespace DataLogger.ViewModels
{
    internal class Charting_VM : Base_VM
    {
        #region Fields
        private readonly ITable<ExerciseLog> _logsTable;

        private ObservableCollection<SelectableObject<Exercise>> allSelectableExercises = [];
        public ObservableCollection<SelectableObject<Exercise>> AllSelectableExercises 
        {
            get => allSelectableExercises;
            set
            {
                allSelectableExercises = value;
                OnPropertyChanged(nameof(ExerciseList));
            }
        }

        public ObservableCollection<string> ExerciseTags { get; }

        private string? selectedExerciseTag;
        public string? SelectedExerciseTag 
        {  
            get => selectedExerciseTag; 
            set 
            { 
                selectedExerciseTag = value; 
                OnPropertyChanged(nameof(ExerciseList));
            } 
        }

        public ObservableCollection<SelectableObject<Exercise>> ExerciseList =>
            new(AllSelectableExercises
            .Where(e => string.IsNullOrEmpty(SelectedExerciseTag) || e.Object.Tags.Contains(SelectedExerciseTag)));

        public ObservableCollection<ISeries> Series { get; set; }
        public List<Axis> XAxes { get; set; }
        public List<Axis> YAxes { get; set; }

        private readonly ObservableCollection<Exercise.Units> yAxisLabels;
        #endregion

        public Charting_VM(ITable<string> tagsTable, ITable<Exercise> exerciseTable, ITable<ExerciseLog> logsTable)
        {
            _logsTable = logsTable;
            ExerciseTags = tagsTable.Values;
            ExerciseTags.Add("");
            yAxisLabels = [];
            Series = [];

            AllSelectableExercises = [];
            foreach (var value in exerciseTable.Values)
                AllSelectableExercises.Add(new(value, false));

            XAxes = [
                new DateTimeAxis(TimeSpan.MinValue, date => date.ToString("dd/MM/yyyy"))
                {
                    Name = "Date",
                    LabelsRotation = 15,
                    UnitWidth = TimeSpan.FromDays(1).TotalSeconds,  // Set the unit width to 1 day
                    MinStep = TimeSpan.FromDays(1).TotalSeconds,    // Minimum step size as 1 day
                }];

            //Create all Y axes
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

        #region Load Graph
        private ICommand? loadGraphCommand;
        public ICommand LoadGraphCommand
        {
            get
            {
                loadGraphCommand ??= new RelayCommand(p => UpdateSeries());
                return loadGraphCommand;
            }
        }
        #endregion

        private void UpdateSeries()
        {
            Series.Clear();
            yAxisLabels.Clear();

            foreach (var exercise in AllSelectableExercises)
            {
                // Only include selected exercises
                if (exercise.IsSelected)
                {
                    // Map the logs into X (Date) and Y (Value1)
                    var chartPoints = _logsTable.Values
                        .Where(log => log.Exercise == exercise.Object)  // Filter by exercise
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
                        Name = exercise.Object.Name,   // Label the series by exercise name
                        Fill = new SolidColorPaint(),
                        YToolTipLabelFormatter = point => $": {point.Coordinate.PrimaryValue:N2}{exercise.Object.Unit1}",
                        ScalesYAt = YAxes.FindIndex(axis => axis.Name == exercise.Object.Unit1.ToString())
                    });

                    if (!yAxisLabels.Contains(exercise.Object.Unit1))
                        yAxisLabels.Add(exercise.Object.Unit1);
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
