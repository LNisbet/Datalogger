using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using DataLogger.Models;

namespace DataLogger.ViewModels
{
    public class LoggingViewModel : INotifyPropertyChanged
    {
        public ILogger Logger = new Logger();

        public string NewExercise { get; set; }

        public string SelectedExercise { get; set; }

        public ObservableCollection<string> Exercises { get { return Logger.Exercises; } }

        public bool SpecifyDate { get; set; }

        public DateOnly Date { get; set; }

        public float Value { get; set; }


        public LoggingViewModel()
        {
            NewExercise = "";
            SpecifyDate = false;
            Date = DateOnly.FromDateTime(DateTime.Now);
            Value = 0;

            Logger.PropertyChanged += (sender, args) => OnPropertyChanged(args.PropertyName);
        }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion

        #region AddNewExercise
        private ICommand addNewExercise;
        public ICommand AddNewExerciseCommand
        {
            get
            {
                if (addNewExercise == null)
                {
                    addNewExercise = new RelayCommand(
                        p => AddNewExercise());
                }
                return addNewExercise;
            }
        }

        public void AddNewExercise()
        {
            Logger.AddNewExercise(NewExercise);
        }
        #endregion

        #region AddNewLog
        private ICommand addNewLog;
        public ICommand AddNewLogCommand
        {
            get
            {
                if (addNewLog == null)
                {
                    addNewLog = new RelayCommand(
                        p => AddNewLog(SpecifyDate));
                }
                return addNewLog;
            }
        }
        public void AddNewLog(bool dateSpecified)
        {
            ExerciseLog log;
            if (dateSpecified)
                log = new ExerciseLog(Date, SelectedExercise, Value);
            else
                log = new ExerciseLog(SelectedExercise, Value);

            Logger.AddNewLog(log);
        }
        #endregion

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

            string Path = "C:\\Users\\luken\\OneDrive\\Desktop\\TestFolder";

            CSVHelper.WrirteToCSV(Path + "\\logs.csv", Database.ExerciseLogs);
        }
        #endregion
    }
}
