using System.Data.SQLite;
using SQLight_Database;

namespace SQLight_Database
{
    public class ExerciseLog
    {
        public DateOnly Date { get; set; }
        public Exercise Exercise { get; set; }
        public float Value { get; set; }

        public string? Note { get; set; }

        public ExerciseLog()
        {
        }

        public ExerciseLog(DateOnly date, Exercise exercise, float value)
        {
            Date = date;
            Exercise = exercise;
            Value = value;
            Note = null;
        }
        public ExerciseLog(DateOnly date, Exercise exercise, float value, string note)
        {
            Date = date;
            Exercise = exercise;
            Value = value;
            Note = note;
        }

        public ExerciseLog(SQLiteDataReader sqlite_datareader)
        {
            if (sqlite_datareader != null && sqlite_datareader.HasRows && !sqlite_datareader.IsClosed)
            {
                Date = DateOnly.Parse(sqlite_datareader.GetString(0));
                Exercise = SQL_Database.SelectExerciseByName(sqlite_datareader.GetString(1));
                Value = (float)sqlite_datareader.GetFloat(2);
                Note = (string?)sqlite_datareader.GetValue(3);
            }
            else
                throw new ArgumentNullException();
        }

        public List<string> ToSQLStringList()
        {
            List<string> list = [$"'{Date}'", $"'{Exercise.Name}'", $"{Value}"];
            if (Note == null)
                list.Add("'null'");
            else 
                list.Add($"'{Note}'");

            return list;
        }
    }
}
