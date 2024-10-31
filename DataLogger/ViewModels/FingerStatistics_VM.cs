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
using DataLogger.Models;

namespace DataLogger.ViewModels
{
    internal class FingerStatistics_VM : Base_VM
    {
        #region Fileds
        private BasicStatistics.Options selectedOption = BasicStatistics.Options.MostRecent;

        public BasicStatistics.Options SelectedOption
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

        public float LeftLittleOpenValue => GetExerciseValues("Left Little Finger Open Crimp", SelectedOption);
        public float LeftLittleHalfValue => GetExerciseValues("Left Little Finger Half Crimp", SelectedOption);
        public float LeftRingOpenValue => GetExerciseValues("Left Ring Finger Open Crimp", SelectedOption);
        public float LeftRingHalfValue => GetExerciseValues("Left Ring Finger Half Crimp", SelectedOption);
        public float LeftMiddleOpenValue => GetExerciseValues("Left Middle Finger Open Crimp", SelectedOption);
        public float LeftMiddleHalfValue => GetExerciseValues("Left Middle Finger Half Crimp", SelectedOption);
        public float LeftIndexOpenValue => GetExerciseValues("Left Index Finger Open Crimp", SelectedOption);
        public float LeftIndexHalfValue => GetExerciseValues("Left Index Finger Half Crimp", SelectedOption);

        public float RightLittleOpenValue => GetExerciseValues("Right Little Finger Open Crimp", SelectedOption);
        public float RightLittleHalfValue => GetExerciseValues("Right Little Finger Half Crimp", SelectedOption);
        public float RightRingOpenValue => GetExerciseValues("Right Ring Finger Open Crimp", SelectedOption);
        public float RightRingHalfValue => GetExerciseValues("Right Ring Finger Half Crimp", SelectedOption);
        public float RightMiddleOpenValue => GetExerciseValues("Right Middle Finger Open Crimp", SelectedOption);
        public float RightMiddleHalfValue => GetExerciseValues("Right Middle Finger Half Crimp", SelectedOption);
        public float RightIndexOpenValue => GetExerciseValues("Right Index Finger Open Crimp", SelectedOption);
        public float RightIndexHalfValue => GetExerciseValues("Right Index Finger Half Crimp", SelectedOption);
        #endregion

        public FingerStatistics_VM()
        {
            LeftHalfCrimp_Series = [];
            LeftOpenCrimp_Series = [];
            RightHalfCrimp_Series = [];
            RightOpenCrimp_Series = [];

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

        private float GetExerciseValues(string exerciseName, BasicStatistics.Options option)
        {
            var stat = BasicStatisticsList.GetStatisticsForExercise(exerciseName, DateOnly.MinValue, DateOnly.MaxValue);

            return option switch
            {
                BasicStatistics.Options.MostRecent => stat.MostRecent?.Value1 ?? 0,
                BasicStatistics.Options.Max => stat.Max?.Value1 ?? 0,
                BasicStatistics.Options.Min => stat.Min?.Value1 ?? 0,
                _ => throw new NotImplementedException()
            };
        }

        #region Enums
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
