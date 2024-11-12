using SQLight_Database.Database.Interfaces;
using SQLight_Database.Tables.Interfaces;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data.SQLite;
using System.Linq;

namespace SQLight_Database
{
    public class SQL_Database : ISQL_Database
    {
        private readonly IDatabaseConnection _databaseConnection;

        public SQL_Database(UsersTable usersTable, IDatabaseConnection databaseConnection, ITagsTable tagsTable, IExerciseTable exerciseTable) 
        {
            _databaseConnection = databaseConnection;
            InititiliseDatabase(tagsTable, exerciseTable);
            databaseConnection.CurrentUser.Initilised = true;

            if (!usersTable.AllUserNames.Contains(databaseConnection.CurrentUser.Name))
            {
                usersTable.Add(databaseConnection.CurrentUser);
            }
            else if (!usersTable.AllUsers.FirstOrDefault(u => u.Name == databaseConnection.CurrentUser.Name).Initilised)
            {
                usersTable.Modify(databaseConnection.CurrentUser);
            }
        }

        public void DeleteDatabase()
        {
            SQL_Commands.ExecuteSQLString(_databaseConnection.SQLite_conn, SQL_Strings.DeleteDatabase(_databaseConnection.CurrentUser.Name), SQL_Commands.CommandType.NonQuery);
            _databaseConnection.Close();

            if (File.Exists($"{_databaseConnection.CurrentUser.Name}.db"))
            {
                File.Delete($"{_databaseConnection.CurrentUser.Name}.db");
            }
        }

        private void InititiliseDatabase(ITagsTable tagsTable, IExerciseTable exerciseTable)
        {
            CreateTable(Config.TagsTableName, Config.TagsTableDescription); //Tags Table
            tagsTable.AddMultipleTags(Config.StandardTags);
            CreateTable(Config.ExercieseTableName, Config.ExerciseTableDescription); //Exercise Table
            exerciseTable.AddMultipleExercises(Config.StandardExercises);
            CreateTable(Config.LogsTableName, Config.LogTableDescription); //Log Table
        }

        private void CreateTable(string tableName, List<ColumnDescription> tableDescription)
        {
            SQL_Commands.ExecuteSQLString(_databaseConnection.SQLite_conn, SQL_Strings.CreateTable(tableName, tableDescription), SQL_Commands.CommandType.NonQuery);
        }
    }
}
