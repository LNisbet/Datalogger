
using SQLight_Database.Config.Interface;
using SQLight_Database.Models;

namespace SQLight_Database.Config
{
    public class LogsTableConfig : ITableConfig<ExerciseLog>
    {
        public string Name => "ExerciseLogs";

        public List<ColumnDescription> Description =>
        [
            new ColumnDescription("Id", "INTEGER PRIMARY KEY"),
            new ColumnDescription("Date", "DATE", "NOT NULL"),
            new ColumnDescription("Exercise", "VARCHAR(20)", "NOT NULL"),
            new ColumnDescription("Value1", "FLOAT", "NOT NULL"),
            new ColumnDescription("Value2", "FLOAT"),
            new ColumnDescription("Value3", "FLOAT"),
            new ColumnDescription("Value4", "FLOAT"),
            new ColumnDescription("Note", "TEXT")
        ];

        public List<ExerciseLog>? DefaultValues => null;
    }
}
