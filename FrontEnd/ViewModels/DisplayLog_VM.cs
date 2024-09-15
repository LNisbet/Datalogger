using DataLogger.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLogger.ViewModels
{
    class DisplayLog_VM
    {
        public ObservableCollection<ExerciseLog> ExerciseLogs { get => Database.ExerciseLogs; set => value = Database.ExerciseLogs; }
    }
}
