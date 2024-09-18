using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLogger.Models
{
    public static class Model
    {
        public static IDatabase InternalDatabase = new InternalDatabase();
    }
}
