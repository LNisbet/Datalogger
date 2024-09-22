namespace SQLight_Database
{
    public class FailedToAddLogException : Exception
    {
        public FailedToAddLogException() { }

        public FailedToAddLogException(string message) : base(message) { }
    }
}
