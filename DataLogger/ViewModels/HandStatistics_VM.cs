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
        private FingerStatistics.Hand hand;
        public FingerStatistics.Hand Hand 
        {
            get => hand;
            set
            {
                hand = value;
                UpdateValues();
            }
        }

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

        private readonly FingerStatisticsStore _fingerStatisticsStore;
        private FingerStatistics? fingerStatistics;

        public float? LittleOpen => fingerStatistics?
            .SelectetBasicStatistic(FingerStatistics.Fingers.Little, FingerStatistics.Crimp.Open)
            .SelectetStatistic(Option);
        public float? LittleHalf => fingerStatistics?
            .SelectetBasicStatistic(FingerStatistics.Fingers.Little, FingerStatistics.Crimp.Half)
            .SelectetStatistic(Option);

        public float? RingOpen => fingerStatistics?
            .SelectetBasicStatistic(FingerStatistics.Fingers.Ring, FingerStatistics.Crimp.Open)
            .SelectetStatistic(Option);
        public float? RingHalf => fingerStatistics?
            .SelectetBasicStatistic(FingerStatistics.Fingers.Ring, FingerStatistics.Crimp.Half)
            .SelectetStatistic(Option);

        public float? MiddleOpen => fingerStatistics?
            .SelectetBasicStatistic(FingerStatistics.Fingers.Middle, FingerStatistics.Crimp.Open)
            .SelectetStatistic(Option);
        public float? MiddleHalf => fingerStatistics?
            .SelectetBasicStatistic(FingerStatistics.Fingers.Middle, FingerStatistics.Crimp.Half)
            .SelectetStatistic(Option);

        public float? IndexOpen => fingerStatistics?
            .SelectetBasicStatistic(FingerStatistics.Fingers.Index, FingerStatistics.Crimp.Open)
            .SelectetStatistic(Option);
        public float? IndexHalf => fingerStatistics?
            .SelectetBasicStatistic(FingerStatistics.Fingers.Index, FingerStatistics.Crimp.Half)
            .SelectetStatistic(Option);
        #endregion

        public HandStatistics_VM(FingerStatisticsStore fingerStatisticsStore)
        {
            _fingerStatisticsStore = fingerStatisticsStore;
            Option = BasicStatistics.Options.MostRecent;
        }

        private void UpdateValues()
        {
            fingerStatistics = _fingerStatisticsStore.Get(Hand, DateOnly.MinValue, DateOnly.MaxValue);
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
