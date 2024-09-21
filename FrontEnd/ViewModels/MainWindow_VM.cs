using SQLight_Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Windows.Input;

namespace DataLogger.ViewModels
{
    class MainWindow_VM
    {
        private string userName = "User1";
        public string UserName { get => userName; set => userName = value; }

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
        }
        #endregion
    }
}
