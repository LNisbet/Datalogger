using System.Windows.Input;
using SQLight_Database;
using CSV_Exporter;
using Newtonsoft.Json.Linq;
using System.Windows.Controls;
using DataLogger.ViewModels.HelperClasses;
using SQLight_Database.Tables.Interfaces;

namespace DataLogger.ViewModels
{
    public class CSV_VM : Base_VM
    {
        private readonly IExerciseTable _exerciseTable;
        private readonly ILogsTable _logsTable;
        public CSV_VM(IExerciseTable exerciseTable, ILogsTable logsTable)
        {
            _exerciseTable = exerciseTable;
            _logsTable = logsTable;
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
                exportLogsCommand ??= new RelayCommand(p => ExportLogs(), p => _logsTable.Logs.Count > 0);
                return exportLogsCommand;
            }
        }

        public void ExportLogs()
        {
            var path = SelectSaveFilePath();
            if (path == null)
                return;
            CSVHelper.WriteToCSV(path, new List<ExerciseLog>(_logsTable.Logs));
        }
        #endregion

        #region ExportExercisesToCSV
        private ICommand? exportExercisesCommand;
        public ICommand ExportExercisesCommand
        {
            get
            {
                exportExercisesCommand ??= new RelayCommand(p => ExportExercises(), p => _exerciseTable.Exercises.Count > 0);
                return exportExercisesCommand;
            }
        }

        public void ExportExercises()
        {
            var path = SelectSaveFilePath();
            if (path == null)
                return;
            CSVHelper.WriteToCSV(path, new List<Exercise>(_exerciseTable.Exercises));
        }
        #endregion

        #region ImportLogsFromCSV
        private ICommand? importLogsCommand;
        public ICommand ImportLogsCommand
        {
            get
            {
                importLogsCommand ??= new RelayCommand(p => ImportLogs());
                return importLogsCommand;
            }
        }

        public void ImportLogs()
        {
            var path = SelectOpenFilePath();
            if (path == null)
                return;
            _logsTable.AddMultipleLogs(CSVHelper.ReadFromCSV<ExerciseLog>(path));
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

        public void ImportExercises()
        {
            var path = SelectOpenFilePath();
            if (path == null)
                return;
            _exerciseTable.AddMultipleExercises(CSVHelper.ReadFromCSV<Exercise>(path));
        }
        #endregion
    }
}
