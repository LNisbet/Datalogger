using SQLight_Database.Config;
using SQLight_Database.Config.Interface;
using SQLight_Database.Database;
using SQLight_Database.Database.Interfaces;
using SQLight_Database.Exceptions;
using SQLight_Database.HelperMethods;
using SQLight_Database.Models;
using SQLight_Database.Tables;
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
    public class ExerciseTable : BaseTable<Exercise>
    {
        private readonly ITable<string> _tagsTable;

        public override ObservableCollection<string> AllNames  => new(Values.Select(exercise => exercise.Name).Distinct());

        public ExerciseTable(DatabaseConnectionStore databaseConnectionStore, ITableConfig<Exercise> config, ITable<string> tagsTable) : base(databaseConnectionStore, config)
        {
            _tagsTable = tagsTable;
        }

        public override void Initilise()
        {
            if (String.IsNullOrEmpty(_databaseConnectionStore.CurrentUser?.Name) || _databaseConnectionStore.CurrentUser.ExerciseTableInitilised == true)
                return;

            CreateTable(_config.Name, _config.Description);

            if (_config.DefaultValues != null)
                AddMultipleRows(_config.DefaultValues);

            _databaseConnectionStore.CurrentUser.ExerciseTableInitilised = true;
        }

        public override Exercise SelectByName(string name)
        {
            var ex = Values.SingleOrDefault(ex => ex.Name == name);
            return ex ?? throw new ExerciseNotFoundException(name);
        }

        public override void AddSingleRow(Exercise exercise)
        {
            if (!AllNames.Contains(exercise.Name))
            {
                SQL_Commands.ExecuteSQLString(_databaseConnectionStore.SQLite_conn, SQL_Strings.InsertData(_config.Name, exercise.ToSQLStringList()), SQL_Commands.CommandType.NonQuery);
                foreach (var tag in exercise.Tags)
                {
                    if (!_tagsTable.AllNames.Contains(tag))
                        _tagsTable.AddSingleRow(tag);
                }

                ReadAllRows();
            }
        }

        public override void AddMultipleRows(List<Exercise> exercises)
        {
            foreach (var exercise in exercises)
                if (!AllNames.Contains(exercise.Name))
                {
                    SQL_Commands.ExecuteSQLString(_databaseConnectionStore.SQLite_conn, SQL_Strings.InsertData(_config.Name, exercise.ToSQLStringList()), SQL_Commands.CommandType.NonQuery);
                    foreach (var tag in exercise.Tags)
                    {
                        if (!_tagsTable.AllNames.Contains(tag))
                            _tagsTable.AddSingleRow(tag);
                    }
                }
            ReadAllRows();
        }

        public override void RemoveSingleRow(Exercise exercise)
        {
            SQL_Commands.ExecuteSQLString(_databaseConnectionStore.SQLite_conn, SQL_Strings.DeleteFromTable(_config.Name, $"Name='{exercise.Name}'"), SQL_Commands.CommandType.NonQuery);
            ReadAllRows();
        }

        public override void RemoveMultipleRows(List<Exercise> exercises)
        {
            throw new NotImplementedException();
        }

        private protected override void ReadAllRows()
        {
            var sqlite_datareader = SQL_Commands.ExecuteSQLString(_databaseConnectionStore.SQLite_conn, SQL_Strings.ReadData(_config.Name, "*", false), SQL_Commands.CommandType.Reader) as SQLiteDataReader;
            values.Clear();

            while (sqlite_datareader != null && sqlite_datareader.Read())
            {
                values.Add(new Exercise(sqlite_datareader));
            }
        }
    }
}
