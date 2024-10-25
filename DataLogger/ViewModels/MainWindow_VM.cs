﻿using SQLight_Database;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Input;

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
                DatabaseConnection.CurrentUser = Users.SelectUserByName(value);
                Properties.Settings.Default.LastLoggedInUser = value;
                Properties.Settings.Default.Save();
                OnPropertyChanged(nameof(NavigationEnabled)); 
            }  
        }

        public ObservableCollection<string> UserNames { get => SQLight_Database.Users.AllUserNames; }

        public MainWindow_VM() 
        {
            SelectedUserName = Properties.Settings.Default.LastLoggedInUser;
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
        }
        #endregion
    }
}
