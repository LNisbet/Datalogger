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
                        p => InitiliseDataBase(userName));
                }
                return initiliseDb;
            }
        }
        public void InitiliseDataBase(string dbName)
        {
            SQL_Database.InititiliseDatabase(dbName);
            NavigationEnabled = true;
            OnPropertyChanged(nameof(NavigationEnabled));
        }
        #endregion
    }
}
