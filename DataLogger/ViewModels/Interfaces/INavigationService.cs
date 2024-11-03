using DataLogger.ViewModels.HelperClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLogger.ViewModels.Interfaces
{
    public interface INavigationService
    {
        void NavigateTo<TViewModel>() where TViewModel : Base_VM;
        void NavigateTo<TViewModel>(object parameter) where TViewModel : Base_VM;
    }

}
