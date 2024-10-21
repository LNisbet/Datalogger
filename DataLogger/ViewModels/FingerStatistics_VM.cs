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
            LeftHalfCrimp_Series = CreatePieSeries(Hand.Left, Crimp.Half);
            LeftOpenCrimp_Series = CreatePieSeries(Hand.Left, Crimp.Open);
            RightHalfCrimp_Series = CreatePieSeries(Hand.Right, Crimp.Half);
            RightOpenCrimp_Series = CreatePieSeries(Hand.Right, Crimp.Open);
        }

        private ObservableCollection<ISeries> CreatePieSeries(Hand hand, Crimp crimp)
        {
            return new ObservableCollection<ISeries>
            {
                new PieSeries<float> {Values = new float[] { SelectValueFromMostRecentLog($"Max {hand} Little {crimp}") } },
                new PieSeries<float> {Values = new float[] { SelectValueFromMostRecentLog($"Max {hand} Ring {crimp}") } },
                new PieSeries<float> {Values = new float[] { SelectValueFromMostRecentLog($"Max {hand} Middle {crimp}") } },
                new PieSeries<float> {Values = new float[] { SelectValueFromMostRecentLog($"Max {hand} Index {crimp}") } }
            };
        }

        private float SelectValueFromMostRecentLog(string exerciseName)
        {
            return Statistics.MostRecent(ExerciseTable.SelectExerciseByName(exerciseName)).Value1;
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
