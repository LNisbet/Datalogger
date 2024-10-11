using SQLight_Database;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace DataLogger.ViewModels
{
    class MainWindow_VM : NotifyPropertyChanged
    {
        public bool NavigationEnabled { get{ return !String.IsNullOrEmpty(SelectedUserName); } }

        private string? selectedUserName;
        public string? SelectedUserName { get => selectedUserName; set { selectedUserName = value; OnPropertyChanged(nameof(NavigationEnabled)); }  }

        public bool IsNewUser { get; set; } = true;

        public ObservableCollection<string> UserNames { get => SQLight_Database.Users.AllUserNames; }

        #region LogIn
        private ICommand? logIn;
        public ICommand LogIn
        {
            get
            {
                if (logIn == null)
                {
                    logIn = new RelayCommand(
                        P => (!String.IsNullOrEmpty(SelectedUserName)),
                        p => LogUserIn(new User(SelectedUserName, !IsNewUser)));
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
                        p => (!String.IsNullOrEmpty(SelectedUserName)),
                        p => DeleteDataBase(new User(SelectedUserName, !IsNewUser)));
                }
                return deleteUser;
            }
        }
        public void DeleteDataBase(User dbName)
        {
            Users.Remove(dbName);
            SelectedUserName = null;
        }
        #endregion
    }
}
