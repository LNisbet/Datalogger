using SQLight_Database.Config;
using SQLight_Database.Config.Interface;
using SQLight_Database.Database;
using SQLight_Database.Database.Interfaces;
using SQLight_Database.HelperMethods;
using SQLight_Database.Tables.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration.Internal;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLight_Database.Tables
{
    public class TagsTable : BaseTable<string>
    {
        public override ObservableCollection<string> AllNames => throw new NotImplementedException();

        public TagsTable(DatabaseConnectionStore databaseConnectionStore, ITableConfig<string> tagTableConfig) : base(databaseConnectionStore, tagTableConfig)
        {
        }

        public override void Initilise()
        {
            if (string.IsNullOrEmpty(_databaseConnectionStore.CurrentUser?.Name) || _databaseConnectionStore.CurrentUser.TagsTableInitilised == true)
                return;

            CreateTable(_config.Name, _config.Description);
            if (_config.DefaultValues != null)
                AddMultipleRows(_config.DefaultValues);

            _databaseConnectionStore.CurrentUser.TagsTableInitilised = true;
        }

        public override void AddSingleRow(string tag)
        {
            if (!AllNames.Contains(tag))
            {
                SQL_Commands.ExecuteSQLString(_databaseConnectionStore.SQLite_conn, SQL_Strings.InsertData(_config.Name, [$"'{tag}'"]), SQL_Commands.CommandType.NonQuery);
                ReadAllRows();
            }
        }

        public override void AddMultipleRows(List<string> tags)
        {
            foreach (var tag in tags)
                if (!Values.Contains(tag))
                    SQL_Commands.ExecuteSQLString(_databaseConnectionStore.SQLite_conn, SQL_Strings.InsertData(_config.Name, [$"'{tag}'"]), SQL_Commands.CommandType.NonQuery);

            ReadAllRows();
        }

        public override void RemoveSingleRow(string tag)
        {
            SQL_Commands.ExecuteSQLString(_databaseConnectionStore.SQLite_conn, SQL_Strings.DeleteFromTable(_config.Name, $"Tags='{tag}'"), SQL_Commands.CommandType.NonQuery);
            ReadAllRows();
        }

        public override void RemoveMultipleRows(List<string> tags)
        {
            throw new NotImplementedException();
        }

        private protected override void ReadAllRows()
        {
            var sqlite_datareader = SQL_Commands.ExecuteSQLString(_databaseConnectionStore.SQLite_conn, SQL_Strings.ReadData(_config.Name, "*", true), SQL_Commands.CommandType.Reader) as SQLiteDataReader;

            while (sqlite_datareader != null && sqlite_datareader.Read())
            {
                string tag = sqlite_datareader.GetString(0);
                if (!Values.Contains(tag))
                    Values.Add(tag);
            }
        }

        public override string SelectByName(string name)
        {
            throw new NotImplementedException();
        }
    }
}
