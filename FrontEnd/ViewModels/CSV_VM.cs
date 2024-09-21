using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using SQLight_Database;
using SQLight_Database.CSV;

namespace DataLogger.ViewModels
{
    internal class CSV_VM
    {
        const string ExerciseFileName = "\\exercises.csv";
        const string LogFileName = "\\logs.csv";

        private string path = "C:\\Users\\luken\\OneDrive\\Desktop\\TestFolder";
        public string Path { get => path; set => path = value; }

        #region ExportLogsToCSV
        private ICommand? exportLogsCommand;
        public ICommand ExportLogsCommand
        {
            get
            {
                if (exportLogsCommand == null)
                {
                    exportLogsCommand = new RelayCommand(
                        p => SQL_Database.Logs.Count > 0,
                        p => ExportLogs());
                }
                return exportLogsCommand;
            }
        }

        public void ExportLogs()
        {
            CSVHelper.WriteToCSV(Path + LogFileName, new List<ExerciseLog>(SQL_Database.Logs));
        }
        #endregion

        #region ExportExercisesToCSV
        private ICommand? exportExercisesCommand;
        public ICommand ExportExercisesCommand
        {
            get
            {
                if (exportExercisesCommand == null)
                {
                    exportExercisesCommand = new RelayCommand(
                        p => SQL_Database.Exercises.Count > 0,
                        p => ExportExercises());
                }
                return exportExercisesCommand;
            }
        }

        public void ExportExercises()
        {
            CSVHelper.WriteToCSV(Path + ExerciseFileName, new List<Exercise>(SQL_Database.Exercises));
        }
        #endregion

        #region ImportLogsFromCSV
        private ICommand? importLogsCommand;
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
            var logs = CSVHelper.ReadFromCSV<ExerciseLog>(Path + LogFileName);
            foreach (ExerciseLog l in logs)
            {
                SQL_Database.AddNewLog(l);
            }
        }
        #endregion

        #region ImportExercisesFromCSV
        private ICommand? importExercisesCommand;
        public ICommand ImportExercisesCommand
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
            var exercises = CSVHelper.ReadFromCSV<Exercise>(Path + ExerciseFileName);
            foreach (Exercise ex in exercises)
            {
                SQL_Database.AddNewExercise(ex);
            }
        }
        #endregion
    }
}
