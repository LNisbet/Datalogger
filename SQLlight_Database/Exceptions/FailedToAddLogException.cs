﻿namespace SQLight_Database.Exceptions
{
    public class FailedToAddLogException : Exception
    {
        public FailedToAddLogException() { }

        public FailedToAddLogException(string message) : base(message) { }
    }
}
