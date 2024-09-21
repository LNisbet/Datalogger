using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using CsvHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLight_Database;

namespace SQLight_Database
{
    public class ExerciseConverter : ITypeConverter
    {
        public object ConvertFromString(string? text, IReaderRow row, MemberMapData memberMapData)
        {
            ArgumentNullException.ThrowIfNull(text);
            throw new NotImplementedException();
            //return SQL_Database.Exercises.SelectExerciseByName(text);
        }

        public string ConvertToString(object? value, IWriterRow row, MemberMapData memberMapData)
        {
            ArgumentNullException.ThrowIfNull(value);
            return ((Exercise)value).Name; // Convert back to string if you need to write it out
        }
    }

}
