namespace SQLight_Database
{
    public class NoOpenSQLConnection : Exception
    {
        public NoOpenSQLConnection() { }

        public NoOpenSQLConnection(string message) : base(message) { }
    }
}
