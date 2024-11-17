using DataLogger.Models;
using DataLogger.ViewModels.HelperClasses;
using DataLogger.ViewModels.Interfaces;
using DataLogger.ViewModels;
using DataLogger.Views;
using Microsoft.Extensions.DependencyInjection;

namespace DataLogger
{
    public class DependencyInjection
    {
        public static void ConfigureDependencies(ServiceCollection serviceCollection)
        {
            // Register ViewModels
            serviceCollection.AddSingleton<MainWindow_VM>();
            serviceCollection.AddSingleton<Home_VM>();
            serviceCollection.AddSingleton<Logging_VM>();
            serviceCollection.AddSingleton<CreateExercise_VM>();
            serviceCollection.AddSingleton<CSV_VM>();
            serviceCollection.AddSingleton<BasicStatistics_VM>();
            serviceCollection.AddSingleton<HandStatisticsOverview_VM>();
            serviceCollection.AddTransient<HandStatistics_VM>();
            serviceCollection.AddSingleton<Charting_VM>();
            serviceCollection.AddSingleton<Debug_VM>();
            serviceCollection.AddSingleton<NavigationBar_VM>();

            // Register Stores
            serviceCollection.AddSingleton<NavigationStore>();
            serviceCollection.AddSingleton<BasicStatisticsStore>();
            serviceCollection.AddSingleton<FingerStatisticsStore>();

            // Register Views with dependency injection
            serviceCollection.AddSingleton<MainWindow_V>(s => new MainWindow_V
            {
                DataContext = s.GetRequiredService<MainWindow_VM>()
            });

            // Register NavigationService with callback to set CurrentViewModel
            serviceCollection.AddSingleton<INavigationService>(s =>
            {
                var navigationStore = s.GetRequiredService<NavigationStore>();
                return new NavigationService(s, vm => navigationStore.CurrentViewModel = vm);
            });
        }
    }
}
