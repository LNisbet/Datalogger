using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper.Configuration;
using SQLight_Database;

namespace CSV_Exporter.Converters
{
    public class ExerciseToCSVMap : ClassMap<Exercise>
    {
        public ExerciseToCSVMap()
        {
            Map(m => m.Name);
            Map(m => m.Tags).Convert(row => string.Join(',', row.Value.Tags));
            Map(m => m.Unit1);
            Map(m => m.Unit2).Optional();
            Map(m => m.Unit3).Optional();
            Map(m => m.Unit4).Optional();
            Map(m => m.Description).Optional();
        }
    }

}
