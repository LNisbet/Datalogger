using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLogger.Models
{
    public class ExerciseLogMap : ClassMap<ExerciseLog>
    {
        public ExerciseLogMap()
        {
            Map(m => m.Date).Name("Date").TypeConverterOption.Format("MM/dd/yyyy"); // Adjust format based on your CSV format
            Map(m => m.Exercise).Name("Exercise").TypeConverter<ExerciseConverter>(); // Custom conversion for Exercise class
            Map(m => m.Value).Name("Value");
        }
    }

}
