using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data.SQLite;

namespace SQLight_Database
{
    public static class SQL_Database
    {
        
        //private static SQLiteTransaction? sqlite_transaction; Find out what this is for

        public static void InititiliseDatabase(string dbName)
        {
            CreateTable(Config.TagsTableName, Config.TagsTableDescription); //Tags Table
            TagsTable.AddMultipleTags(Config.StandardTags);
            CreateTable(Config.ExercieseTableName, Config.ExerciseTableDescription); //Exercise Table
            ExerciseTable.AddMultipleExercises(Config.StandardExercises);
            CreateTable(Config.LogsTableName, Config.LogTableDescription); //Log Table
        }

        public static void DeleteDatabase(string dbName)
        {
            SQL_Commands.ExecuteSQLString(DatabaseConnection.SQLite_conn, SQL_Strings.DeleteDatabase(dbName), Enums.CommandType.NonQuery);
            DatabaseConnection.CloseConnection();
        }

        private static void CreateTable(string tableName, List<ColumnDescription> tableDescription)
        {
            SQL_Commands.ExecuteSQLString(DatabaseConnection.SQLite_conn, SQL_Strings.CreateTable(tableName, tableDescription), Enums.CommandType.NonQuery);
        }
    }
}
