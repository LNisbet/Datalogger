using SQLight_Database.Models;
using System.Collections.ObjectModel;

namespace SQLight_Database.Tables.Interfaces
{
    public interface IUsersTable
    {
        ObservableCollection<User> AllUsers { get; }

        ObservableCollection<string> AllUserNames { get; }

        User? SelectUserByName(string? name);

        void Add(User user);

        void Remove(User user);

        void Modify(User user);
    }
}
