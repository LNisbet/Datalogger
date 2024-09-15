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
            CSVHelper.WriteToCSV(Path + LogFileName, Database.ExerciseLogs);
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
            CSVHelper.WriteToCSV(Path + ExerciseFileName, new List<Exercise>(Database.Exercises));
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
            var logs = CSVHelper.ReadFromCSV<ExerciseLog>(Path + LogFileName);
            string allExercises = "";
            foreach (ExerciseLog l in logs)
            {
                allExercises += l.Date + ", " + l.Exercise.Name + ", " + l.Value + "\n";
            }
            MessageBox.Show(allExercises);
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
            var exercises = CSVHelper.ReadFromCSV<Exercise>(Path + ExerciseFileName);
            string allExercises = "";
            foreach (Exercise s in exercises)
            {
                allExercises += s.Name + "\n";
            }
            MessageBox.Show(allExercises);
        }
        #endregion
    }
}
