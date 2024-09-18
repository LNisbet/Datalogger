using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLight_Database
{
    public class ColumnDescription(string name, string dataType)
    {
        public string Name { get; set; } = name;
        public string DataType { get; set; } = dataType;
    }
}
