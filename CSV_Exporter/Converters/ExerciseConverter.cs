using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using CsvHelper;
using SQLight_Database;
using SQLight_Database.Tables.Interfaces;

namespace CSV_Exporter
{
    public class ExerciseConverter : ITypeConverter
    {
        private readonly IExerciseTable _exerciseTable;
        public ExerciseConverter(IExerciseTable exerciseTable) 
        {
            _exerciseTable = exerciseTable;
        }

        public object ConvertFromString(string? text, IReaderRow row, MemberMapData memberMapData)
        {
            ArgumentNullException.ThrowIfNull(text);
            Exercise exercise;
            var list = text.Split(',').ToList();
            try
            {
                exercise = _exerciseTable.SelectExerciseByName(list[0]);
            }
            catch
            {
                try
                {
                    Enum.TryParse(list[1], out Exercise.Units unit1);
                    Enum.TryParse(list[2], out Exercise.Units unit2);
                    Enum.TryParse(list[3], out Exercise.Units unit3);
                    Enum.TryParse(list[4], out Exercise.Units unit4);
                    exercise = new(list[0], ["Imported"], unit1, unit2, unit3, unit4);
                }
                catch
                {
                    exercise = new(list[0], ["Partial Imported"], Exercise.Units.None);
                }
            }
            return exercise;
        }

        public string ConvertToString(object? value, IWriterRow row, MemberMapData memberMapData)
        {
            ArgumentNullException.ThrowIfNull(value);
            string name = ((Exercise)value).Name;
            string unit1 = ((Exercise)value).Unit1.ToString();
            string? unit2 = ((Exercise)value).Unit2.ToString();
            string? unit3 = ((Exercise)value).Unit3.ToString();
            string? unit4 = ((Exercise)value).Unit4.ToString();

            return $"{name},{unit1},{unit2},{unit3},{unit4}"; // Convert back to string if you need to write it out
        }
    }

}
