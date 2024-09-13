
namespace DataLogger.Models
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
