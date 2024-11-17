namespace SQLight_Database.Exceptions
{
    public class ExerciseNotFoundException : Exception
    {
        public ExerciseNotFoundException() { }

        public ExerciseNotFoundException(string message) : base(message) { }
    }
}
