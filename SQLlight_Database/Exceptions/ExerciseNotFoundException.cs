namespace SQLight_Database
{
    public class ExerciseNotFoundException : Exception
    {
        public ExerciseNotFoundException() { }

        public ExerciseNotFoundException(string message) : base(message) { }
    }
}
