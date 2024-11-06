using DataLogger.ViewModels.HelperClasses;
using DataLogger.ViewModels.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Linq;
using SQLight_Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DataLogger.ViewModels
{
    internal class NavigationBar_VM : Base_VM
    {
        private readonly INavigationService _navigationService;

        public bool CanNavigate => !string.IsNullOrEmpty(DatabaseConnection.CurrentUser?.Name);


        public NavigationBar_VM(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        #region Home
        private ICommand? navigateToHomeCommand;
        public ICommand NavigateToHomeCommand
        {
            get
            {
                navigateToHomeCommand ??= new RelayCommand(p => _navigationService.NavigateTo<Logging_VM>(), p => CanNavigate);
                return navigateToHomeCommand;
            }
        }
        #endregion

        #region CSV
        private ICommand? navigateToCSVCommand;
        public ICommand NavigateToCSVCommand
        {
            get
            {
                navigateToCSVCommand ??= new RelayCommand(p => _navigationService.NavigateTo<CSV_VM>(), p => CanNavigate);
                return navigateToCSVCommand;
            }
        }
        #endregion

        #region Logging
        private ICommand? navigateToLoggingCommand;
        public ICommand NavigateToLoggingCommand
        {
            get
            {
                navigateToLoggingCommand ??= new RelayCommand(p => _navigationService.NavigateTo<Logging_VM>(), p => CanNavigate);
                return navigateToLoggingCommand;
            }
        }
        #endregion

        #region Create Exercise
        private ICommand? navigateToCreateExerciseCommand;
        public ICommand NavigateToCreateExerciseCommand
        {
            get
            {
                navigateToCreateExerciseCommand ??= new RelayCommand(p => _navigationService.NavigateTo<CreateExercise_VM>(), p => CanNavigate);
                return navigateToCreateExerciseCommand;
            }
        }
        #endregion

        #region Basic Statistics
        private ICommand? navigateToBasicStatisticsCommand;
        public ICommand NavigateToBasicStatisticsCommand
        {
            get
            {
                navigateToBasicStatisticsCommand ??= new RelayCommand(p => _navigationService.NavigateTo<BasicStatistics_VM>(), p => CanNavigate);
                return navigateToBasicStatisticsCommand;
            }
        }
        #endregion

        #region Finger Statistics
        private ICommand? navigateToFingerStatisticsCommand;
        public ICommand NavigateToFingerStatisticsCommand
        {
            get
            {
                navigateToFingerStatisticsCommand ??= new RelayCommand(p => _navigationService.NavigateTo<FingerStatistics_VM>(), p => CanNavigate);
                return navigateToFingerStatisticsCommand;
            }
        }
        #endregion

        #region Charting
        private ICommand? navigateToChartingCommand;
        public ICommand NavigateToChartingCommand
        {
            get
            {
                navigateToChartingCommand ??= new RelayCommand(p => _navigationService.NavigateTo<Charting_VM>(), p => CanNavigate);
                return navigateToChartingCommand;
            }
        }
        #endregion

        #region Debug
        private ICommand? navigateToDebugCommand;
        public ICommand NavigateToDebugCommand
        {
            get
            {
                navigateToDebugCommand ??= new RelayCommand(p => _navigationService.NavigateTo<Debug_VM>(), p => CanNavigate);
                return navigateToDebugCommand;
            }
        }
        #endregion
    }
}
