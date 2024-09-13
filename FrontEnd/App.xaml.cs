using System.ComponentModel;
using System.Windows;
using DataLogger.Models;
using DataLogger.ViewModels;

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
            MainView view = new MainView();
            MainViewModel VM = new MainViewModel();
            view.DataContext = VM;
            view.Show();
        }
    }
}
