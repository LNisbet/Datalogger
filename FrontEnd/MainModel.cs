using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DataLogger
{
    public class MainModel : ILogger
    {

        private List<ExerciseLog> exerciseLogs = new();
        private List<string> exercises = new();

        List<ExerciseLog> ILogger.ExerciseLogs {  get { return exerciseLogs; } }

        List<string> ILogger.Exercises { get { return exercises; } }

        void ILogger.AddNewExercise(string exercise)
        {
            if (!exercises.Contains(exercise) && exercise != "")
                exercises.Add(exercise);
        }

        void ILogger.AddNewLog(ExerciseLog log)
        {
            if (LogIsValid(log))
                exerciseLogs.Add(log);
            else
                throw new NotImplementedException();
        }

        private bool LogIsValid(ExerciseLog log)
        {
            return (log != null && exercises.Contains(log.Exercise));
        }
    }
}
