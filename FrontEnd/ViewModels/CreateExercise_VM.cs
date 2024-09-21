using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using SQLight_Database;

namespace DataLogger.ViewModels
{
    public class CreateExercise_VM : NotifyPropertyChanged
    {
        #region Fields
        public string NewType { get; set; }

        public string NewExerciseName { get; set; }

        public string NewExerciseType { get; set; }

        public string NewExerciseDescription { get; set; }

        private Exercise newExercise { get => new(NewExerciseName, NewExerciseType, NewExerciseDescription); }

        public ObservableCollection<string> AllExerciseTypes { get => SQL_Database.AllExerciseTypes;}

        public ObservableCollection<Exercise> Exercises{ get => SQL_Database.Exercises; }
        #endregion

        public CreateExercise_VM()
        {
            NewType = "";
            NewExerciseName = "";
            NewExerciseType = "";
            NewExerciseDescription = "";

            //SQL_Database.InternalDatabase.PropertyChanged += (sender, args) => OnPropertyChanged(args.PropertyName);
        }

        #region AddNewType
        private ICommand? addNewType;
        public ICommand AddNewTypeCommand
        {
            get
            {
                if (addNewType == null)
                {
                    addNewType = new RelayCommand(
                        p => TypeIsValid(NewType),
                        p => AddNewExerciseType(NewType));
                }
                return addNewType;
            }
        }

        public void AddNewExerciseType(string ty)
        {
            throw new NotImplementedException();
            //SQL_Database.InternalDatabase.AddNewExerciseType(ty);
            OnPropertyChanged(nameof(Exercises));
        }

        public bool TypeIsValid(string ty)
        {
            return !AllExerciseTypes.Contains(ty) && ty != "";
        }
        #endregion

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
            SQL_Database.AddNewExercise(ex);
            OnPropertyChanged(nameof(Exercises));
        }

        public bool ExerciseIsValid(Exercise ex)
        {
            return AllExerciseTypes.Contains(ex.Type) && !SQL_Database.AllExerciseNames.Contains(ex.Name);
        }
        #endregion
    }
}
