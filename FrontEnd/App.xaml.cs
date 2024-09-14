using DataLogger.Views;
using System.Windows;

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
            var view = new MainWindow_V();
            view.Show();
        }
    }
}
