using System.Collections.ObjectModel;
using System.Windows.Input;
using SQLight_Database;
using System.Windows;

namespace DataLogger.ViewModels
{
    public class CreateExercise_VM : Base_VM
    {
        #region Fields
        public string NewExerciseName { get; set; }

        public ObservableCollection<string> NewExerciseTags { get; set; }
        public Enums.Units NewUnit1 { get; set; }
        public Enums.Units? NewUnit2 { get; set; }
        public Enums.Units? NewUnit3 { get; set; }
        public Enums.Units? NewUnit4 { get; set; }

        public string? NewExerciseDescription { get; set; }

        private Exercise newExercise { get => new(NewExerciseName, NewExerciseTags.ToList(), NewUnit1, NewUnit2, NewUnit3, NewUnit4, NewExerciseDescription); }

        public ObservableCollection<string> AllExerciseTags { get => TagsTable.AllExerciseTags;}

        public ObservableCollection<Exercise> Exercises => ExerciseTable.Exercises;

        public Exercise? SelectedExercise { get; set; }
        #endregion

        public CreateExercise_VM()
        {
            NewExerciseName = "";
            NewExerciseDescription = "";
            NewExerciseTags = new ObservableCollection<string>();
        }

        #region AddNewExercise
        private ICommand? addNewExercise;
        public ICommand AddNewExerciseCommand
        {
            get
            {
                if (addNewExercise == null)
                {
                    addNewExercise = new RelayCommand(
                        p => ExerciseIsValid(newExercise),
                        p => AddNewExercise(newExercise));
                }
                return addNewExercise;
            }
        }

        public void AddNewExercise(Exercise ex)
        {
            ExerciseTable.AddSingleExercise(ex);
            OnPropertyChanged(nameof(Exercises));
            OnPropertyChanged(nameof(AllExerciseTags));
        }

        public bool ExerciseIsValid(Exercise ex)
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
                if (deleteExerciseCommand == null)
                {
                    deleteExerciseCommand = new RelayCommand(
                        p => SelectedExercise != null,
                        p => DeleteExercise(SelectedExercise));
                }
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
