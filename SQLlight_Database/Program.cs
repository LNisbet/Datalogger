using SQLight_Database;

namespace SQLite_Database
{
    class Program
    {
        static void Main(string[] args)
        {
            SQL_Database.InititiliseDatabase("User1");
            //SQL_Database.AddSingleExercise(new Exercise("ex_name1", "ex_type1", "ex_discription1"));
            //SQL_Database.AddSingleExercise(new Exercise("ex_name2", "ex_type2"));
            SQL_Database.ReadAllExercises();

            foreach (var thing in SQL_Database.Exercises) {Console.WriteLine($"{thing.Name} {thing.Type} {thing.Description}");}

            DateOnly date = DateOnly.FromDateTime(DateTime.Now);
            //SQL_Database.AddSingleLog(new ExerciseLog(date, new Exercise("ex_name1", "ex type1", "ex discription1"), 11f));
            //SQL_Database.AddSingleLog(new ExerciseLog(date, new Exercise("ex_name2", "ex type2"), 11f,"note about something 2"));
            SQL_Database.ReadAllLogs();

            foreach (var thing in SQL_Database.Logs) { Console.WriteLine($"{thing.Date} {thing.Exercise} {thing.Value1} {thing.Note}"); }

            SQL_Database.CloseConnection();
            Console.ReadLine();
        }
    }
}
