using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using CsvHelper;
using SQLight_Database;

namespace CSV_Exporter
{
    public class ExerciseConverter : ITypeConverter
    {
        public object ConvertFromString(string? text, IReaderRow row, MemberMapData memberMapData)
        {
            ArgumentNullException.ThrowIfNull(text);
            return SQL_Database.SelectExerciseByName(text);
        }

        public string ConvertToString(object? value, IWriterRow row, MemberMapData memberMapData)
        {
            ArgumentNullException.ThrowIfNull(value);
            return ((Exercise)value).Name; // Convert back to string if you need to write it out
        }
    }

}
