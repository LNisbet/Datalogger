using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLight_Database
{
    public class NumberWithUnit
    {
        public float Number { get; set; }
        public Enums.Units Unit { get; set; }

        public NumberWithUnit(float number, Enums.Units units = Enums.Units.None) 
        {
            Number = number;
            Unit = units;
        }

        public NumberWithUnit(string numberWithUnitString)
        {
            var words = numberWithUnitString.Split(' ', StringSplitOptions.TrimEntries);
            Number = float.Parse(words[0]);
            Enum.TryParse(words[1], out Enums.Units unit);
            Unit = unit;
        }

        public override string ToString()
        {
            return $"{Number} {Unit}";
        }
    }
}
