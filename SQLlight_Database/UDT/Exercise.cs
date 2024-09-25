using System.Data.SQLite;

namespace SQLight_Database
{
    public class Exercise
    {
        public string Name { get; }
        public string Unit1 { get; }
        public string? Unit2 { get; }
        public string? Unit3 { get; }
        public string Type { get; }
        public string? Description { get; }

        public Exercise(string name, string unit1, string type, string? unit2 = null, string? unit3 = null, string? description = null)
        {
            Name = name;
            Unit1 = unit1;
            Unit2 = unit2;
            Unit3 = unit3;
            Type = type;
            Description = description;
        }

        public Exercise(SQLiteDataReader sqlite_datareader)
        {
            if (sqlite_datareader != null && sqlite_datareader.HasRows && !sqlite_datareader.IsClosed)
            {
                Name = sqlite_datareader.GetString(0);
                Unit1 = sqlite_datareader.GetString(1);
                Unit2 = sqlite_datareader.GetString(2);
                Unit3 = sqlite_datareader.GetString(3);
                Type = (string)sqlite_datareader.GetValue(4);
                Description = (string?)sqlite_datareader.GetValue(5);
            }
            else
                throw new ArgumentNullException();
        }

    public List<string> ToSQLStringList()
        {
            var unit2 = Unit2 == null ? "'null'" : $"'{Unit2}'";
            var unit3 = Unit3 == null ? "'null'" : $"'{Unit3}'";
            var description = Description == null ? "'null'" : $"'{Description}'";

            return [$"'{Name}'", $"'{Unit1}'", unit2, unit3, $"'{Type}'", description];
        }
    }
}
