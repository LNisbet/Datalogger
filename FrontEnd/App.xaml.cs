using System.Configuration;
using System.Data;
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
            DataLogger.MainView view = new MainView();
            MainViewModel VM = new MainViewModel();
            view.DataContext = VM;
            view.Show();
        }
    }

}
