﻿using CsvHelper.Configuration;
using CsvHelper;
using System.Globalization;
using CSV_Exporter.Converters;
using SQLight_Database.Models;

namespace CSV_Exporter
{
    static public class CSVHelper
    {
        static public List<T> ReadFromCSV<T>(string path)
        {
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                PrepareHeaderForMatch = args => args.Header.ToLower(),
                //HeaderValidated = null,
                MissingFieldFound = null
            };

            using var reader = new StreamReader(path);
            using var csv = new CsvReader(reader, config);

            if (typeof(T) == typeof(Exercise))
            {
                csv.Context.RegisterClassMap<ExerciseFromCSVMap>();
            }
            if (typeof(T) == typeof(ExerciseLog))
            {
                csv.Context.RegisterClassMap<ExerciseLogFromCSVMap>();
            }
                
            return csv.GetRecords<T>().ToList();
        }

        static public void WriteToCSV<T>(string path, List<T> list)
        {
            using var writer = new StreamWriter(path);
            using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);

            if (typeof(T) == typeof(Exercise))
            {
                csv.Context.RegisterClassMap<ExerciseToCSVMap>();
            }
            if (typeof(T) == typeof(ExerciseLog))
            {
                csv.Context.RegisterClassMap<ExerciseLogToCSVMap>();
            }

            csv.WriteRecords(list);
        }
    }
}
