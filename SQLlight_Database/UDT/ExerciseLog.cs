using System.Data.SQLite;
using SQLight_Database;

namespace SQLight_Database
{
    public class ExerciseLog
    {
        public int? Id { get; set; }
        public DateOnly Date { get; set; }
        public Exercise Exercise { get; set; }
        public float Value1 { get; set; }
        public float? Value2 { get; set; }
        public float? Value3 { get; set; }
        public string? Note { get; set; }

        public ExerciseLog()
        {
        }
        public ExerciseLog(DateOnly date, Exercise exercise, float value1, float? value2 = null, float? value3 = null, string? note = null, int? id = null)
        {
            Id = id;
            Date = date;
            Exercise = exercise;
            Value1 = value1;
            Value2 = value2;
            Value3 = value3;
            Note = note;
        }

        public ExerciseLog(SQLiteDataReader sqlite_datareader)
        {
            if (sqlite_datareader != null && sqlite_datareader.HasRows && !sqlite_datareader.IsClosed)
            {
                Id = sqlite_datareader.GetInt32(0);
                Date = DateOnly.Parse(sqlite_datareader.GetString(1));
                Exercise = SQL_Database.SelectExerciseByName(sqlite_datareader.GetString(2));
                Value1 = (float)sqlite_datareader.GetFloat(3);
                Value2 = sqlite_datareader.GetFloat(4);
                Value3 = sqlite_datareader.GetFloat(5);
                Note = (string?)sqlite_datareader.GetString(6);
            }
            else
                throw new ArgumentNullException();
        }

        public List<string> ToSQLStringList()
        {
            var value2 = Value2 == null ? "'null'" : $"{Value2}";
            var value3 = Value3 == null ? "'null'" : $"{Value3}";
            var note = Note == null ? "'null'" : $"'{Note}'";

            return ["'null'", $"'{Date}'", $"'{Exercise.Name}'", $"{Value1}", value2, value3, note];
        }
    }
}
