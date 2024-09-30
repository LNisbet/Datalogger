using CSV_Exporter.Converters;
using CsvHelper.Configuration;
using SQLight_Database;

namespace CSV_Exporter
{
    public class ExerciseLogToCSVMap : ClassMap<ExerciseLog>
    {
        public ExerciseLogToCSVMap()
        {
            Map(m => m.Id).Name("Id");
            Map(m => m.Date).Name("Date").TypeConverterOption.Format("dd/MM/yyyy");
            Map(m => m.Exercise).Name("Name").TypeConverter<ExerciseConverter>(); // Custom conversion for Exercise class
            Map(m => m.Value1).Name("Value1");
            Map(m => m.Value2).Name("Value2");
            Map(m => m.Value3).Name("Value3");
            Map(m => m.Value4).Name("Value4");
            Map(m => m.Note).Name("Note");
        }
    }

}
