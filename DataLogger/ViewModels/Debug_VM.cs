using SQLight_Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using DataLogger.ViewModels.HelperClasses;

namespace DataLogger.ViewModels
{
    internal class Debug_VM : Base_VM
    {
        public bool IsNewUser { get; set; } = true;
        public static string? CurrentUserName => DatabaseConnection.CurrentUser?.Name;

        public Debug_VM() { }


        #region Delete User
        private ICommand? deleteUser;
        public ICommand DeleteUser
        {
            get
            {
                if (deleteUser == null)
                {
                    deleteUser = new RelayCommand(p => DeleteDataBase(new User(CurrentUserName, !IsNewUser)), p => (!String.IsNullOrEmpty(CurrentUserName)));
                }
                return deleteUser;
            }
        }
        public void DeleteDataBase(User dbName)
        {
            UsersTable.Remove(dbName);
            OnPropertyChanged(nameof(CurrentUserName));
        }
        #endregion
    }
}
