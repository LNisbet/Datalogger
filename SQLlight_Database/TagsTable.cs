using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLight_Database
{
    public class TagsTable
    {
        private static ObservableCollection<string> allExerciseTags = new();
        public static ObservableCollection<string> AllExerciseTags { get { ReadAllExerciseTags(); return allExerciseTags; } }

        internal static void AddSingleTag(string tag)
        {
            if (!AllExerciseTags.Contains(tag))
            {
                SQL_Commands.ExecuteSQLString(DatabaseConnection.SQLite_conn, SQL_Strings.InsertData(Config.TagsTableName, [$"'{tag}'"]), Enums.CommandType.NonQuery);
                ReadAllExerciseTags();
            }
        }

        internal static void AddMultipleTags(List<string> tags)
        {
            foreach (var tag in tags)
                if (!AllExerciseTags.Contains(tag))
                    SQL_Commands.ExecuteSQLString(DatabaseConnection.SQLite_conn, SQL_Strings.InsertData(Config.TagsTableName, [$"'{tag}'"]), Enums.CommandType.NonQuery);

            ReadAllExerciseTags();
        }

        public static void RemoveSingleTag(string tag)
        {
            SQL_Commands.ExecuteSQLString(DatabaseConnection.SQLite_conn, SQL_Strings.DeleteFromTable(Config.TagsTableName, $"Tags='{tag}'"), Enums.CommandType.NonQuery);
            ReadAllExerciseTags();
        }

        internal static void ReadAllExerciseTags()
        {
            var sqlite_datareader = SQL_Commands.ExecuteSQLString(DatabaseConnection.SQLite_conn, SQL_Strings.ReadData(Config.TagsTableName, "*", true), Enums.CommandType.Reader) as SQLiteDataReader;

            while (sqlite_datareader != null && sqlite_datareader.Read())
            {
                string tag = sqlite_datareader.GetString(0);
                if (!allExerciseTags.Contains(tag))
                    allExerciseTags.Add(tag);
            }
        }
    }
}
