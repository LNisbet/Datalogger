using SQLight_Database.Tables.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLight_Database
{
    public class LogsTable : ILogsTable
    {
        private ObservableCollection<ExerciseLog>? logs = null;
        public ObservableCollection<ExerciseLog> Logs
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

        public LogsTable()
        {

        }

        public void AddSingleLog(ExerciseLog log)
        {
            if (IsLogUnique(log))
            {
                SQL_Commands.ExecuteSQLString(DatabaseConnection.SQLite_conn, SQL_Strings.InsertData(Config.LogsTableName, log.ToSQLStringList()), SQL_Commands.CommandType.NonQuery);
                ReadAllLogs();
            }
        }

        public void AddMultipleLogs(List<ExerciseLog> logs)
        {
            foreach (var log in logs) if (IsLogUnique(log))
                    SQL_Commands.ExecuteSQLString(DatabaseConnection.SQLite_conn, SQL_Strings.InsertData(Config.LogsTableName, log.ToSQLStringList()), SQL_Commands.CommandType.NonQuery);
            ReadAllLogs();
        }

        public void RemoveSingleLog(ExerciseLog log)
        {
            SQL_Commands.ExecuteSQLString(DatabaseConnection.SQLite_conn, SQL_Strings.DeleteFromTable(Config.LogsTableName, $"Id={log.Id}"), SQL_Commands.CommandType.NonQuery);
            ReadAllLogs();
        }

        public void RemoveMultipleLogs(List<ExerciseLog> logs)
        {
            throw new NotImplementedException();
        }

        private void ReadAllLogs()
        {
            var sqlite_datareader = SQL_Commands.ExecuteSQLString(DatabaseConnection.SQLite_conn, SQL_Strings.ReadData(Config.LogsTableName, "*", false), SQL_Commands.CommandType.Reader) as SQLiteDataReader;
            logs.Clear();

            while (sqlite_datareader != null && sqlite_datareader.Read())
            {
                logs.Add(new ExerciseLog(sqlite_datareader));
            }
        }

        private bool IsLogUnique(ExerciseLog log)
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
