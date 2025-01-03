﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper.Configuration;
using SQLight_Database.Models;

namespace CSV_Exporter.Converters
{
    public class ExerciseFromCSVMap : ClassMap<Exercise>
    {
        public ExerciseFromCSVMap()
        {
            Map(m => m.Name);
            Map(m => m.Tags).Name("Tags").Convert(row => row.Row.GetField("Tags").Split(',').ToList());
            Map(m => m.Unit1);
            Map(m => m.Unit2).Optional();
            Map(m => m.Unit3).Optional();
            Map(m => m.Unit4).Optional();
            Map(m => m.Description).Optional();
        }
    }

}
