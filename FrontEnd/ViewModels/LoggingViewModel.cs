using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using DataLogger.Models;

namespace DataLogger.ViewModels
{
    public class LoggingViewModel : INotifyPropertyChanged
    {
        public IDatabase Logger = new Logger();

        public string NewExercise { get; set; }

        public string SelectedExercise { get; set; }

        public ObservableCollection<string> ExerciseNames { get { return new ObservableCollection<string>(Logger.Exercises.Select(exercise => exercise.Name)); } }


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
            Logger.AddNewExercise(new Exercise(NewExercise));
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
            var selectedExercise = Logger.Exercises.SingleOrDefault(ex => ex.Name == SelectedExercise);

            if (dateSpecified)
                log = new ExerciseLog(Date, selectedExercise, Value);
            else
                log = new ExerciseLog(selectedExercise, Value);

            Logger.AddNewLog(log);
        }
        #endregion

    }
}
