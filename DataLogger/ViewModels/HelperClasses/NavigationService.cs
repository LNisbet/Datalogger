using DataLogger.ViewModels.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace DataLogger.ViewModels.HelperClasses
{
    public class NavigationService : INavigationService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly Action<Base_VM> _setViewModelAction;

        public NavigationService(IServiceProvider serviceProvider, Action<Base_VM> setViewModelAction)
        {
            _serviceProvider = serviceProvider;
            _setViewModelAction = setViewModelAction;
        }

        public void NavigateTo<TViewModel>() where TViewModel : Base_VM
        {
            var viewModel = _serviceProvider.GetRequiredService<TViewModel>();
            _setViewModelAction(viewModel);
        }

        public void NavigateTo<TViewModel>(object parameter) where TViewModel : Base_VM
        {
            var viewModel = _serviceProvider.GetRequiredService<TViewModel>();
            if (viewModel is IParameterReceiver parameterReceiver)
            {
                parameterReceiver.ReceiveParameter(parameter);
            }
            _setViewModelAction(viewModel);
        }
    }

}
