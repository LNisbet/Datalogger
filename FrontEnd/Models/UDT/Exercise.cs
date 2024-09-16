
namespace DataLogger.Models
{
    public class Exercise
    {
        public string Name { get; }
        public string Type { get; }
        public string Description { get; }

        public Exercise(string name, string type)
        {
            Name = name;
            Type = type;
            Description = "";
        }

        public Exercise(string name, string type, string description)
        {
            Name = name;
            Type = type;
            Description = description;
        }
    }
}
