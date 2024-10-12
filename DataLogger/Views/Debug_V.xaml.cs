using DataLogger.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DataLogger.Views
{
    /// <summary>
    /// Interaction logic for Debug_V.xaml
    /// </summary>
    public partial class Debug_V : Page
    {
        private object? dataContext = null;
        public Debug_V()
        {
            InitializeComponent();
            //dataContext ??= new BasicStatistics_VM();
            DataContext = dataContext;
        }
    }
}
