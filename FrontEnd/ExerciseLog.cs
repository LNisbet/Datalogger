using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLogger
{
    public class ExerciseLog
    {
        public DateOnly Date { get; }
        public string Exercise { get; }
        public float Value { get; }

        public ExerciseLog(DateOnly date, string exercise, float value) 
        {
            Date = date;
            Exercise = exercise;
            Value = value;
        }
        public ExerciseLog(string exercise, float value)
        {
            Date = DateOnly.FromDateTime(DateTime.Now);
            Exercise = exercise;
            Value = value;
        }
    }
}
