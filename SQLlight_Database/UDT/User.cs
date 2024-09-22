using System.Data.SQLite;

namespace SQLight_Database
{
    public class User
    {
        public string Name { get; }

        public User(string name)
        {
            Name = name;
        }

        public User(SQLiteDataReader sqlite_datareader)
        {
            if (sqlite_datareader != null && sqlite_datareader.HasRows && !sqlite_datareader.IsClosed)
            {
                Name = (string)sqlite_datareader.GetValue(0);
            }
            else
                throw new ArgumentNullException();
        }

    public List<string> ToSQLStringList()
        {
            List<string> list = [$"'{Name}'"];
            return list;
        }
    }
}
