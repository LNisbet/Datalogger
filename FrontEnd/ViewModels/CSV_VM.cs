using DataLogger.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DataLogger.ViewModels
{
    internal class CSV_VM
    {
        private string path = "C:\\Users\\luken\\OneDrive\\Desktop\\TestFolder";
        public string Path { get => path; set => path = value; }

        #region ExportLogsToCSV
        private ICommand exportLogsCommand;
        public ICommand ExportLogsCommand
        {
            get
            {
                if (exportLogsCommand == null)
                {
                    exportLogsCommand = new RelayCommand(
                        p => Database.ExerciseLogs.Count > 0,
                        p => ExportLogs());
                }
                return exportLogsCommand;
            }
        }

        public void ExportLogs()
        {
            ICSV CSVHelper = new CSVHelper();

            CSVHelper.WrirteToCSV(Path + "\\logs.csv", Database.ExerciseLogs);
        }
        #endregion

        #region ExportExercisesToCSV
        private ICommand exportExercisesCommand;
        public ICommand ExportExercisesCommand
        {
            get
            {
                if (exportExercisesCommand == null)
                {
                    exportExercisesCommand = new RelayCommand(
                        p => Database.Exercises.Count > 0,
                        p => ExportExercises());
                }
                return exportExercisesCommand;
            }
        }

        public void ExportExercises()
        {
            ICSV CSVHelper = new CSVHelper();
            CSVHelper.WrirteToCSV(Path + "\\exercises.csv", Database.Exercises.ToList());
        }
        #endregion

        #region ImportLogsFromCSV
        private ICommand importLogsCommand;
        public ICommand ImportLogsCommand
        {
            get
            {
                if (importLogsCommand == null)
                {
                    importLogsCommand = new RelayCommand(
                        p => ImportLogs());
                }
                return importLogsCommand;
            }
        }

        public void ImportLogs()
        {
            throw new NotImplementedException();
        }
        #endregion

        #region ImportExercisesFromCSV
        private ICommand importExercisesCommand;
        public ICommand IMportExercisesCommand
        {
            get
            {
                if (importExercisesCommand == null)
                {
                    importExercisesCommand = new RelayCommand(
                        p => ImportExercises());
                }
                return importExercisesCommand;
            }
        }

        public void ImportExercises()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
