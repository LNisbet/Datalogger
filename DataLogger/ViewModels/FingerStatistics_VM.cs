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
using System.Linq.Expressions;
using System.Xml.Linq;
using SkiaSharp;
using System.ComponentModel;

namespace DataLogger.ViewModels
{
    internal class FingerStatistics_VM : Base_VM
    {
        #region Fileds
        private Options selectedOption = Options.MostRecent;

        public Options SelectedOption
        {
            get => selectedOption;
            set
            {
                selectedOption = value;
                UpdatePieCharts();
            }
        }

        public ObservableCollection<ISeries> LeftHalfCrimp_Series { get; set; }

        public ObservableCollection<ISeries> LeftOpenCrimp_Series { get; set; }

        public ObservableCollection<ISeries> RightHalfCrimp_Series { get; set; }

        public ObservableCollection<ISeries> RightOpenCrimp_Series { get; set; }

        public ObservableCollection<BasicStatistics> FingerStatistics { get; private set; }
        #endregion

        public FingerStatistics_VM()
        {
            LeftHalfCrimp_Series = [];
            LeftOpenCrimp_Series = [];
            RightHalfCrimp_Series = [];
            RightOpenCrimp_Series = [];

            FingerStatistics = [];

            UpdatePieCharts();
        }

        private void UpdatePieCharts()
        {
            LeftHalfCrimp_Series = CreatePieChartSeries(Hand.Left, Crimp.Half);
            OnPropertyChanged(nameof(LeftHalfCrimp_Series));
            LeftOpenCrimp_Series = CreatePieChartSeries(Hand.Left, Crimp.Open);
            OnPropertyChanged(nameof(LeftOpenCrimp_Series));
            RightHalfCrimp_Series = CreatePieChartSeries(Hand.Right, Crimp.Half);
            OnPropertyChanged(nameof(RightHalfCrimp_Series));
            RightOpenCrimp_Series = CreatePieChartSeries(Hand.Right, Crimp.Open);
            OnPropertyChanged(nameof(RightOpenCrimp_Series));
        }

        private ObservableCollection<ISeries> CreatePieChartSeries(Hand hand, Crimp crimp)
        {
            return new ObservableCollection<ISeries>
            {
                CreatePieSeries($"{hand} Little Finger {crimp} Crimp", hand, crimp),
                CreatePieSeries($"{hand} Ring Finger {crimp} Crimp", hand, crimp),
                CreatePieSeries($"{hand} Middle Finger {crimp} Crimp", hand, crimp),
                CreatePieSeries($"{hand} Index Finger {crimp} Crimp", hand, crimp)
            };
        }

        private PieSeries<float> CreatePieSeries(string exerciseName, Hand hand, Crimp crimp)
        {

            return new PieSeries<float> 
            { 
                Values = new List<float>([GetExerciseValues(exerciseName, SelectedOption)]),
                Name = exerciseName,
                DataLabelsPaint = new SolidColorPaint(SKColors.Black),
                DataLabelsSize = 12,
                DataLabelsPosition = LiveChartsCore.Measure.PolarLabelsPosition.Middle,
                DataLabelsFormatter = segment => $"{(segment.Coordinate.PrimaryValue / GetPieChartTotal(hand, crimp)) * 100f:N2}%",
                ToolTipLabelFormatter = segment => $"{segment.Coordinate.PrimaryValue:N2}Kg",
            };
        }

        private float GetPieChartTotal(Hand hand, Crimp crimp)
        {
            return Enum.GetValues(typeof(Fingers))
                .Cast<Fingers>()
                .Sum(finger => GetExerciseValues($"{hand} {finger} Finger {crimp} Crimp", SelectedOption));

        }

        private float GetExerciseValues(string exerciseName, Options option)
        {
            BasicStatistics stat = FingerStatistics.FirstOrDefault(st => st.Exercise.Name == exerciseName) 
                ?? new BasicStatistics(ExerciseTable.SelectExerciseByName(exerciseName), DateOnly.MinValue, DateOnly.MaxValue);

            if (!FingerStatistics.Contains(stat))
                FingerStatistics.Add(stat);

            return option switch
            {
                Options.MostRecent => stat.MostRecent?.Value1 ?? 0,
                Options.Max => stat.Max?.Value1 ?? 0,
                Options.Min => stat.Min?.Value1 ?? 0,
                _ => throw new NotImplementedException()
            };
        }

        #region Enums
        public enum Options
        {
            [Description("Most Recent")]
            MostRecent,
            [Description("Maximum")]
            Max,
            [Description("Minimum")]
            Min
        }

        private enum Hand
        {
            Left,
            Right
        }

        private enum Fingers
        {
            Little,
            Ring,
            Middle,
            Index
        }

        private enum Crimp
        {
            Half,
            Open
        }
        #endregion
    }
}
