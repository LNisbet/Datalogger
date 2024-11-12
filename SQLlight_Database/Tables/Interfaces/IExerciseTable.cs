using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLight_Database.Tables.Interfaces
{
    public interface IExerciseTable
    {
        ObservableCollection<Exercise> Exercises { get; }

        ObservableCollection<string> AllExerciseNames { get; }

        Exercise SelectExerciseByName(string name);

        void AddSingleExercise(Exercise exercise);

        void AddMultipleExercises(List<Exercise> exercises);

        void RemoveSingleExercise(Exercise exercise);

        void RemoveMultipleExercises(List<Exercise> exercises);

        
    }
}
