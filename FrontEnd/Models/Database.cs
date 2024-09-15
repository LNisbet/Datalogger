using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLogger.Models
{
    static public class Database
    {
        static private ObservableCollection<ExerciseLog> exerciseLogs = new ObservableCollection<ExerciseLog>();

        static public ObservableCollection<ExerciseLog> ExerciseLogs { get => exerciseLogs; set => exerciseLogs = value; }



        static private ObservableCollection<Exercise> exercises = new ObservableCollection<Exercise>() { new Exercise("pullup"), new Exercise("pushup"),new Exercise("curl"),new Exercise("hang") };

        static public ObservableCollection<Exercise> Exercises { get => exercises; set => exercises = value; }
    }
}
