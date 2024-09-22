using CsvHelper.Configuration;
using SQLight_Database;

namespace CSV_Exporter
{
    public class ExerciseLogMap : ClassMap<ExerciseLog>
    {
        public ExerciseLogMap()
        {
            Map(m => m.Date).Name("Date").TypeConverterOption.Format("MM/dd/yyyy"); // Adjust format based on your CSV format
            Map(m => m.Exercise).Name("Name").TypeConverter<ExerciseConverter>(); // Custom conversion for Exercise class
            Map(m => m.Value).Name("Value");
            Map(m => m.Note).Name("Note");
        }
    }

}
