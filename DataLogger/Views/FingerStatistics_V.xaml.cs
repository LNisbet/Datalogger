﻿using DataLogger.ViewModels;
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
    /// Interaction logic for FingerStatistics_V.xaml
    /// </summary>
    public partial class FingerStatistics_V : Page
    {
        private object? dataContext = null;
        public FingerStatistics_V()
        {
            InitializeComponent();
            dataContext ??= new FingerStatistics_VM();
            DataContext = dataContext;
        }
    }
}
