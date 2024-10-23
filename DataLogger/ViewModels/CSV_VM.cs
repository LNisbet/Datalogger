using System.Windows.Input;
using SQLight_Database;
using CSV_Exporter;
using Newtonsoft.Json.Linq;

namespace DataLogger.ViewModels
{
    public class CSV_VM : Base_VM
    {
        const string ExerciseFileName = "\\exercises.csv";
        const string LogFileName = "\\logs.csv";
        public string Path { get; set; }

        public CSV_VM()
        {
            Path = Properties.Settings.Default.LastExportFilePath;
        }

        #region ExportLogsToCSV
        private ICommand? exportLogsCommand;
        public ICommand ExportLogsCommand
        {
            get
            {
                if (exportLogsCommand == null)
                {
                    exportLogsCommand = new RelayCommand(
                        p => LogsTable.Logs.Count > 0,
                        p => ExportLogs());
                }
                return exportLogsCommand;
            }
        }

        public void ExportLogs()
        {
            CSVHelper.WriteToCSV(Path + LogFileName, new List<ExerciseLog>(LogsTable.Logs));
            Properties.Settings.Default.LastExportFilePath = System.IO.Path.GetDirectoryName(Path);
            Properties.Settings.Default.Save();
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
                        p => ExerciseTable.Exercises.Count > 0,
                        p => ExportExercises());
                }
                return exportExercisesCommand;
            }
        }

        public void ExportExercises()
        {
            CSVHelper.WriteToCSV(Path + ExerciseFileName, new List<Exercise>(ExerciseTable.Exercises));
            Properties.Settings.Default.LastExportFilePath = System.IO.Path.GetDirectoryName(Path);
            Properties.Settings.Default.Save();
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
            LogsTable.AddMultipleLogs(CSVHelper.ReadFromCSV<ExerciseLog>(Path + LogFileName));
            Properties.Settings.Default.LastExportFilePath = System.IO.Path.GetDirectoryName(Path);
            Properties.Settings.Default.Save();
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
            ExerciseTable.AddMultipleExercises(CSVHelper.ReadFromCSV<Exercise>(Path + ExerciseFileName));
            Properties.Settings.Default.LastExportFilePath = System.IO.Path.GetDirectoryName(Path);
            Properties.Settings.Default.Save();
        }
        #endregion
    }
}
