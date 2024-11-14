using System.Data.SQLite;
using SQLight_Database;
using SQLight_Database.Tables.Interfaces;

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
        public float? Value4 { get; set; }
        public string? Note { get; set; }

        public ExerciseLog()
        {
        }

        public ExerciseLog(DateOnly date, Exercise exercise, float value1, float? value2 = null, float? value3 = null, float? value4 = null, string? note = null, int? id = null)
        {
            Id = id;
            Date = date;
            Exercise = exercise;
            Value1 = value1;
            Value2 = value2;
            Value3 = value3;
            Value4 = value4;
            Note = note;
        }

        public ExerciseLog(SQLiteDataReader sqlite_datareader, IExerciseTable exerciseTable)
        {
            if (sqlite_datareader != null && sqlite_datareader.HasRows && !sqlite_datareader.IsClosed)
            {
                Id = sqlite_datareader.GetInt32(0);
                Date = DateOnly.Parse(sqlite_datareader.GetString(1));
                Exercise = exerciseTable.SelectExerciseByName(sqlite_datareader.GetString(2));
                Value1 = (float)sqlite_datareader.GetFloat(3);
                Value2 = GetFloatFromDatabase(sqlite_datareader,4);
                Value3 = GetFloatFromDatabase(sqlite_datareader, 5);
                Value4 = GetFloatFromDatabase(sqlite_datareader, 6);
                Note = (string?)sqlite_datareader.GetString(7);
            }
            else
                throw new ArgumentNullException();
        }

        private static float? GetFloatFromDatabase(SQLiteDataReader sqlite_datareader, int col)
        {
            try
            {
                return (float)sqlite_datareader.GetFloat(col);
            }
            catch
            {
                return null;
            }
        }

        public List<string> ToSQLStringList()
        {
            var value2 = Value2 == null ? "null" : $"{Value2}";
            var value3 = Value3 == null ? "null" : $"{Value3}";
            var value4 = Value4 == null ? "null" : $"{Value4}";
            var note = Note == null ? "'null'" : $"'{Note}'";

            return ["null", $"'{Date}'", $"'{Exercise.Name}'", $"{Value1}", value2, value3, value4, note];
        }
    }
}
