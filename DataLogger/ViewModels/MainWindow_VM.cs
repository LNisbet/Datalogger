using SQLight_Database;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace DataLogger.ViewModels
{
    class MainWindow_VM : NotifyPropertyChanged
    {
        public bool NavigationEnabled { get; set; } = false;

        private string userName = "User1";
        public string UserName { get => userName; set => userName = value; }

        public bool IsNewUser { get; set; } = false;

        public ObservableCollection<string> UserNames { get; set; }

        #region Initilise
        private ICommand? initiliseDb;
        public ICommand InitiliseDb
        {
            get
            {
                if (initiliseDb == null)
                {
                    initiliseDb = new RelayCommand(
                        p => InitiliseDataBase(userName,IsNewUser));
                }
                return initiliseDb;
            }
        }
        public void InitiliseDataBase(string dbName, bool isNewUser)
        {
            SQL_Database.InititiliseDatabase(dbName, isNewUser);
            NavigationEnabled = true;
            OnPropertyChanged(nameof(NavigationEnabled));
        }
        #endregion

        #region Delete Database
        private ICommand? deleteDb;
        public ICommand DeleteDb
        {
            get
            {
                if (deleteDb == null)
                {
                    deleteDb = new RelayCommand(
                        p => DeleteDataBase(userName));
                }
                return deleteDb;
            }
        }
        public void DeleteDataBase(string dbName)
        {
            SQL_Database.DeleteDatabase(dbName);
            NavigationEnabled = false;
            OnPropertyChanged(nameof(NavigationEnabled));
        }
        #endregion
    }
}
