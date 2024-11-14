﻿using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using SQLight_Database;
using DataLogger.ViewModels.HelperClasses;
using SQLight_Database.Tables.Interfaces;

namespace DataLogger.ViewModels
{
    public class Logging_VM : Base_VM
    {
        #region Fields
        private readonly IExerciseTable _exerciseTable;
        private readonly ILogsTable _logsTable;

        public ObservableCollection<ExerciseLog> ExerciseLogs  => _logsTable.Logs;

        private Exercise? selectedExercise;
        public Exercise? SelectedExercise 
        { 
            get => selectedExercise; 
            set 
            { 
                selectedExercise = value; 
                OnPropertyChanged(nameof(SelectedExercise)); 
                UpdateValues(); 
            } 
        }

        public ObservableCollection<Exercise> Exercises  => _exerciseTable.Exercises;

        public bool SpecifyDate { get; set; }

        public DateOnly Date { get; set; }

        public float Value1 { get; set; }
        public float? Value2 { get; set; }
        public float? Value3 { get; set; }
        public float? Value4 { get; set; }
        public string? Note { get; set; }

        private ExerciseLog? selectedExerciseLog;
        public ExerciseLog? SelectedExerciseLog 
        { 
            get => selectedExerciseLog; 
            set 
            { 
                selectedExerciseLog = value; 
                UpdateValues(); 
            } 
        }
        #endregion

        public Logging_VM(IExerciseTable exerciseTable, ILogsTable logsTable)
        {
            _exerciseTable = exerciseTable;
            _logsTable = logsTable;
            SpecifyDate = false;
            Date = DateOnly.FromDateTime(DateTime.Now);
            UpdateValues();
        }

        #region AddNewLog
        private ICommand? addNewLogCommand;
        public ICommand AddNewLogCommand
        {
            get
            {
                addNewLogCommand ??= new RelayCommand(p => AddNewLog(SpecifyDate), p => SelectedExercise != null);
                return addNewLogCommand;
            }
        }

        public void AddNewLog(bool dateSpecified)
        {
            if (SelectedExercise == null) 
                return;

            if (!dateSpecified)
                Date = DateOnly.FromDateTime(DateTime.Now);

            ExerciseLog log = new(Date, SelectedExercise, Value1, Value2, Value3, Value4, Note);
            _logsTable.AddSingleLog(log);
        }
        #endregion

        #region DeleteLog
        private ICommand? deleteLogCommand;
        public ICommand DeleteLogCommand
        {
            get
            {
                deleteLogCommand ??= new RelayCommand(p => DeleteLog(SelectedExerciseLog), p => SelectedExerciseLog != null);
                return deleteLogCommand;
            }
        }
        public void DeleteLog(ExerciseLog? exercise)
        {
            ArgumentNullException.ThrowIfNull(exercise);

            _logsTable.RemoveSingleLog(exercise);
        }
        #endregion

        private void UpdateValues()
        {
            Value1 = 0;
            OnPropertyChanged(nameof(Value1));
            Value2 = (SelectedExercise == null || SelectedExercise.Unit2 == null) ? null : 0;
            OnPropertyChanged(nameof(Value2));
            Value3 = (SelectedExercise == null || SelectedExercise.Unit3 == null) ? null : 0;
            OnPropertyChanged(nameof(Value3));
            Value4 = (SelectedExercise == null || SelectedExercise.Unit4 == null) ? null : 0;
            OnPropertyChanged(nameof(Value4));
        }
    }
}
