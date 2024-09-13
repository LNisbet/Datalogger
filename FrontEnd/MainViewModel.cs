using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Windows;

namespace DataLogger
{
    public class MainViewModel
    {
        public ILogger mainModel;
        string Exercise;
        List<string> Exercises = new();
        bool SpecifyDate = false;
        DateOnly Date = DateOnly.FromDateTime(DateTime.Now);
        float Value = 0;

        public void AddNewExercise()
        {
            mainModel.AddNewExercise(Exercise);
        }

        public void AddNewLog()
        {
            ExerciseLog log;
            if (SpecifyDate)
                log = new ExerciseLog(Date,Exercise,Value);
            else
                log = new ExerciseLog(Exercise, Value);

            mainModel.AddNewLog(log);
        }
    }
}
