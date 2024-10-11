using System.Data.SQLite;

namespace SQLight_Database
{
    public class User
    {
        public int? Id { get;}
        public string Name { get;}
        public bool Initilised { get; set; }

        public User(string name, bool initilised, int? id = null)
        {
            Id = id;
            Name = name;
            Initilised = initilised;
        }

        public User(SQLiteDataReader sqlite_datareader)
        {
            if (sqlite_datareader != null && sqlite_datareader.HasRows && !sqlite_datareader.IsClosed)
            {
                Id = sqlite_datareader.GetInt32(0);
                Name = sqlite_datareader.GetString(1);
                Initilised = sqlite_datareader.GetBoolean(2);
            }
            else
                throw new ArgumentNullException();
        }

        public List<string> ToSQLStringList()
        {
            return ["null",$"'{Name}'", $"{Initilised}"];
        }
    }
}
