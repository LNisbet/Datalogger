using SQLight_Database;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Input;

namespace DataLogger.ViewModels
{
    class MainWindow_VM : NotifyPropertyChanged
    {
        public bool NavigationEnabled { get{ return !String.IsNullOrEmpty(SelectedUserName); } }

        private static string? selectedUserName;
        public string? SelectedUserName { get => selectedUserName; set { selectedUserName = value; OnPropertyChanged(nameof(NavigationEnabled)); }  }

        public ObservableCollection<string> UserNames { get => SQLight_Database.Users.AllUserNames; }

        public MainWindow_VM() 
        {
            selectedUserName = Properties.Settings.Default.LastLoggedInUser;
        }

        #region Close App
        private ICommand? closeApp;
        public ICommand CloseApp
        {
            get
            {
                if (closeApp == null)
                {
                    closeApp = new RelayCommand(
                        p => CloseAndSaveApp());
                }
                return closeApp;
            }
        }
        public void CloseAndSaveApp()
        {
            Properties.Settings.Default.LastLoggedInUser = selectedUserName;
            Properties.Settings.Default.Save();
        }
        #endregion
    }
}
