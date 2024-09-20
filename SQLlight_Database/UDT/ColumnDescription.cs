using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLight_Database
{
    public class ColumnDescription
    {
        public string Name { get; set; }
        public string DataType { get; set; }

        public string? Constraints { get; set; }

        /*  NOT NULL - Ensures that a column cannot have a NULL value
         *  UNIQUE - Ensures that all values in a column are different
         *  PRIMARY KEY - A combination of a NOT NULL and UNIQUE. Uniquely identifies each row in a table
         *  FOREIGN KEY - Prevents actions that would destroy links between tables
         *  CHECK - Ensures that the values in a column satisfies a specific condition
         *  DEFAULT - Sets a default value for a column if no value is specified
         *  CREATE INDEX - Used to create and retrieve data from the database very quickly
         */

        public ColumnDescription(string name, string dataType)
        {
            Name = name;
            DataType = dataType;
        }
        public ColumnDescription(string name, string dataType, string constraints)
        {
            Name = name;
            DataType = dataType;
            Constraints = constraints;
        }
    }
}
