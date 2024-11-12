using SQLight_Database.Tables.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLight_Database
{
    public class TagsTable : ITagsTable
    {
        private ObservableCollection<string> allExerciseTags = new();
        public ObservableCollection<string> AllExerciseTags { get { ReadAllExerciseTags(); return allExerciseTags; } }

        public TagsTable() 
        { 

        }

        public void AddSingleTag(string tag)
        {
            if (!AllExerciseTags.Contains(tag))
            {
                SQL_Commands.ExecuteSQLString(DatabaseConnection.SQLite_conn, SQL_Strings.InsertData(Config.TagsTableName, [$"'{tag}'"]), SQL_Commands.CommandType.NonQuery);
                ReadAllExerciseTags();
            }
        }

        public void AddMultipleTags(List<string> tags)
        {
            foreach (var tag in tags)
                if (!AllExerciseTags.Contains(tag))
                    SQL_Commands.ExecuteSQLString(DatabaseConnection.SQLite_conn, SQL_Strings.InsertData(Config.TagsTableName, [$"'{tag}'"]), SQL_Commands.CommandType.NonQuery);

            ReadAllExerciseTags();
        }

        public void RemoveSingleTag(string tag)
        {
            SQL_Commands.ExecuteSQLString(DatabaseConnection.SQLite_conn, SQL_Strings.DeleteFromTable(Config.TagsTableName, $"Tags='{tag}'"), SQL_Commands.CommandType.NonQuery);
            ReadAllExerciseTags();
        }

        public void RemoveMultipleTags(List<string> tags)
        {
            throw new NotImplementedException();
        }

        private void ReadAllExerciseTags()
        {
            var sqlite_datareader = SQL_Commands.ExecuteSQLString(DatabaseConnection.SQLite_conn, SQL_Strings.ReadData(Config.TagsTableName, "*", true), SQL_Commands.CommandType.Reader) as SQLiteDataReader;

            while (sqlite_datareader != null && sqlite_datareader.Read())
            {
                string tag = sqlite_datareader.GetString(0);
                if (!allExerciseTags.Contains(tag))
                    allExerciseTags.Add(tag);
            }
        }
    }
}
