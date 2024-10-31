using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data.SQLite;

namespace SQLight_Database
{
    internal static class SQL_Database
    {
        
        //private static SQLiteTransaction? sqlite_transaction; Find out what this is for

        internal static void InititiliseDatabase(SQLiteConnection conn)
        {
            CreateTable(Config.TagsTableName, Config.TagsTableDescription, conn); //Tags Table
            TagsTable.AddMultipleTags(Config.StandardTags);
            CreateTable(Config.ExercieseTableName, Config.ExerciseTableDescription, conn); //Exercise Table
            ExerciseTable.AddMultipleExercises(Config.StandardExercises);
            CreateTable(Config.LogsTableName, Config.LogTableDescription, conn); //Log Table
        }

        internal static void DeleteDatabase(string dbName, SQLiteConnection conn)
        {
            SQL_Commands.ExecuteSQLString(conn, SQL_Strings.DeleteDatabase(dbName), SQL_Commands.CommandType.NonQuery);
            DatabaseConnection.CloseConnection();
            if (File.Exists(dbName))
            {
                File.Delete(dbName);
            }
        }

        private static void CreateTable(string tableName, List<ColumnDescription> tableDescription, SQLiteConnection conn)
        {
            SQL_Commands.ExecuteSQLString(conn, SQL_Strings.CreateTable(tableName, tableDescription), SQL_Commands.CommandType.NonQuery);
        }
    }
}
