using DataLogger.Views;
using System.Windows;
using System.Globalization;
using DataLogger.ViewModels;
using DataLogger.ViewModels.Interfaces;
using DataLogger.ViewModels.HelperClasses;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics;
using DataLogger.Models;

namespace DataLogger
{
    public partial class App : Application
    {
        private readonly IServiceProvider _serviceProvider;

        public App()
        {
            var serviceCollection = new ServiceCollection();

            // Register ViewModels
            serviceCollection.AddSingleton<MainWindow_VM>();
            serviceCollection.AddSingleton<Home_VM>();
            serviceCollection.AddSingleton<Logging_VM>();
            serviceCollection.AddSingleton<CreateExercise_VM>();
            serviceCollection.AddSingleton<CSV_VM>();
            serviceCollection.AddSingleton<BasicStatistics_VM>();
            serviceCollection.AddSingleton<FingerStatistics_VM>();
            serviceCollection.AddSingleton<Charting_VM>();
            serviceCollection.AddSingleton<Debug_VM>();
            serviceCollection.AddSingleton<NavigationBar_VM>();

            // Register Stores
            serviceCollection.AddSingleton<NavigationStore>();
            serviceCollection.AddSingleton<BasicStatisticsList>();

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

            SQLight_Database.DependencyInjection.ConfigureDependencies(serviceCollection);

            _serviceProvider = serviceCollection.BuildServiceProvider();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            
            Localisation();

            _serviceProvider.GetService<NavigationStore>().CurrentViewModel = _serviceProvider.GetService<Home_VM>();

            var mainWindow = _serviceProvider.GetRequiredService<MainWindow_V>();
            mainWindow.Show();
        }

        private static void Localisation()
        {
            // Set the global culture for the application to use dd/MM/yyyy
            var culture = new CultureInfo("en-GB");  // British English uses dd/MM/yyyy format.

            // Apply this culture to all threads (UI and background)
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;

            FrameworkElement.LanguageProperty.OverrideMetadata
                (
                    typeof(FrameworkElement),
                    new FrameworkPropertyMetadata(System.Windows.Markup.XmlLanguage.GetLanguage(culture.IetfLanguageTag))
                );
        }
    }

}
