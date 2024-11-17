
using SQLight_Database.Models;

namespace SQLight_Database.Database.Interfaces
{
    public interface IDatabaseConnectionService
    {
        void Open(User user);
        void Close();
    }
}
