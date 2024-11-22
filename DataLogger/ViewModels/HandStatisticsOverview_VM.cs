using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using System.Collections.ObjectModel;
using LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp;
using DataLogger.Models;
using DataLogger.ViewModels.HelperClasses;

namespace DataLogger.ViewModels
{
    public class HandStatisticsOverview_VM : Base_VM
    {
        #region Fileds
        private readonly FingerStatisticsStore _fingerStatisticsStore;

        private BasicStatistics.Options selectedOption = BasicStatistics.Options.MostRecent;
        public BasicStatistics.Options SelectedOption
        {
            get => selectedOption;
            set
            {
                selectedOption = value;
                UpdateHandStatistics();
                UpdatePieCharts();                
            }
        }

        public ObservableCollection<ISeries> LeftHalfCrimp_Series { get; set; }
        public ObservableCollection<ISeries> LeftOpenCrimp_Series { get; set; }
        public ObservableCollection<ISeries> RightHalfCrimp_Series { get; set; }
        public ObservableCollection<ISeries> RightOpenCrimp_Series { get; set; }

        public HandStatistics_VM LeftHand { get; }
        public HandStatistics_VM RightHand { get; }
        #endregion

        public HandStatisticsOverview_VM(FingerStatisticsStore fingerStatisticsStore, HandStatistics_VM leftHand, HandStatistics_VM rightHand)
        {
            _fingerStatisticsStore = fingerStatisticsStore;
            LeftHalfCrimp_Series = [];
            LeftOpenCrimp_Series = [];
            RightHalfCrimp_Series = [];
            RightOpenCrimp_Series = [];

            leftHand.Hand = FingerStatistics.Hand.Left;
            rightHand.Hand = FingerStatistics.Hand.Right;
            LeftHand = leftHand;
            RightHand = rightHand;

            UpdatePieCharts();
            UpdateHandStatistics();
        }

        private void UpdateHandStatistics()
        {
            LeftHand.Option = SelectedOption;
            RightHand.Option = SelectedOption;
        }

        private void UpdatePieCharts()
        {
            LeftHalfCrimp_Series = CreatePieChartSeries(FingerStatistics.Hand.Left, FingerStatistics.Crimp.Half);
            OnPropertyChanged(nameof(LeftHalfCrimp_Series));
            LeftOpenCrimp_Series = CreatePieChartSeries(FingerStatistics.Hand.Left, FingerStatistics.Crimp.Open);
            OnPropertyChanged(nameof(LeftOpenCrimp_Series));
            RightHalfCrimp_Series = CreatePieChartSeries(FingerStatistics.Hand.Right, FingerStatistics.Crimp.Half);
            OnPropertyChanged(nameof(RightHalfCrimp_Series));
            RightOpenCrimp_Series = CreatePieChartSeries(FingerStatistics.Hand.Right, FingerStatistics.Crimp.Open);
            OnPropertyChanged(nameof(RightOpenCrimp_Series));
        }

        private ObservableCollection<ISeries> CreatePieChartSeries(FingerStatistics.Hand hand, FingerStatistics.Crimp crimp)
        {
            return
            [
                CreatePieSeries(hand, crimp, FingerStatistics.Fingers.Little),
                CreatePieSeries(hand, crimp, FingerStatistics.Fingers.Ring),
                CreatePieSeries(hand, crimp, FingerStatistics.Fingers.Middle),
                CreatePieSeries(hand, crimp, FingerStatistics.Fingers.Index)
            ];
        }

        private PieSeries<float> CreatePieSeries(FingerStatistics.Hand hand, FingerStatistics.Crimp crimp, FingerStatistics.Fingers finger)
        {
            FingerStatistics handStat = _fingerStatisticsStore.Get(hand, DateOnly.MinValue, DateOnly.MaxValue);
            return new PieSeries<float> 
            { 
                Values = new List<float>([handStat.SelectetBasicStatistic(finger, crimp).SelectetStatistic(SelectedOption) ?? 0f]),
                Name = handStat.SelectetBasicStatistic(finger, crimp).Exercise.Name,
                DataLabelsPaint = new SolidColorPaint(SKColors.Black),
                DataLabelsSize = 12,
                DataLabelsPosition = LiveChartsCore.Measure.PolarLabelsPosition.Middle,
                DataLabelsFormatter = segment => $"{(segment.Coordinate.PrimaryValue / handStat.GetCrimpTotal(crimp, SelectedOption)) * 100f:N2}%",
                ToolTipLabelFormatter = segment => $"{segment.Coordinate.PrimaryValue:N2}Kg",
            };
        }
    }
}
