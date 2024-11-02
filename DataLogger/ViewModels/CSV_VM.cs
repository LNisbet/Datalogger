using System.Windows.Input;
using SQLight_Database;
using CSV_Exporter;
using Newtonsoft.Json.Linq;
using System.Windows.Controls;
using DataLogger.ViewModels.HelperClasses;

namespace DataLogger.ViewModels
{
    public class CSV_VM : Base_VM
    {
        public CSV_VM()
        {
        }

        #region Select File Path
        private static string? SelectOpenFilePath()
        {
            // Create OpenFileDialog 
            Microsoft.Win32.OpenFileDialog dlg = new()
            {
                // Set filter for file extension and default file extension 
                DefaultExt = ".csv",
                Filter = "CSV Files (*.csv)|*.csv",
                DefaultDirectory = Properties.Settings.Default.LastExportFilePath
            };

            // Display OpenFileDialog by calling ShowDialog method 
            Nullable<bool> result = dlg.ShowDialog();

            if (result == true)
            {
                string filename = dlg.FileName;
                Properties.Settings.Default.LastExportFilePath = filename;
                Properties.Settings.Default.Save();
                return filename;
            }
            return null;
        }

        private static string? SelectSaveFilePath()
        {
            Microsoft.Win32.SaveFileDialog dlg = new()
            {
                // Set filter for file extension and default file extension 
                DefaultExt = ".csv",
                Filter = "CSV Files (*.csv)|*.csv",
                DefaultDirectory = Properties.Settings.Default.LastExportFilePath
            };

            // Display OpenFileDialog by calling ShowDialog method 
            Nullable<bool> result = dlg.ShowDialog();

            if (result == true)
            {
                string filename = dlg.FileName;
                Properties.Settings.Default.LastExportFilePath = filename;
                Properties.Settings.Default.Save();
                return filename;
            }
            return null;
        }
        #endregion

        #region ExportLogsToCSV
        private ICommand? exportLogsCommand;
        public ICommand ExportLogsCommand
        {
            get
            {
                exportLogsCommand ??= new RelayCommand(
                        p => LogsTable.Logs.Count > 0,
                        p => ExportLogs());
                return exportLogsCommand;
            }
        }

        public static void ExportLogs()
        {
            var path = SelectSaveFilePath();
            if (path == null)
                return;
            CSVHelper.WriteToCSV(path, new List<ExerciseLog>(LogsTable.Logs));
        }
        #endregion

        #region ExportExercisesToCSV
        private ICommand? exportExercisesCommand;
        public ICommand ExportExercisesCommand
        {
            get
            {
                exportExercisesCommand ??= new RelayCommand(
                        p => ExerciseTable.Exercises.Count > 0,
                        p => ExportExercises());
                return exportExercisesCommand;
            }
        }

        public static void ExportExercises()
        {
            var path = SelectSaveFilePath();
            if (path == null)
                return;
            CSVHelper.WriteToCSV(path, new List<Exercise>(ExerciseTable.Exercises));
        }
        #endregion

        #region ImportLogsFromCSV
        private ICommand? importLogsCommand;
        public ICommand ImportLogsCommand
        {
            get
            {
                importLogsCommand ??= new RelayCommand(
                        p => ImportLogs());
                return importLogsCommand;
            }
        }

        public static void ImportLogs()
        {
            var path = SelectOpenFilePath();
            if (path == null)
                return;
            LogsTable.AddMultipleLogs(CSVHelper.ReadFromCSV<ExerciseLog>(path));
        }
        #endregion

        #region ImportExercisesFromCSV
        private ICommand? importExercisesCommand;
        public ICommand ImportExercisesCommand
        {
            get
            {
                importExercisesCommand ??= new RelayCommand(
                        p => ImportExercises());
                return importExercisesCommand;
            }
        }

        public static void ImportExercises()
        {
            var path = SelectOpenFilePath();
            if (path == null)
                return;
            ExerciseTable.AddMultipleExercises(CSVHelper.ReadFromCSV<Exercise>(path));
        }
        #endregion
    }
}
