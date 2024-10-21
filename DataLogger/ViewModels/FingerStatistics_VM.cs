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

namespace DataLogger.ViewModels
{
    internal class FingerStatistics_VM : Base_VM
    {
        public ObservableCollection<ISeries> LeftHalfCrimp_Series { get; set; }
        public ObservableCollection<ISeries> LeftOpenCrimp_Series { get; set; }
        public ObservableCollection<ISeries> RightHalfCrimp_Series { get; set; }
        public ObservableCollection<ISeries> RightOpenCrimp_Series { get; set; }


        public FingerStatistics_VM()
        {
            LeftHalfCrimp_Series = CreatePieChartSeries(Hand.Left, Crimp.Half);
            LeftOpenCrimp_Series = CreatePieChartSeries(Hand.Left, Crimp.Open);
            RightHalfCrimp_Series = CreatePieChartSeries(Hand.Right, Crimp.Half);
            RightOpenCrimp_Series = CreatePieChartSeries(Hand.Right, Crimp.Open);
        }

        private ObservableCollection<ISeries> CreatePieChartSeries(Hand hand, Crimp crimp)
        {
            return new ObservableCollection<ISeries>
            {
                CreatePieSeries($"Max {hand} Little {crimp}"),
                CreatePieSeries($"Max {hand} Ring {crimp}"),
                CreatePieSeries($"Max {hand} Middle {crimp}"),
                CreatePieSeries($"Max {hand} Index {crimp}")
            };
        }

        private PieSeries<float> CreatePieSeries(string exerciseName)
        {
            return new PieSeries<float> 
            { 
                Values = new float[] { Statistics.MostRecent(ExerciseTable.SelectExerciseByName(exerciseName)).Value1 },
                Name = exerciseName,
                DataLabelsPaint = new SolidColorPaint(SKColors.Black),
                DataLabelsSize = 10,
                DataLabelsPosition = LiveChartsCore.Measure.PolarLabelsPosition.Middle,
                DataLabelsFormatter = segment => $"{segment.Coordinate.PrimaryValue:N2}Kg",
                ToolTipLabelFormatter = segment => $"{segment.Context.DataSource}%",
            };
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
