using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLight_Database
{
    public class DataDescription(List<string> columnNames, List<string?> values)
    {
        public List<string> ColumnNames { get; set; } = columnNames;
        public List<string?> Values { get; set; } = values;
    }
}
