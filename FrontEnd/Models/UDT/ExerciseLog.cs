
namespace DataLogger.Models
{
    public class ExerciseLog
    {
        public DateOnly Date { get; set; }
        public Exercise Exercise { get; set; }
        public float Value { get; set; }

        public ExerciseLog(DateOnly date, Exercise exercise, float value)
        {
            Date = date;
            Exercise = exercise;
            Value = value;
        }
        public ExerciseLog(Exercise exercise, float value)
        {
            Date = DateOnly.FromDateTime(DateTime.Now);
            Exercise = exercise;
            Value = value;
        }
        public ExerciseLog() { } // Add a parameterless constructor for CsvHelper to instantiate the object
    }
}
