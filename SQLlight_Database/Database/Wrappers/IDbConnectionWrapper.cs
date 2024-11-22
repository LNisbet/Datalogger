using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLight_Database.Database.Wrappers
{
    public interface IDbConnectionWrapper
    {
        void Open();
        void Close();
        IDbCommand CreateCommand();
    }
}
