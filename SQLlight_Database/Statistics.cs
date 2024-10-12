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
            var sql_string = SQL_Strings.SelectData(Config.LogsTableName, "*", "Date", Enums.SelectDataOptions.MAX, $"Exercise='{exercise.Name}'");

            if (SQL_Commands.ExecuteSQLString(DatabaseConnection.SQLite_conn, sql_string, Enums.CommandType.Reader) is not SQLiteDataReader sqlite_datareader)
                return null;

            if (!sqlite_datareader.HasRows || !sqlite_datareader.Read() || sqlite_datareader.IsDBNull(0))
            {
                sqlite_datareader.Close();
                return null;
            }

            var log = new ExerciseLog(sqlite_datareader);
            sqlite_datareader.Close();
            return log;
        }

        public static ExerciseLog? Max(Exercise exercise, DateOnly startDate, DateOnly endDate)
        {
            var sql_string = SQL_Strings.SelectData(Config.LogsTableName, "*", "value1", Enums.SelectDataOptions.MAX, $"Exercise='{exercise.Name}' AND Date >={startDate} AND Date <={endDate}");

            if (SQL_Commands.ExecuteSQLString(DatabaseConnection.SQLite_conn, sql_string, Enums.CommandType.Reader) is not SQLiteDataReader sqlite_datareader)
                return null;

            if (!sqlite_datareader.HasRows || !sqlite_datareader.Read() || sqlite_datareader.IsDBNull(0))
            {
                sqlite_datareader.Close();
                return null;
            }

            var log = new ExerciseLog(sqlite_datareader);
            sqlite_datareader.Close();
            return log;
        }

        public static ExerciseLog? Min(Exercise exercise, DateOnly startDate, DateOnly endDate)
        {
            var sql_string = SQL_Strings.SelectData(Config.LogsTableName, "*", "value1", Enums.SelectDataOptions.MIN, $"Exercise='{exercise.Name}' AND Date >={startDate} AND Date <={endDate}");

            if (SQL_Commands.ExecuteSQLString(DatabaseConnection.SQLite_conn, sql_string, Enums.CommandType.Reader) is not SQLiteDataReader sqlite_datareader)
                return null;

            if (!sqlite_datareader.HasRows || !sqlite_datareader.Read() || sqlite_datareader.IsDBNull(0))
            {
                sqlite_datareader.Close();
                return null;
            }

            var log = new ExerciseLog(sqlite_datareader);
            sqlite_datareader.Close();
            return log;
        }

        public static ExerciseLog? Average(Exercise exercise, DateOnly startDate, DateOnly endDate)
        {
            var sql_string = SQL_Strings.SelectData(Config.LogsTableName, "*", "value1", Enums.SelectDataOptions.AVG, $"Exercise='{exercise.Name}' AND Date >={startDate} AND Date <={endDate}");

            if (SQL_Commands.ExecuteSQLString(DatabaseConnection.SQLite_conn, sql_string, Enums.CommandType.Reader) is not SQLiteDataReader sqlite_datareader)
                return null;

            if (!sqlite_datareader.HasRows || !sqlite_datareader.Read() || sqlite_datareader.IsDBNull(0))
            {
                sqlite_datareader.Close();
                return null;
            }

            var log = new ExerciseLog(sqlite_datareader);
            sqlite_datareader.Close();
            return log;
        }
    }
}
