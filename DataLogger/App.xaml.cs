using DataLogger.Views;
using System.Windows;
using System.Globalization;

namespace DataLogger
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            Localisation();
            var view = new MainWindow_V();
            view.Show();
        }
        private void Localisation()
        {
            // Set the global culture for the application to use dd/MM/yyyy
            var culture = new CultureInfo("en-GB");  // British English uses dd/MM/yyyy format.

            // Apply this culture to all threads (UI and background)
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;

            FrameworkElement.LanguageProperty.OverrideMetadata(
                typeof(FrameworkElement),
                new FrameworkPropertyMetadata(
                    System.Windows.Markup.XmlLanguage.GetLanguage(culture.IetfLanguageTag)));
        }
    }
    
}
