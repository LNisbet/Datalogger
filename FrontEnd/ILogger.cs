using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLogger
{
    public interface ILogger
    {
        List<ExerciseLog> ExerciseLogs { get; }
        List<string> Exercises { get; }

        void AddNewExercise(string exercise);
        void AddNewLog(ExerciseLog log);
    }
}
