using System.Windows.Input;
using SQLight_Database;
using CSV_Exporter;
using Newtonsoft.Json.Linq;
using System.Windows.Controls;

namespace DataLogger.ViewModels
{
    public class CSV_VM : Base_VM
    {
        public CSV_VM()
        {
        }

        private string? SelectOpenFilePath()
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

        private string? SelectSaveFilePath()
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
            var path = SelectOpenFilePath();
            if (path == null)
                return;
            ExerciseTable.AddMultipleExercises(CSVHelper.ReadFromCSV<Exercise>(path));
        }
        #endregion
    }
}
