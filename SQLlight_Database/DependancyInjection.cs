using Microsoft.Extensions.DependencyInjection;
using SQLight_Database.Config;
using SQLight_Database.Config.Interface;
using SQLight_Database.Database;
using SQLight_Database.Database.Interfaces;
using SQLight_Database.Models;
using SQLight_Database.Tables;
using SQLight_Database.Tables.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLight_Database
{
    public class DependencyInjection
    {
        public static void ConfigureDependencies(ServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<IDatabaseConnectionService, DatabaseConnectionService>();
            serviceCollection.AddSingleton<DatabaseConnectionStore>();
            serviceCollection.AddSingleton<ISQL_Database, SQL_Database>();

            serviceCollection.AddSingleton<ITableConfig<Exercise>>();
            serviceCollection.AddSingleton<ITableConfig<ExerciseLog>>();
            serviceCollection.AddSingleton<ITableConfig<string>>();
            serviceCollection.AddSingleton<UserConfig>();

            serviceCollection.AddSingleton<ITable<Exercise>>();
            serviceCollection.AddSingleton<ITable<ExerciseLog>>();
            serviceCollection.AddSingleton<ITable<string>>();
            serviceCollection.AddSingleton<IUsersTable>();
        }
    }
}
