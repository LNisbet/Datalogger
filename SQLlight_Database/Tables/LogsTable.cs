using SQLight_Database.Config;
using SQLight_Database.Config.Interface;
using SQLight_Database.Database;
using SQLight_Database.HelperMethods;
using SQLight_Database.Models;
using SQLight_Database.Tables.Interfaces;
using System.Collections.ObjectModel;
using System.Data.SQLite;

namespace SQLight_Database.Tables
{
    public class LogsTable : BaseTable<ExerciseLog>
    {
        private readonly ITable<Exercise> _exerciseTable;

        public override ObservableCollection<string> AllNames => throw new NotImplementedException();

        public LogsTable(DatabaseConnectionStore databaseConnectionStore, ITableConfig<ExerciseLog> config, ITable<Exercise> exerciseTable) : base(databaseConnectionStore, config)
        {
            _exerciseTable = exerciseTable;
        }

        public override void Initilise()
        {
            if (String.IsNullOrEmpty(_databaseConnectionStore.CurrentUser?.Name) || _databaseConnectionStore.CurrentUser.LogsTableInitilised == true)
                return;

            CreateTable(_config.Name, _config.Description);
            if (_config.DefaultValues != null)
                AddMultipleRows(_config.DefaultValues);

            _databaseConnectionStore.CurrentUser.LogsTableInitilised = true;
        }

        public override void AddSingleRow(ExerciseLog log)
        {
            if (IsLogUnique(log))
            {
                SQL_Commands.ExecuteSQLString(_databaseConnectionStore.SQLite_conn, SQL_Strings.InsertData(_config.Name, log.ToSQLStringList()), SQL_Commands.CommandType.NonQuery);
                ReadAllRows();
            }
        }

        public override void AddMultipleRows(List<ExerciseLog> logs)
        {
            foreach (var log in logs) if (IsLogUnique(log))
                    SQL_Commands.ExecuteSQLString(_databaseConnectionStore.SQLite_conn, SQL_Strings.InsertData(_config.Name, log.ToSQLStringList()), SQL_Commands.CommandType.NonQuery);
            ReadAllRows();
        }

        public override void RemoveSingleRow(ExerciseLog log)
        {
            SQL_Commands.ExecuteSQLString(_databaseConnectionStore.SQLite_conn, SQL_Strings.DeleteFromTable(_config.Name, $"Id={log.Id}"), SQL_Commands.CommandType.NonQuery);
            ReadAllRows();
        }

        public override void RemoveMultipleRows(List<ExerciseLog> logs)
        {
            throw new NotImplementedException();
        }

        private protected override void ReadAllRows()
        {
            var sqlite_datareader = SQL_Commands.ExecuteSQLString(_databaseConnectionStore.SQLite_conn, SQL_Strings.ReadData(_config.Name, "*", false), SQL_Commands.CommandType.Reader) as SQLiteDataReader;
            values.Clear();

            while (sqlite_datareader != null && sqlite_datareader.Read())
            {
                values.Add(new ExerciseLog(sqlite_datareader, _exerciseTable));
            }
        }

        private bool IsLogUnique(ExerciseLog log)
        {
            return !(Values.Any(l => l.Date.Equals(log.Date)) &&
                Values.Any(l => l.Exercise.Name.Equals(log.Exercise.Name)) &&
                Values.Any(l => l.Value1.Equals(log.Value1)) &&
                Values.Any(l => l.Value2.Equals(log.Value2)) &&
                Values.Any(l => l.Value3.Equals(log.Value3)) &&
                Values.Any(l => l.Value4.Equals(log.Value4)));
        }

        public override ExerciseLog SelectByName(string name)
        {
            throw new NotImplementedException();
        }
    }
}
