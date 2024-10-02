using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLight_Database
{
    public static class Enums
    {

        public enum SelectDataOptions
        {
            MIN,
            MAX,
            COUNT,
            SUM,
            AVG
        }

        public enum Units
        {
            None,
            Kg,
            N,
            mm,
            cm,
            Reps,
            Sets,
            Minutes,
            Seconds
        }

        internal enum CommandType
        {
            NonQuery,
            Reader
        }
    }
}
