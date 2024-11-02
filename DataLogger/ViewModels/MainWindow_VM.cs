using SQLight_Database;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Input;
using DataLogger.ViewModels.HelperClasses;

namespace DataLogger.ViewModels
{
    class MainWindow_VM : Base_VM
    {
        public bool NavigationEnabled { get{ return !String.IsNullOrEmpty(SelectedUserName); } }
        public string? SelectedUserName 
        {
            get
            {
                if(DatabaseConnection.CurrentUser == null)
                    return null;
                return DatabaseConnection.CurrentUser.Name;
            }
            set 
            {
                var user = String.IsNullOrEmpty(value) ? null : (Users.SelectUserByName(value) ?? new(value, false));
                DatabaseConnection.CurrentUser = user;

                Properties.Settings.Default.LastLoggedInUser = value;
                Properties.Settings.Default.Save();
                OnPropertyChanged(nameof(NavigationEnabled)); 
            }  
        }

        public ObservableCollection<string> UserNames => SQLight_Database.Users.AllUserNames;

        public MainWindow_VM() 
        {
            if (Users.AllUserNames.Contains(Properties.Settings.Default.LastLoggedInUser))
                SelectedUserName = Properties.Settings.Default.LastLoggedInUser;
            else
                SelectedUserName = null;
        }

        #region Close App
        private ICommand? closeApp;
        public ICommand CloseApp
        {
            get
            {
                closeApp ??= new RelayCommand(
                        p => CloseAndSaveApp());
                return closeApp;
            }
        }
        public void CloseAndSaveApp()
        {
        }
        #endregion
    }
}
