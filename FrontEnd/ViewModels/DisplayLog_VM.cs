using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLight_Database;

namespace DataLogger.ViewModels
{
    class DisplayLog_VM
    {
        public ObservableCollection<ExerciseLog> ExerciseLogs { get => Model.InternalDatabase.ExerciseLogs; }
    }
}
