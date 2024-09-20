using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLight_Database
{
    public class ExerciseNotFoundException : Exception
    {
        public ExerciseNotFoundException() { }

        public ExerciseNotFoundException(string message) : base(message) { }
    }
}
