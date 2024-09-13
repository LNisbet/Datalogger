using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using DataLogger.Models;

namespace DataLogger.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public ILogger MainModel = new MainModel();

        public string Exercise { get; set; }

        public ObservableCollection<string> Exercises { get { return MainModel.Exercises; } }

        public bool SpecifyDate { get; set; }

        public DateOnly Date { get; set; }

        public float Value { get; set; }


        public MainViewModel()
        {
            Exercise = "test";
            MainModel.AddNewExercise("Curls");
            MainModel.AddNewExercise("Deadhang");
            MainModel.AddNewExercise("Pushups");
            SpecifyDate = false;
            Date = DateOnly.FromDateTime(DateTime.Now);
            Value = 0;

            MainModel.PropertyChanged += (sender, args) => OnPropertyChanged(args.PropertyName);
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

        private ICommand addNewExercise;
        public ICommand AddNewExerciseCommand
        {
            get
            {
                MessageBox.Show("AddNewExerciseCommand");
                if (addNewExercise == null)
                {
                    addNewExercise = new RelayCommand(
                        p => true,
                        p => AddNewExercise());
                }
                return addNewExercise;
            }
        }

        public void AddNewExercise()
        {
            MainModel.AddNewExercise(Exercise);
        }


        private ICommand addNewLog;
        public ICommand AddNewLogCommand
        {
            get
            {
                if (addNewLog == null)
                {
                    addNewLog = new RelayCommand(
                        p => true,
                        p => AddNewLog());
                }
                return addNewLog;
            }
        }
        public void AddNewLog()
        {
            ExerciseLog log;
            if (SpecifyDate)
                log = new ExerciseLog(Date, Exercise, Value);
            else
                log = new ExerciseLog(Exercise, Value);

            MainModel.AddNewLog(log);
        }
    }
}
