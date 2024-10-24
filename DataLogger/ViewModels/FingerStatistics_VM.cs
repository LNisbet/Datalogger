﻿using System;
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
        private Options selectedOption = Options.MostRecent;
        public Options SelectedOption { get { return selectedOption; } set { selectedOption = value; UpdatePieCharts(); } }

        public ObservableCollection<ISeries> LeftHalfCrimp_Series { get; set; }
        private int leftHalfCrimpFullValue;
        public ObservableCollection<ISeries> LeftOpenCrimp_Series { get; set; }
        public ObservableCollection<ISeries> RightHalfCrimp_Series { get; set; }
        public ObservableCollection<ISeries> RightOpenCrimp_Series { get; set; }


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
                CreatePieSeries($"{hand} Little {crimp}"),
                CreatePieSeries($"{hand} Ring {crimp}"),
                CreatePieSeries($"{hand} Middle {crimp}"),
                CreatePieSeries($"{hand} Index {crimp}")
            };
        }

        private PieSeries<float> CreatePieSeries(string exerciseName)
        {
            return new PieSeries<float> 
            { 
                Values = GetPieChartValues(exerciseName, SelectedOption),
                Name = exerciseName,
                DataLabelsPaint = new SolidColorPaint(SKColors.Black),
                DataLabelsSize = 10,
                DataLabelsPosition = LiveChartsCore.Measure.PolarLabelsPosition.Middle,
                DataLabelsFormatter = segment => $"{segment.Coordinate.PrimaryValue:N2}Kg",
                ToolTipLabelFormatter = segment => $"{segment.Context.DataSource}%",
            };
        }

        private float[] GetPieChartValues(string exerciseName, Options option)
        {
            switch (option)
            {
                case Options.MostRecent:
                    return [Statistics.MostRecent(ExerciseTable.SelectExerciseByName(exerciseName)).Value1];
                case Options.Max:
                    return [Statistics.Max(ExerciseTable.SelectExerciseByName(exerciseName),DateOnly.MinValue,DateOnly.MaxValue).Value1];
                default:
                    throw new NotImplementedException();
            }
        }

        public enum Options
        {
            [Description("Most Recent")]
            MostRecent,
            [Description("Maximum")]
            Max
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
    }

}
