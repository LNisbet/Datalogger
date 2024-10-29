using System.Data.SQLite;

namespace SQLight_Database
{
    public class Exercise
    {
        public string Name { get; set; }
        public List<string> Tags { get; set; }
        public Enums.Units Unit1 { get; set; }
        public Enums.Units? Unit2 { get; set; }
        public Enums.Units? Unit3 { get; set; }
        public Enums.Units? Unit4 { get; set; }

        public string? Description { get; set; }

        public Exercise() { }

        public Exercise(string name, List<string> tags, Enums.Units unit1, Enums.Units? unit2 = null, Enums.Units? unit3 = null, Enums.Units? unit4 = null,  string? description = null)
        {
            Name = name;
            Tags = tags;
            Unit1 = unit1;
            Unit2 = unit2;
            Unit3 = unit3;
            Unit4 = unit4;
            Description = description;
        }

        public Exercise(SQLiteDataReader sqlite_datareader)
        {
            if (sqlite_datareader != null && sqlite_datareader.HasRows && !sqlite_datareader.IsClosed)
            {
                Name = sqlite_datareader.GetString(0);
                Tags = TagsFromString(sqlite_datareader.GetString(1));
                Unit1 = (Enums.Units)Enum.Parse(typeof(Enums.Units), sqlite_datareader.GetString(2));
                Unit2 = GetUnitsFromDatabase(sqlite_datareader, 3);
                Unit3 = GetUnitsFromDatabase(sqlite_datareader, 4);
                Unit4 = GetUnitsFromDatabase(sqlite_datareader, 5);
                Description = (string?)sqlite_datareader.GetValue(6);
            }
            else
                throw new ArgumentNullException();
        }

        private static Enums.Units? GetUnitsFromDatabase(SQLiteDataReader sqlite_datareader, int col)
        {
            try
            {
                return (Enums.Units)Enum.Parse(typeof(Enums.Units), sqlite_datareader.GetString(col));
            }
            catch 
            { 
                return null; 
            }
        }

        public List<string> ToSQLStringList()
        {
            var unit2 = Unit2 == null ? "'null'" : $"'{Unit2}'";
            var unit3 = Unit3 == null ? "'null'" : $"'{Unit3}'";
            var unit4 = Unit4 == null ? "'null'" : $"'{Unit4}'";
            var description = Description == null ? "'null'" : $"'{Description}'";

            return [$"'{Name}'", TagsToString(Tags), $"'{Unit1}'", unit2, unit3, unit4, description];
        }

        private static string TagsToString(List<string>? list)
        {
            if (list == null)
                return "'null'";

            var tagsAsAString = "'";
            var i = 0;
            foreach (var tag in list) 
            {
                tagsAsAString += tag;
                if (i < list.Count - 1)
                    tagsAsAString += ",";

                i++;
            }
            tagsAsAString += "'";

            return tagsAsAString;
        }

        private static List<string> TagsFromString(string tags)
        {
            return tags.Split(',').ToList();
        }
    }
}
