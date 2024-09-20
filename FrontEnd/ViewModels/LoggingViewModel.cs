﻿using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using SQLight_Database;

namespace DataLogger.ViewModels
{
    public class LoggingViewModel : NotifyPropertyChanged
    {
        #region Fields
        public ObservableCollection<ExerciseLog> ExerciseLogs { get => Model.InternalDatabase.ExerciseLogs; }
        public string SelectedExercise { get; set; }

        public ObservableCollection<string> ExerciseNames { get => Model.InternalDatabase.AllExerciseNames; }

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

            Model.InternalDatabase.PropertyChanged += (sender, args) => OnPropertyChanged(args.PropertyName);
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
            var _selectedExercise = Model.InternalDatabase.SelectExerciseByName(SelectedExercise);

            if (dateSpecified)
                log = new ExerciseLog(Date, _selectedExercise, Value);
            else
                log = new ExerciseLog(_selectedExercise, Value);

            Model.InternalDatabase.AddNewLog(log);
        }
        #endregion
    }
}
