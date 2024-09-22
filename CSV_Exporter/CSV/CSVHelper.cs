using CsvHelper.Configuration;
using CsvHelper;
using System.Globalization;

namespace CSV_Exporter
{
    static public class CSVHelper
    {
        static public List<T> ReadFromCSV<T>(string path)
        {
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                PrepareHeaderForMatch = args => args.Header.ToLower(),
                HeaderValidated = null
            };

            using var reader = new StreamReader(path);
            using var csv = new CsvReader(reader, config);
            csv.Context.RegisterClassMap<ExerciseLogMap>(); // Register custom map
            return csv.GetRecords<T>().ToList();
        }

        static public void WriteToCSV<T>(string path, List<T> list)
        {
            using var writer = new StreamWriter(path);
            using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);
            csv.WriteRecords(list);
        }
    }
}
