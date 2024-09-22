using System.Collections.ObjectModel;
using System.Windows.Input;
using SQLight_Database;

namespace DataLogger.ViewModels
{
    public class Logging_VM : NotifyPropertyChanged
    {
        #region Fields
        public ObservableCollection<ExerciseLog> ExerciseLogs { get => SQL_Database.Logs; }
        public string SelectedExercise { get; set; }

        public ObservableCollection<string> ExerciseNames { get => SQL_Database.AllExerciseNames; }

        public bool SpecifyDate { get; set; }

        public DateOnly Date { get; set; }

        public float Value { get; set; }

        public ExerciseLog? SelectedExerciseLog { get; set; }
        #endregion

        public Logging_VM()
        {
            SelectedExercise = "";
            SpecifyDate = false;
            Date = DateOnly.FromDateTime(DateTime.Now);
            Value = 0;
        }

        #region AddNewLog
        private ICommand? addNewLogCommand;
        public ICommand AddNewLogCommand
        {
            get
            {
                if (addNewLogCommand == null)
                {
                    addNewLogCommand = new RelayCommand(
                        p => AddNewLog(SpecifyDate));
                }
                return addNewLogCommand;
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

            SQL_Database.AddSingleLog(log);
        }
        #endregion

        #region DeleteLog
        private ICommand? deleteLogCommand;
        public ICommand DeleteLogCommand
        {
            get
            {
                if (deleteLogCommand == null)
                {
                    deleteLogCommand = new RelayCommand(
                        p => SelectedExerciseLog != null,
                        p => DeleteLog(SelectedExerciseLog));
                }
                return deleteLogCommand;
            }
        }
        public void DeleteLog(ExerciseLog? exercise)
        {
            if (exercise != null)
                SQL_Database.RemoveSingleLog(exercise);
            else
                throw new ArgumentNullException(nameof(exercise));
                
        }
        #endregion
    }
}
