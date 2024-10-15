using System.Windows.Input;
using SQLight_Database;
using CSV_Exporter;

namespace DataLogger.ViewModels
{
    internal class CSV_VM : Base_VM
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
                        p => LogsTable.Logs.Count > 0,
                        p => ExportLogs());
                }
                return exportLogsCommand;
            }
        }

        public void ExportLogs()
        {
            CSVHelper.WriteToCSV(Path + LogFileName, new List<ExerciseLog>(LogsTable.Logs));
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
        }
        #endregion
    }
}
