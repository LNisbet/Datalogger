using DataLogger.Views;
using System.Windows;
using System.Globalization;
using DataLogger.ViewModels;
using DataLogger.ViewModels.Interfaces;
using DataLogger.ViewModels.HelperClasses;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics;

namespace DataLogger
{
    public partial class App : Application
    {
        private readonly IServiceProvider _serviceProvider;

        public App()
        {
            var services = new ServiceCollection();

            // Register ViewModels
            services.AddSingleton<MainWindow_VM>();
            services.AddSingleton<Home_VM>();
            services.AddSingleton<Logging_VM>();
            services.AddSingleton<CreateExercise_VM>();
            services.AddSingleton<CSV_VM>();
            services.AddSingleton<BasicStatistics_VM>();
            services.AddSingleton<FingerStatistics_VM>();
            services.AddSingleton<Charting_VM>();
            services.AddSingleton<Debug_VM>();
            services.AddSingleton<NavigationBar_VM>();

            // Register Stores
            services.AddSingleton<NavigationStore>();

            // Register Views with dependency injection
            services.AddSingleton<MainWindow_V>(s => new MainWindow_V
            {
                DataContext = s.GetRequiredService<MainWindow_VM>()
            });

            // Register NavigationService with callback to set CurrentViewModel
            services.AddSingleton<INavigationService>(s =>
            {
                var navigationStore = s.GetRequiredService<NavigationStore>();
                return new NavigationService(s, vm => navigationStore.CurrentViewModel = vm);
            });

            _serviceProvider = services.BuildServiceProvider();
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
