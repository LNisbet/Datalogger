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
            Map(m => m.Value1).Name("Value1");
            Map(m => m.Value2).Name("Value2");
            Map(m => m.Value3).Name("Value3");
            Map(m => m.Value4).Name("Value4");
            Map(m => m.Note).Name("Note");
        }
    }

}
