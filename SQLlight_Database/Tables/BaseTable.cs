using Newtonsoft.Json.Linq;
using SQLight_Database.Config;
using SQLight_Database.Config.Interface;
using SQLight_Database.Database;
using SQLight_Database.HelperMethods;
using SQLight_Database.Models;
using SQLight_Database.Tables.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLight_Database.Tables
{
    public abstract class BaseTable<T> : ITable<T>
    {
        private protected readonly DatabaseConnectionStore _databaseConnectionStore;
        private protected readonly ITableConfig<T> _config;

        private protected ObservableCollection<T>? values = null;
        public virtual ObservableCollection<T> Values
        {
            get
            {
                if (values == null)
                {
                    values = [];
                    ReadAllRows();
                }
                return values;
            }
        }

        public abstract ObservableCollection<string> AllNames { get; }

        public BaseTable(DatabaseConnectionStore databaseConnectionStore, ITableConfig<T> config)
        {
            _databaseConnectionStore = databaseConnectionStore;
            _config = config;
        }

        public abstract void Initilise();

        public abstract T SelectByName(string name);

        public abstract void AddSingleRow(T log);

        public abstract void AddMultipleRows(List<T> logs);

        public abstract void RemoveSingleRow(T log);

        public abstract void RemoveMultipleRows(List<T> logs);

        private protected void CreateTable(string tableName, List<ColumnDescription> tableDescription)
        {
            SQL_Commands.ExecuteSQLString(_databaseConnectionStore.SQLite_conn, SQL_Strings.CreateTable(tableName, tableDescription), SQL_Commands.CommandType.NonQuery);
        }
        private protected abstract void ReadAllRows();
    }
}
