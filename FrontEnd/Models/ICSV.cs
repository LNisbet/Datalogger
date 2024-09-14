
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace DataLogger.Models
{
    public interface ICSV
    {
        void WrirteToCSV<T>(string path, List<T> list);

        List<ExerciseLog> ReadLogsFromCSV(string path);

        List<string> ReadExercisesFromCSV(string path);
    }
}
