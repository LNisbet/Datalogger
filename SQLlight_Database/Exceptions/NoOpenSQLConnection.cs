namespace SQLight_Database.Exceptions
{
    public class NoOpenSQLConnection : Exception
    {
        public NoOpenSQLConnection() { }

        public NoOpenSQLConnection(string message) : base(message) { }
    }
}
