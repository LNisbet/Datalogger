using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLight_Database
{
    internal static class Config
    {
        static internal readonly string ExercieseTableName = "Exercises";
        static internal readonly List<ColumnDescription> ExerciseTableDescription =
        [
            new ColumnDescription("Name", "VARCHAR(20)", "PRIMARY KEY"),
            new ColumnDescription("Unit1", "VARCHAR(20)", "NOT NULL"),
            new ColumnDescription("Unit2", "VARCHAR(20)", "NOT NULL"),
            new ColumnDescription("Unit3", "VARCHAR(20)", "NOT NULL"),
            new ColumnDescription("Type", "VARCHAR(20)", "NOT NULL"),
            new ColumnDescription("Description", "TEXT")
        ];

        static internal readonly string LogsTableName = "ExerciseLogs";
        static internal readonly List<ColumnDescription> LogTableDescription =
        [
            new ColumnDescription("Id", "INTEGER PRIMARY KEY"),
            new ColumnDescription("Date", "DATE", "NOT NULL"),
            new ColumnDescription("Exercise", "VARCHAR(20)", "NOT NULL"),
            new ColumnDescription("Value1", "FLOAT", "NOT NULL"),
            new ColumnDescription("Value2", "FLOAT"),
            new ColumnDescription("Value3", "FLOAT"),
            new ColumnDescription("Note", "TEXT")
        ];
    }
}
