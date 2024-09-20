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
    public static class ExerciseLog_SQLConverter
    {
        public static ExerciseLog ConvertFromString(string text)
        {
            throw new NotImplementedException();
        }

        public static List<string> ConvertToStringList(ExerciseLog log)
        {
            List<string> list = [$"'{log.Date}'",log.Exercise.Name,$"'{ log.Value}'"];
            if (log.Note != null)
                list.Add(log.Note);
            return list;
        }
    }

}
