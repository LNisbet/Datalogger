﻿using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using CsvHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLogger.Models
{
    public class ExerciseConverter : ITypeConverter
    {
        private IDatabase _database = Model.InternalDatabase;
        ExerciseConverter(IDatabase database) 
        {
            _database = database;
        }
        ExerciseConverter()
        {
            _database = Model.InternalDatabase;
        }

        public object ConvertFromString(string? text, IReaderRow row, MemberMapData memberMapData)
        {
            ArgumentNullException.ThrowIfNull(text);
            return _database.SelectExerciseByName(text);
        }

        public string ConvertToString(object? value, IWriterRow row, MemberMapData memberMapData)
        {
            ArgumentNullException.ThrowIfNull(value);
            return ((Exercise)value).Name; // Convert back to string if you need to write it out
        }
    }

}
