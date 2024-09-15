using DataLogger.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace DataLogger.ViewModels
{
    internal class CSV_VM
    {
        const string ExerciseFileName = "\\exercises.csv";
        const string LogFileName = "\\logs.csv";

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
            IDatabase Logger = new Logger();
            CSVHelper.WriteToCSV(Path + LogFileName, new List<ExerciseLog>(Logger.ExerciseLogs));
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
                    IDatabase Logger = new Logger();
                    exportExercisesCommand = new RelayCommand(
                        p => Logger.Exercises.Count > 0,
                        p => ExportExercises());
                }
                return exportExercisesCommand;
            }
        }

        public void ExportExercises()
        {
            ICSV CSVHelper = new CSVHelper();
            IDatabase Logger = new Logger();
            CSVHelper.WriteToCSV(Path + ExerciseFileName, new List<Exercise>(Logger.Exercises));
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
            ICSV CSVHelper = new CSVHelper();
            IDatabase Logger = new Logger();
            var logs = CSVHelper.ReadFromCSV<ExerciseLog>(Path + LogFileName);
            foreach (ExerciseLog l in logs)
            {
                Logger.AddNewLog(l);
            }
        }
        #endregion

        #region ImportExercisesFromCSV
        private ICommand importExercisesCommand;
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
            ICSV CSVHelper = new CSVHelper();
            IDatabase Logger = new Logger();
            var exercises = CSVHelper.ReadFromCSV<Exercise>(Path + ExerciseFileName);
            foreach (Exercise ex in exercises)
            {
                Logger.AddNewExercise(ex);
            }
        }
        #endregion
    }
}
