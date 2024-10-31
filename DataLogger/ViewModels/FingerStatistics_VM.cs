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

        private FingerStatistics LeftHandStatistics = new(FingerStatistics.Hand.Left, DateOnly.MinValue, DateOnly.MaxValue);
        private FingerStatistics RightHandStatistics = new(FingerStatistics.Hand.Right, DateOnly.MinValue, DateOnly.MaxValue);

        public float LeftLittleOpenValue => LeftHandStatistics
            .SelectetBasicStatistic(FingerStatistics.Fingers.Little, FingerStatistics.Crimp.Open)
            .SelectetStatistic(SelectedOption);
        public float LeftLittleHalfValue => LeftHandStatistics
            .SelectetBasicStatistic(FingerStatistics.Fingers.Little, FingerStatistics.Crimp.Half)
            .SelectetStatistic(SelectedOption);

        public float LeftRingOpenValue => LeftHandStatistics
            .SelectetBasicStatistic(FingerStatistics.Fingers.Ring, FingerStatistics.Crimp.Open)
            .SelectetStatistic(SelectedOption);
        public float LeftRingHalfValue => LeftHandStatistics
            .SelectetBasicStatistic(FingerStatistics.Fingers.Ring, FingerStatistics.Crimp.Half)
            .SelectetStatistic(SelectedOption);

        public float LeftMiddleOpenValue => LeftHandStatistics
            .SelectetBasicStatistic(FingerStatistics.Fingers.Middle, FingerStatistics.Crimp.Open)
            .SelectetStatistic(SelectedOption);
        public float LeftMiddleHalfValue => LeftHandStatistics
            .SelectetBasicStatistic(FingerStatistics.Fingers.Middle, FingerStatistics.Crimp.Half)
            .SelectetStatistic(SelectedOption);

        public float LeftIndexOpenValue => LeftHandStatistics
            .SelectetBasicStatistic(FingerStatistics.Fingers.Index, FingerStatistics.Crimp.Open)
            .SelectetStatistic(SelectedOption);
        public float LeftIndexHalfValue => LeftHandStatistics
            .SelectetBasicStatistic(FingerStatistics.Fingers.Index, FingerStatistics.Crimp.Half)
            .SelectetStatistic(SelectedOption);


        public float RightLittleOpenValue => RightHandStatistics
            .SelectetBasicStatistic(FingerStatistics.Fingers.Little, FingerStatistics.Crimp.Open)
            .SelectetStatistic(SelectedOption);
        public float RightLittleHalfValue => RightHandStatistics
            .SelectetBasicStatistic(FingerStatistics.Fingers.Little, FingerStatistics.Crimp.Half)
            .SelectetStatistic(SelectedOption);

        public float RightRingOpenValue => RightHandStatistics
            .SelectetBasicStatistic(FingerStatistics.Fingers.Ring, FingerStatistics.Crimp.Open)
            .SelectetStatistic(SelectedOption);
        public float RightRingHalfValue => RightHandStatistics
            .SelectetBasicStatistic(FingerStatistics.Fingers.Ring, FingerStatistics.Crimp.Half)
            .SelectetStatistic(SelectedOption);

        public float RightMiddleOpenValue => RightHandStatistics
            .SelectetBasicStatistic(FingerStatistics.Fingers.Middle, FingerStatistics.Crimp.Open)
            .SelectetStatistic(SelectedOption);
        public float RightMiddleHalfValue => RightHandStatistics
            .SelectetBasicStatistic(FingerStatistics.Fingers.Middle, FingerStatistics.Crimp.Half)
            .SelectetStatistic(SelectedOption);

        public float RightIndexOpenValue => RightHandStatistics
            .SelectetBasicStatistic(FingerStatistics.Fingers.Index, FingerStatistics.Crimp.Open)
            .SelectetStatistic(SelectedOption);
        public float RightIndexHalfValue => RightHandStatistics
            .SelectetBasicStatistic(FingerStatistics.Fingers.Index, FingerStatistics.Crimp.Half)
            .SelectetStatistic(SelectedOption);
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
            FingerStatistics handStat;
            if (hand == FingerStatistics.Hand.Left)
                handStat = LeftHandStatistics;
            else
                handStat = RightHandStatistics;

            return new PieSeries<float> 
            { 
                Values = new List<float>([handStat.SelectetBasicStatistic(finger, crimp).SelectetStatistic(SelectedOption)]),
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
