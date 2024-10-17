using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLight_Database
{
    public static class LogsTable
    {
        private static ObservableCollection<ExerciseLog>? logs = null;
        public static ObservableCollection<ExerciseLog> Logs
        {
            get
            {
                if (logs == null)
                {
                    logs = [];
                    ReadAllLogs();
                }
                return logs;
            }
        }

        public static void AddSingleLog(ExerciseLog log)
        {
            if (IsLogUnique(log))
            {
                SQL_Commands.ExecuteSQLString(DatabaseConnection.SQLite_conn, SQL_Strings.InsertData(Config.LogsTableName, log.ToSQLStringList()), Enums.CommandType.NonQuery);
                ReadAllLogs();
            }
        }

        public static void AddMultipleLogs(List<ExerciseLog> logs)
        {
            foreach (var log in logs) if (IsLogUnique(log))
                    SQL_Commands.ExecuteSQLString(DatabaseConnection.SQLite_conn, SQL_Strings.InsertData(Config.LogsTableName, log.ToSQLStringList()), Enums.CommandType.NonQuery);
            ReadAllLogs();
        }

        public static void RemoveSingleLog(ExerciseLog log)
        {
            SQL_Commands.ExecuteSQLString(DatabaseConnection.SQLite_conn, SQL_Strings.DeleteFromTable(Config.LogsTableName, $"Id={log.Id}"), Enums.CommandType.NonQuery);
            ReadAllLogs();
        }

        public static void ReadAllLogs()
        {
            var sqlite_datareader = SQL_Commands.ExecuteSQLString(DatabaseConnection.SQLite_conn, SQL_Strings.ReadData(Config.LogsTableName, "*", false), Enums.CommandType.Reader) as SQLiteDataReader;
            logs.Clear();

            while (sqlite_datareader != null && sqlite_datareader.Read())
            {
                logs.Add(new ExerciseLog(sqlite_datareader));
            }
        }

        private static bool IsLogUnique(ExerciseLog log)
        {
            return !(Logs.Any(l => l.Date.Equals(log.Date)) &&
                Logs.Any(l => l.Exercise.Name.Equals(log.Exercise.Name)) &&
                Logs.Any(l => l.Value1.Equals(log.Value1)) &&
                Logs.Any(l => l.Value2.Equals(log.Value2)) &&
                Logs.Any(l => l.Value3.Equals(log.Value3)) &&
                Logs.Any(l => l.Value4.Equals(log.Value4)));
        }
    }
}
