using Microsoft.Extensions.DependencyInjection;
using SQLight_Database.Config;
using SQLight_Database.Config.Interface;
using SQLight_Database.Database;
using SQLight_Database.Database.Interfaces;
using SQLight_Database.Models;
using SQLight_Database.Tables;
using SQLight_Database.Tables.Interfaces;

namespace SQLight_Database
{
    public class DependencyInjection
    {
        public static void ConfigureDependencies(ServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<IDatabaseConnectionService, DatabaseConnectionService>();
            serviceCollection.AddSingleton<DatabaseConnectionStore>();
            serviceCollection.AddSingleton<ISQL_Database, SQL_Database>();

            serviceCollection.AddSingleton<ITableConfig<Exercise>, ExerciseTableConfig>();
            serviceCollection.AddSingleton<ITableConfig<ExerciseLog>, LogsTableConfig>();
            serviceCollection.AddSingleton<ITableConfig<string>, TagTableConfig>();
            serviceCollection.AddSingleton<UserConfig>();

            serviceCollection.AddSingleton<ITable<Exercise>, ExerciseTable>();
            serviceCollection.AddSingleton<ITable<ExerciseLog>, LogsTable>();
            serviceCollection.AddSingleton<ITable<string>, TagsTable>();
            serviceCollection.AddSingleton<IUsersTable, UsersTable>();
        }
    }
}
