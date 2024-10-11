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
    }
}
