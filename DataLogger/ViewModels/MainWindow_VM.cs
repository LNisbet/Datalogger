using SQLight_Database;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Input;
using DataLogger.ViewModels.HelperClasses;
using DataLogger.ViewModels.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace DataLogger.ViewModels
{
    class MainWindow_VM : Base_VM
    {
        private readonly IServiceProvider _serviceProvider;

        private readonly NavigationStore _navigationStore;
        public Base_VM CurrentViewModel => _navigationStore.CurrentViewModel;

        public NavigationBar_VM NavigationViewModel { get; }

        public MainWindow_VM(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _navigationStore = serviceProvider.GetRequiredService<NavigationStore>();
            NavigationViewModel = new NavigationBar_VM(serviceProvider);

            _navigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;
        }
        private void OnCurrentViewModelChanged()
        {
            // Notify that CurrentViewModel has changed
            OnPropertyChanged(nameof(CurrentViewModel));
        }

        #region Close App
        private ICommand? closeApp;
        public ICommand CloseApp
        {
            get
            {
                closeApp ??= new RelayCommand(p => CloseAndSaveApp());
                return closeApp;
            }
        }
        public void CloseAndSaveApp()
        {
        }
        #endregion
    }
}
