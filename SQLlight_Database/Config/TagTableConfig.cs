using SQLight_Database.Config.Interface;
using SQLight_Database.Models;

namespace SQLight_Database.Config
{
    public class TagTableConfig : ITableConfig<string>
    {
        public string Name => "Tags";

        public List<ColumnDescription> Description =>
        [
            new ColumnDescription("Tags", "TEXT", "PRIMARY KEY")
        ];

        public List<string> DefaultValues =>
        [
            "UpperBody",
            "LowerBody",
            "Core",
            "Fingers",
            "Endurance",
            "Strength",
            "Power",
            "Flexibility"
        ];
    }
}
