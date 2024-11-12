using DataLogger.ViewModels.HelperClasses;
using DataLogger.ViewModels.Interfaces;
using SQLight_Database;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DataLogger.ViewModels
{
    internal class Home_VM : Base_VM
    {
        #region Fields
        public string? SelectedUserName
        {
            get
            {
                if (DatabaseConnection.CurrentUser == null)
                    return null;
                return DatabaseConnection.CurrentUser.Name;
            }
            set
            {
                var user = String.IsNullOrEmpty(value) ? null : (UsersTable.SelectUserByName(value) ?? new(value, false));
                DatabaseConnection.CurrentUser = user;
                OnPropertyChanged(nameof(DatabaseConnection.CurrentUser));

                Properties.Settings.Default.LastLoggedInUser = value;
                Properties.Settings.Default.Save();
            }
        }

        public static ObservableCollection<string> UserNames => SQLight_Database.UsersTable.AllUserNames;
        #endregion

        public Home_VM()
        {
            if (UsersTable.AllUserNames.Contains(Properties.Settings.Default.LastLoggedInUser))
                SelectedUserName = Properties.Settings.Default.LastLoggedInUser;
            else
                SelectedUserName = null;
        }
    }
}
