
namespace SQLight_Database.Models
{
    public class User
    {
        public int? Id { get; }
        public string Name { get; }
        public bool TagsTableInitilised { get; set; }
        public bool LogsTableInitilised { get; set; }
        public bool ExerciseTableInitilised { get; set; }

        public User(string name, bool tagsTableInitilised = false, bool logsTableInitilised = false, bool exerciseTableInitilised = false, int? id = null)
        {
            Id = id;
            Name = name;
            TagsTableInitilised = tagsTableInitilised;
            LogsTableInitilised = logsTableInitilised;
            ExerciseTableInitilised = exerciseTableInitilised;
        }
    }
}
