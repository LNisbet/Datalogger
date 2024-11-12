using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLight_Database.Tables.Interfaces
{
    public interface ITagsTable
    {
        ObservableCollection<string> AllExerciseTags { get; }

        void AddSingleTag(string tag);

        void AddMultipleTags(List<string> tags);

        void RemoveSingleTag(string tag);

        void RemoveMultipleTags(List<string> tags);
    }
}
