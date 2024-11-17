using SQLight_Database.Models;
using System.Collections.ObjectModel;

namespace SQLight_Database.Tables.Interfaces
{
    public interface ITable<T>
    {
        ObservableCollection<T> Values { get; }

        ObservableCollection<string> AllNames { get; }

        void Initilise();

        T SelectByName(string name);

        void AddSingleRow(T row);

        void AddMultipleRows(List<T> rows);

        void RemoveSingleRow(T row);

        void RemoveMultipleRows(List<T> row);

        
    }
}
