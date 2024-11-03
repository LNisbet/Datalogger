using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLogger.ViewModels.Interfaces
{
    public interface IParameterReceiver
    {
        void ReceiveParameter(object parameter);
    }

}
