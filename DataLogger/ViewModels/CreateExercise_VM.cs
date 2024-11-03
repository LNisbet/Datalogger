using System.Collections.ObjectModel;
using System.Windows.Input;
using SQLight_Database;
using System.Windows;
using DataLogger.ViewModels.HelperClasses;

namespace DataLogger.ViewModels
{
    public class CreateExercise_VM : Base_VM
    {
        #region Fields
        public string NewExerciseName { get; set; }

        public ObservableCollection<string> NewExerciseTags { get; set; }
        public Exercise.Units NewUnit1 { get; set; }
        public Exercise.Units NewUnit2 { get; set; }
        public Exercise.Units NewUnit3 { get; set; }
        public Exercise.Units NewUnit4 { get; set; }

        public string? NewExerciseDescription { get; set; }

        private Exercise NewExercise { get => new(NewExerciseName, NewExerciseTags.ToList(), NewUnit1, NewUnit2, NewUnit3, NewUnit4, NewExerciseDescription); }

        public static ObservableCollection<string> AllExerciseTags => TagsTable.AllExerciseTags;

        public static ObservableCollection<Exercise> Exercises => ExerciseTable.Exercises;

        public Exercise? SelectedExercise { get; set; }
        #endregion

        public CreateExercise_VM()
        {
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
            ExerciseTable.AddSingleExercise(ex);
            OnPropertyChanged(nameof(Exercises));
            OnPropertyChanged(nameof(AllExerciseTags));
        }

        public static bool ExerciseIsValid(Exercise ex)
        {
            return!ExerciseTable.AllExerciseNames.Contains(ex.Name);
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
                ExerciseTable.RemoveSingleExercise(log);
                OnPropertyChanged(nameof(Exercises));
                OnPropertyChanged(nameof(AllExerciseTags));
            }
                
            else
                throw new ArgumentNullException(nameof(log));
        }
        #endregion
    }
}
