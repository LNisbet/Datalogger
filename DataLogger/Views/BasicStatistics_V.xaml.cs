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
    public partial class BasicStatistics_V : Page
    {
        private object? dataContext = null;
        public BasicStatistics_V()
        {
            InitializeComponent();
            dataContext ??= new BasicStatistics_VM();
            DataContext = dataContext;
        }
    }
}
