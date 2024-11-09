using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using System.Collections.ObjectModel;
using LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp;
using DataLogger.Models;
using DataLogger.ViewModels.HelperClasses;
using Accessibility;
using OpenTK.Graphics.OpenGL;

namespace DataLogger.ViewModels
{
    internal class HandStatistics_VM : Base_VM
    {
        #region Fileds
        public FingerStatistics.Hand Hand { get; }

        private BasicStatistics.Options options;

        public BasicStatistics.Options Option 
        { 
            get => options; 
            set
            {
                options = value;
                UpdateValues();
            } 
        }

        internal readonly FingerStatistics HandStatistics;

        public float? LittleOpen => HandStatistics
            .SelectetBasicStatistic(FingerStatistics.Fingers.Little, FingerStatistics.Crimp.Open)
            .SelectetStatistic(Option);
        public float? LittleHalf => HandStatistics
            .SelectetBasicStatistic(FingerStatistics.Fingers.Little, FingerStatistics.Crimp.Half)
            .SelectetStatistic(Option);

        public float? RingOpen => HandStatistics
            .SelectetBasicStatistic(FingerStatistics.Fingers.Ring, FingerStatistics.Crimp.Open)
            .SelectetStatistic(Option);
        public float? RingHalf => HandStatistics
            .SelectetBasicStatistic(FingerStatistics.Fingers.Ring, FingerStatistics.Crimp.Half)
            .SelectetStatistic(Option);

        public float? MiddleOpen => HandStatistics
            .SelectetBasicStatistic(FingerStatistics.Fingers.Middle, FingerStatistics.Crimp.Open)
            .SelectetStatistic(Option);
        public float? MiddleHalf => HandStatistics
            .SelectetBasicStatistic(FingerStatistics.Fingers.Middle, FingerStatistics.Crimp.Half)
            .SelectetStatistic(Option);

        public float? IndexOpen => HandStatistics
            .SelectetBasicStatistic(FingerStatistics.Fingers.Index, FingerStatistics.Crimp.Open)
            .SelectetStatistic(Option);
        public float? IndexHalf => HandStatistics
            .SelectetBasicStatistic(FingerStatistics.Fingers.Index, FingerStatistics.Crimp.Half)
            .SelectetStatistic(Option);
        #endregion

        public HandStatistics_VM(FingerStatistics.Hand hand, BasicStatistics.Options option = BasicStatistics.Options.MostRecent)
        {
            Hand = hand;
            Option = option;
            HandStatistics = new(hand, DateOnly.MinValue, DateOnly.MaxValue);
        }

        private void UpdateValues()
        {
            OnPropertyChanged(nameof(LittleOpen));
            OnPropertyChanged(nameof(LittleHalf));
            OnPropertyChanged(nameof(RingOpen));
            OnPropertyChanged(nameof(RingHalf));
            OnPropertyChanged(nameof(MiddleOpen));
            OnPropertyChanged(nameof(MiddleHalf));
            OnPropertyChanged(nameof(IndexOpen));
            OnPropertyChanged(nameof(IndexHalf));
        }
    }
}
