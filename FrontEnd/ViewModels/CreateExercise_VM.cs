using System.Collections.ObjectModel;
using System.Windows.Input;
using SQLight_Database;
using System.Windows;

namespace DataLogger.ViewModels
{
    public class CreateExercise_VM : NotifyPropertyChanged
    {
        #region Fields
        public string NewExerciseName { get; set; }

        public string NewExerciseType { get; set; }

        public string NewExerciseDescription { get; set; }

        private Exercise newExercise { get => new(NewExerciseName, NewExerciseType, NewExerciseDescription); }

        public ObservableCollection<string> AllExerciseTypes { get => SQL_Database.AllExerciseTypes;}

        public ObservableCollection<Exercise> Exercises{ get => SQL_Database.Exercises; }

        public Exercise? SelectedExercise { get; set; }
        #endregion

        public CreateExercise_VM()
        {
            NewExerciseName = "";
            NewExerciseType = "";
            NewExerciseDescription = "";
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
            SQL_Database.AddSingleExercise(ex);
            OnPropertyChanged(nameof(Exercises));
            OnPropertyChanged(nameof(AllExerciseTypes));
        }

        public bool ExerciseIsValid(Exercise ex)
        {
            return!SQL_Database.AllExerciseNames.Contains(ex.Name);
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
                SQL_Database.RemoveSingleExercise(log);
                OnPropertyChanged(nameof(Exercises));
                OnPropertyChanged(nameof(AllExerciseTypes));
            }
                
            else
                throw new ArgumentNullException(nameof(log));
        }
        #endregion
    }
}
