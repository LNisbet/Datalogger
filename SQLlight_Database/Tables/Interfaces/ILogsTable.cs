using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLight_Database.Tables.Interfaces
{
    public interface ILogsTable
    {
        ObservableCollection<ExerciseLog> Logs { get; }

        void AddSingleLog(ExerciseLog log);

        void AddMultipleLogs(List<ExerciseLog> logs);

        void RemoveSingleLog(ExerciseLog log);

        void RemoveMultipleLogs(List<ExerciseLog> logs);
    }
}
