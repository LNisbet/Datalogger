using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using SQLight_Database;

namespace DataLogger.ViewModels
{
    public class LoggingViewModel : NotifyPropertyChanged
    {
        #region Fields
        public ObservableCollection<ExerciseLog> ExerciseLogs { get => SQL_Database.Logs; }
        public string SelectedExercise { get; set; }

        public ObservableCollection<string> ExerciseNames { get => SQL_Database.AllExerciseNames; }

        public bool SpecifyDate { get; set; }

        public DateOnly Date { get; set; }

        public float Value { get; set; }
        #endregion

        public LoggingViewModel()
        {
            SelectedExercise = "";
            SpecifyDate = false;
            Date = DateOnly.FromDateTime(DateTime.Now);
            Value = 0;

            //SQL_Database.InternalDatabase.PropertyChanged += (sender, args) => OnPropertyChanged(args.PropertyName);
        }

        #region AddNewLog
        private ICommand? addNewLog;
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
            var _selectedExercise = SQL_Database.SelectExerciseByName(SelectedExercise);

            if (dateSpecified)
                log = new ExerciseLog(Date, _selectedExercise, Value);
            else
                log = new ExerciseLog(DateOnly.FromDateTime(DateTime.Now), _selectedExercise, Value);

            SQL_Database.AddNewLog(log);
        }
        #endregion
    }
}
