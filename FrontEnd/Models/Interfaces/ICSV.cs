
namespace DataLogger.Models
{
    public interface ICSV
    {
        void WriteToCSV<T>(string path, List<T> list);

        List<T> ReadFromCSV<T>(string path);
    }
}
