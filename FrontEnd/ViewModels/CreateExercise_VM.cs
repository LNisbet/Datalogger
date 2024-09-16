using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using DataLogger.Models;

namespace DataLogger.ViewModels
{
    public class CreateExercise_VM : NotifyPropertyChanged
    {
        #region Fields
        public string NewType { get; set; }

        public string NewExercise { get; set; }

        public string NewExerciseType { get; set; }

        public string NewExerciseDescription { get; set; }

        public ObservableCollection<string> AllExerciseTypes { get => Model.InternalDatabase.AllExerciseTypes;}

        public ObservableCollection<Exercise> Exercises{ get => Model.InternalDatabase.Exercises; set => Model.InternalDatabase.Exercises = value; }
        #endregion

        public CreateExercise_VM()
        {
            NewType = "";
            NewExercise = "";
            NewExerciseType = "Default";
            NewExerciseDescription = "";

            Model.InternalDatabase.PropertyChanged += (sender, args) => OnPropertyChanged(args.PropertyName);
        }

        #region AddNewType
        private ICommand addNewType;
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
            Model.InternalDatabase.AddNewExerciseType(ty);
            OnPropertyChanged(nameof(Exercises));
        }

        public bool TypeIsValid(string ty)
        {
            return !AllExerciseTypes.Contains(ty) && ty != "";
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
                    var ex = new Exercise(NewExercise, NewExerciseType, NewExerciseDescription);
                    addNewExercise = new RelayCommand(
                        p => ExerciseIsValid(ex),
                        p => AddNewExercise(ex));
                }
                return addNewExercise;
            }
        }

        public void AddNewExercise(Exercise ex)
        {
            MessageBox.Show(ex.Name + " " + ex.Type + " " + ex.Description);
            Model.InternalDatabase.AddNewExercise(ex);
            OnPropertyChanged(nameof(Exercises));
        }

        public bool ExerciseIsValid(Exercise ex)
        {
            return AllExerciseTypes.Contains(ex.Type) && !Model.InternalDatabase.AllExerciseNames.Contains(ex.Name);
        }
        #endregion
    }
}
