using SQLight_Database;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLite_Database
{
    class Program
    {
        static void Main(string[] args)
        {
            Model.InititiliseDatabase("User1");
            Model.AddNewExercise(new Exercise("ex_name1", "ex_type1", "ex_discription1"));
            Model.AddNewExercise(new Exercise("ex_name2", "ex_type2"));
            Model.ReadExerciseData();
            Model.CloseConnection();
        }
    }
}
