using DataLogger.ViewModels.HelperClasses;
using DataLogger.ViewModels.Interfaces;
using SQLight_Database;
using SQLight_Database.Database.Interfaces;
using SQLight_Database.Tables.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DataLogger.ViewModels
{
    public class Home_VM : Base_VM
    {
        #region Fields
        private readonly IUsersTable _usersTable;
        private readonly IDatabaseConnectionService _databaseConnection;
        private readonly DatabaseConnectionStore _databaseConnectionStore;

        public string? SelectedUserName
        {
            get
            {
                if (_databaseConnectionStore.CurrentUser == null)
                    return null;
                return _databaseConnectionStore.CurrentUser.Name;
            }
            set
            {
                var user = String.IsNullOrEmpty(value) ? null : (_usersTable.SelectUserByName(value) ?? new(value, false));
                _databaseConnection.Open(user);
                OnPropertyChanged(nameof(_databaseConnectionStore.CurrentUser));

                Properties.Settings.Default.LastLoggedInUser = value;
                Properties.Settings.Default.Save();
            }
        }

        public ObservableCollection<string> UserNames => _usersTable.AllUserNames;
        #endregion

        public Home_VM(IUsersTable usersTable, IDatabaseConnectionService databaseConnection, DatabaseConnectionStore databaseConnectionStore)
        {
            _usersTable = usersTable;
            _databaseConnection = databaseConnection;
            _databaseConnectionStore = databaseConnectionStore;

            if (usersTable.AllUserNames.Contains(Properties.Settings.Default.LastLoggedInUser))
                SelectedUserName = Properties.Settings.Default.LastLoggedInUser;
            else
                SelectedUserName = null;
        }
    }
}
