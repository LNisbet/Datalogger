using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLogger.Models
{
    public class FailedToAddLogException : Exception
    {
        public FailedToAddLogException() { }

        public FailedToAddLogException(string message) : base(message) { }
    }
}
