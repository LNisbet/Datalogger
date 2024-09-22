using System.Data.SQLite;

namespace SQLight_Database
{
    public class Exercise
    {
        public string Name { get; }
        public string Type { get; }
        public string? Description { get; }



        public Exercise(string name, string type)
        {
            Name = name;
            Type = type;
            Description = null;
        }

        public Exercise(string name, string type, string description)
        {
            Name = name;
            Type = type;
            Description = description;
        }

        public Exercise(SQLiteDataReader sqlite_datareader)
        {
            if (sqlite_datareader != null && sqlite_datareader.HasRows && !sqlite_datareader.IsClosed)
            {
                Name = (string)sqlite_datareader.GetValue(0);
                Type= (string)sqlite_datareader.GetValue(1);
                Description = (string?)sqlite_datareader.GetValue(2);
            }
            else
                throw new ArgumentNullException();
        }

    public List<string> ToSQLStringList()
        {
            List<string> list = [$"'{Name}'", $"'{Type}'"];
            if (Description == null)
                list.Add("'null'");
            else
                list.Add($"'{Description}'");

            return list;
        }
    }
}
