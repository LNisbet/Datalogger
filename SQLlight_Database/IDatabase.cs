using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLight_Database
{
    public interface IDatabase
    {
        List<string> columnNames { get; }

        void CreateDatabase();
        void DeleteDatabase();
        void AddColumn(string columnName, string variableType);
        void DeleteColumn(string columnName);
        void AddRow(List<string> values);
        void DeleteRow(int id);
        List<string> GetRowById(int id);
        List<List<string>> GetRowsByDataMatch(string columnName, string value);
    }
}
