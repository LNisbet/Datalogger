using CsvHelper.Configuration;
using CsvHelper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace DataLogger.Models
{
    internal class CSVHelper : ICSV
    {
        List<string> ICSV.ReadExercisesFromCSV(string path)
        {
            throw new NotImplementedException();
        }

        List<ExerciseLog> ICSV.ReadLogsFromCSV(string path)
        {
            throw new NotImplementedException();
        }

        void ICSV.WrirteToCSV<T>(string path, List<T> list)
        {
            using (var writer = new StreamWriter(path))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(list);
            }
        }
    }
}
