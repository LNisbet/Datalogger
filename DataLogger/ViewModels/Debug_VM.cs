using SQLight_Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using DataLogger.ViewModels.HelperClasses;
using SQLight_Database.Tables.Interfaces;
using SQLight_Database.Database.Interfaces;

namespace DataLogger.ViewModels
{
    internal class Debug_VM : Base_VM
    {
        private readonly IUsersTable _usersTable;
        private readonly IDatabaseConnectionService _databaseConnection;

        public bool IsNewUser { get; set; } = true;
        //public static string? CurrentUserName => _databaseConnection.CurrentUser?.Name;

        public Debug_VM(IUsersTable usersTable, IDatabaseConnectionService databaseConnection) 
        {
            _usersTable = usersTable;
            _databaseConnection = databaseConnection;
        }

        /*
        #region Delete User
        private ICommand? deleteUser;
        public ICommand DeleteUser
        {
            get
            {
                if (deleteUser == null)
                {
                    deleteUser = new RelayCommand(p => DeleteDataBase(), p => (!String.IsNullOrEmpty(CurrentUserName)));
                }
                return deleteUser;
            }
        }
        public void DeleteDataBase()
        {
            UsersTable.Remove(_databaseConnection.CurrentUser);
            OnPropertyChanged(nameof(CurrentUserName));
        }
        #endregion*/
    }
}
