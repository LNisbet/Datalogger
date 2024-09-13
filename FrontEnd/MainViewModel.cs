using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Windows;
using System.Windows.Input;
using System.ComponentModel;

namespace DataLogger
{
    public class MainViewModel
    {
        public ILogger MainModel = new MainModel();

        public string Exercise { get; set; }

        public List<string> Exercises { get { return MainModel.Exercises; } }

        public bool SpecifyDate { get; set; }

        public DateOnly Date { get; set; }

        public float Value { get; set; }


        public MainViewModel() {
            Exercise = "test";
            MainModel.AddNewExercise("Curls");
            MainModel.AddNewExercise("Deadhang");
            MainModel.AddNewExercise("Pushups");
            SpecifyDate = false;
            Date = DateOnly.FromDateTime(DateTime.Now);
            Value = 0;
        }


        private ICommand? addNewExercise;
        public ICommand AddNewExerciseCommand
        {
            get
            {
                if (addNewExercise == null)
                {
                    addNewExercise = new RelayCommand(
                        p => true,
                        p => MainModel.AddNewExercise(Exercise));
                }
                return addNewExercise;
            }
        }

        private ICommand? addNewLog;
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

    public class RelayCommand : ICommand
    {
        private readonly Predicate<object> _canExecute;
        private readonly Action<object> _execute;

        public RelayCommand(Predicate<object> canExecute, Action<object> execute)
        {
            _canExecute = canExecute;
            _execute = execute;
        }

        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute(parameter);
        }

        public void Execute(object parameter)
        {
            _execute(parameter);
        }
    }
}
