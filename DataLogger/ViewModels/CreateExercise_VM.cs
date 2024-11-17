using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows;
using DataLogger.ViewModels.HelperClasses;
using SQLight_Database.Tables.Interfaces;
using SQLight_Database.Models;

namespace DataLogger.ViewModels
{
    public class CreateExercise_VM : Base_VM
    {
        #region Fields
        private readonly ITable<Exercise> _exerciseTable;
        private readonly ITable<string> _tagsTable;

        public string NewExerciseName { get; set; }

        public ObservableCollection<string> NewExerciseTags { get; set; }
        public Exercise.Units NewUnit1 { get; set; }
        public Exercise.Units NewUnit2 { get; set; }
        public Exercise.Units NewUnit3 { get; set; }
        public Exercise.Units NewUnit4 { get; set; }

        public string? NewExerciseDescription { get; set; }

        private Exercise NewExercise { get => new(NewExerciseName, NewExerciseTags.ToList(), NewUnit1, NewUnit2, NewUnit3, NewUnit4, NewExerciseDescription); }

        public ObservableCollection<string> AllExerciseTags => _tagsTable.Values;

        public ObservableCollection<Exercise> Exercises => _exerciseTable.Values;

        public Exercise? SelectedExercise { get; set; }
        #endregion

        public CreateExercise_VM(ITable<Exercise> exerciseTable, ITable<string> tagsTable)
        {
            _exerciseTable = exerciseTable;
            _tagsTable = tagsTable;
            NewExerciseName = "";
            NewExerciseDescription = "";
            NewExerciseTags = [];
        }

        #region AddNewExercise
        private ICommand? addNewExercise;
        public ICommand AddNewExerciseCommand
        {
            get
            {
                addNewExercise ??= new RelayCommand(p => AddNewExercise(NewExercise), p => ExerciseIsValid(NewExercise));
                return addNewExercise;
            }
        }

        public void AddNewExercise(Exercise ex)
        {
            _exerciseTable.AddSingleRow(ex);
            OnPropertyChanged(nameof(Exercises));
            OnPropertyChanged(nameof(AllExerciseTags));
        }

        public bool ExerciseIsValid(Exercise ex)
        {
            return!_exerciseTable.AllNames.Contains(ex.Name);
        }
        #endregion

        #region DeleteExercise
        private ICommand? deleteExerciseCommand;
        public ICommand DeleteExerciseCommand
        {
            get
            {
                deleteExerciseCommand ??= new RelayCommand(p => DeleteExercise(SelectedExercise), p => SelectedExercise != null);
                return deleteExerciseCommand;
            }
        }
        public void DeleteExercise(Exercise? log)
        {
            if (log != null)
            {
                _exerciseTable.RemoveSingleRow(log);
                OnPropertyChanged(nameof(Exercises));
                OnPropertyChanged(nameof(AllExerciseTags));
            }
                
            else
                throw new ArgumentNullException(nameof(log));
        }
        #endregion
    }
}
