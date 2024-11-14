using Microsoft.Extensions.DependencyInjection;
using SQLight_Database.Database.Interfaces;
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

            serviceCollection.AddSingleton<IExerciseTable, ExerciseTable>();
            serviceCollection.AddSingleton<ILogsTable, LogsTable>();
            serviceCollection.AddSingleton<ITagsTable, TagsTable>();
            serviceCollection.AddSingleton<IUsersTable, UsersTable>();
        }
    }
}
