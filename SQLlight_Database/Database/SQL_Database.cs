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
        private readonly DatabaseConnectionStore _databaseConnectionStore;
        private readonly IUsersTable _usersTable;

        public SQL_Database(IUsersTable usersTable, DatabaseConnectionStore databaseConnectionStore, ITagsTable tagsTable, IExerciseTable exerciseTable) 
        {
            _databaseConnectionStore = databaseConnectionStore;
            _usersTable = usersTable;
            InititiliseDatabase(tagsTable, exerciseTable);
            databaseConnectionStore.CurrentUser.Initilised = true;

            if (!usersTable.AllUserNames.Contains(databaseConnectionStore.CurrentUser.Name))
            {
                usersTable.Add(databaseConnectionStore.CurrentUser);
            }
            else if (!usersTable.AllUsers.FirstOrDefault(u => u.Name == databaseConnectionStore.CurrentUser.Name).Initilised)
            {
                usersTable.Modify(databaseConnectionStore.CurrentUser);
            }
        }

        public void DeleteDatabase()
        {
            SQL_Commands.ExecuteSQLString(_databaseConnectionStore.SQLite_conn, SQL_Strings.DeleteDatabase(_databaseConnectionStore.CurrentUser.Name), SQL_Commands.CommandType.NonQuery);
            _usersTable.Remove(_databaseConnectionStore.CurrentUser);

            if (File.Exists($"{_databaseConnectionStore.CurrentUser.Name}.db"))
            {
                File.Delete($"{_databaseConnectionStore.CurrentUser.Name}.db");
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
            SQL_Commands.ExecuteSQLString(_databaseConnectionStore.SQLite_conn, SQL_Strings.CreateTable(tableName, tableDescription), SQL_Commands.CommandType.NonQuery);
        }
    }
}
