using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLight_Database
{
    public static class Statistics
    {
        public static ExerciseLog? MostRecent(Exercise exercise)
        {
            var sql_string = SQL_Strings.SelectData(Config.LogsTableName, "Date", Enums.SelectDataOptions.MAX, $"exercise={exercise}");

            if (SQL_Commands.ExecuteSQLString(DatabaseConnection.SQLite_conn, sql_string, Enums.CommandType.Reader) is not SQLiteDataReader sqlite_datareader)
                return null;

            return new ExerciseLog(sqlite_datareader);
        }

        public static ExerciseLog? Max(Exercise exercise, DateOnly startDate, DateOnly endDate)
        {
            var sql_string = SQL_Strings.SelectData(Config.LogsTableName, "value1", Enums.SelectDataOptions.MAX, $"exercise={exercise} AND Date >={startDate} AND <={endDate}");

            if (SQL_Commands.ExecuteSQLString(DatabaseConnection.SQLite_conn, sql_string, Enums.CommandType.Reader) is not SQLiteDataReader sqlite_datareader)
                return null;

            return new ExerciseLog(sqlite_datareader);
        }

        public static ExerciseLog? Min(Exercise exercise, DateOnly startDate, DateOnly endDate)
        {
            var sql_string = SQL_Strings.SelectData(Config.LogsTableName, "value1", Enums.SelectDataOptions.MIN, $"exercise={exercise} AND Date >={startDate} AND <={endDate}");

            if (SQL_Commands.ExecuteSQLString(DatabaseConnection.SQLite_conn, sql_string, Enums.CommandType.Reader) is not SQLiteDataReader sqlite_datareader)
                return null;

            return new ExerciseLog(sqlite_datareader);
        }

        public static ExerciseLog? Average(Exercise exercise, DateOnly startDate, DateOnly endDate)
        {
            var sql_string = SQL_Strings.SelectData(Config.LogsTableName, "value1", Enums.SelectDataOptions.AVG, $"exercise={exercise} AND Date >={startDate} AND <={endDate}");

            if (SQL_Commands.ExecuteSQLString(DatabaseConnection.SQLite_conn, sql_string, Enums.CommandType.Reader) is not SQLiteDataReader sqlite_datareader)
                return null;

            return new ExerciseLog(sqlite_datareader);
        }
    }
}
