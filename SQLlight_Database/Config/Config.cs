using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLight_Database
{
    internal static class Config
    {
        static internal readonly string UserTableName = "Users";
        static internal readonly List<ColumnDescription> UserTableDescription =
        [
            new ColumnDescription("Name", "VARCHAR(20)", "PRIMARY KEY")
        ];

        static internal readonly string ExercieseTableName = "Exercises";
        static internal readonly List<ColumnDescription> ExerciseTableDescription =
        [
            new ColumnDescription("Name", "VARCHAR(20)", "PRIMARY KEY"),
            new ColumnDescription("Type", "VARCHAR(20)", "NOT NULL"),
            new ColumnDescription("Description", "TEXT")
        ];

        static internal readonly string LogsTableName = "ExerciseLogs";
        static internal readonly List<ColumnDescription> LogTableDescription =
        [
            new ColumnDescription("Date", "DATE", "NOT NULL"),
            new ColumnDescription("Exercise", "VARCHAR(20)", "NOT NULL"),
            new ColumnDescription("Value", "FLOAT", "NOT NULL"),
            new ColumnDescription("Note", "TEXT")
        ];
    }
}
