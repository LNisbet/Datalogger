namespace SQLight_Database
{
    public class ExerciseLog
    {
        public DateOnly Date { get; set; }
        public Exercise Exercise { get; set; }
        public float Value { get; set; }

        public string? Note { get; set; }

        public ExerciseLog(DateOnly date, Exercise exercise, float value)
        {
            Date = date;
            Exercise = exercise;
            Value = value;
        }
        public ExerciseLog(DateOnly date, Exercise exercise, float value, string note)
        {
            Date = date;
            Exercise = exercise;
            Value = value;
            Note = note;
        }
    }
}
