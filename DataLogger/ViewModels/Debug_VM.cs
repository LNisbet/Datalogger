using SQLight_Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DataLogger.ViewModels
{
    internal class Debug_VM
    {
        public bool IsNewUser { get; set; } = true;
        public string CurrentUserName => DatabaseConnection.CurrentUser.Name;

        public Debug_VM() { }

        #region LogIn
        private ICommand? logIn;
        public ICommand LogIn
        {
            get
            {
                if (logIn == null)
                {
                    logIn = new RelayCommand(
                        P => (!String.IsNullOrEmpty(CurrentUserName)),
                        p => LogUserIn(new User(CurrentUserName, !IsNewUser)));
                }
                return logIn;
            }
        }
        public void LogUserIn(User user)
        {
            Users.Add(user);
        }
        #endregion

        #region Delete User
        private ICommand? deleteUser;
        public ICommand DeleteUser
        {
            get
            {
                if (deleteUser == null)
                {
                    deleteUser = new RelayCommand(
                        p => (!String.IsNullOrEmpty(CurrentUserName)),
                        p => DeleteDataBase(new User(CurrentUserName, !IsNewUser)));
                }
                return deleteUser;
            }
        }
        public void DeleteDataBase(User dbName)
        {
            Users.Remove(dbName);
        }
        #endregion
    }
}
