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
        public Options SelectedOption { get { return selectedOption; } set { selectedOption = value; UpdatePieCharts(); } }

        public ObservableCollection<ISeries> LeftHalfCrimp_Series { get; set; }

        public ObservableCollection<ISeries> LeftOpenCrimp_Series { get; set; }

        public ObservableCollection<ISeries> RightHalfCrimp_Series { get; set; }

        public ObservableCollection<ISeries> RightOpenCrimp_Series { get; set; }
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
                Values = GetPieChartValues(exerciseName, SelectedOption),
                Name = exerciseName,
                DataLabelsPaint = new SolidColorPaint(SKColors.Black),
                DataLabelsSize = 12,
                DataLabelsPosition = LiveChartsCore.Measure.PolarLabelsPosition.Middle,
                DataLabelsFormatter = segment => $"{(segment.Coordinate.PrimaryValue / GetPieChartTotal(hand, crimp)) * 100f:N2}%",
                ToolTipLabelFormatter = segment => $"{segment.Coordinate.PrimaryValue:N2}Kg",
            };
        }

        private float[] GetPieChartValues(string exerciseName, Options option)
        {
            var stat = new BasicStatistics(ExerciseTable.SelectExerciseByName(exerciseName), DateOnly.MinValue, DateOnly.MaxValue);
            switch (option)
            {
                case Options.MostRecent:
                    return stat.MostRecent == null ? [0] : [stat.MostRecent.Value1];
                case Options.Max:
                    return stat.Max == null ? [0] : [stat.Max.Value1];
                case Options.Min:
                    return stat.Min == null ? [0] : [stat.Min.Value1];
                default:
                    throw new NotImplementedException();
            }
        }

        private float GetPieChartTotal(Hand hand, Crimp crimp)
        {
            return GetPieChartValues($"{hand} Little Finger {crimp} Crimp", SelectedOption).Sum() +
                GetPieChartValues($"{hand} Ring Finger {crimp} Crimp", SelectedOption).Sum() +
                GetPieChartValues($"{hand} Middle Finger {crimp} Crimp", SelectedOption).Sum() +
                GetPieChartValues($"{hand} Index Finger {crimp} Crimp", SelectedOption).Sum();
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

        private enum Crimp
        {
            Half,
            Open
        }
        #endregion
    }
}
