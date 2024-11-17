using SQLight_Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLight_Database.Config.Interface
{
    public interface ITableConfig<T>
    {
        public string Name { get; }
        public List<ColumnDescription> Description { get; }
        public List<T>? DefaultValues { get; }
    }
}
