using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SQLight_Database
{
    public static class Users
    {
        private static ObservableCollection<User>? allUsers;
        public static ObservableCollection<User> AllUsers => allUsers ??= Load();

        public static ObservableCollection<string> AllUserNames => new(AllUsers.Select(users => users.Name).Distinct());

        public static User? SelectUserByName(string? name)
        {
            if (string.IsNullOrWhiteSpace(name)) 
                return null;
            return AllUsers.FirstOrDefault(u => u.Name == name);
        }

        public static void Add(User user)
        {
            allUsers ??= [];
            if (allUsers.FirstOrDefault(u => u.Name == user.Name) == null)
            {
                allUsers.Add(user);
                Save();
            }
        }

        public static void Remove(User user)
        {
            if (allUsers != null && allUsers.FirstOrDefault(u => u.Name == user.Name) != null)
            {   
                if (user.Initilised == true)
                    SQL_Database.DeleteDatabase(user.Name,DatabaseConnection.SQLite_conn);
                allUsers.Remove(allUsers.FirstOrDefault(u => u.Name == user.Name));
                Save();
            }  
        }

        public static void Modify(User user)
        {
            if (allUsers != null && allUsers.FirstOrDefault(u => u.Name == user.Name) != null)
            {
                allUsers.Remove(allUsers.FirstOrDefault(u => u.Name == user.Name));
            }
            Add(user);
            Save();
        }

        internal static void Save()
        {
            File.WriteAllText(Config.UsersPathName, JsonConvert.SerializeObject(allUsers));
            allUsers = null;
        }

        private static ObservableCollection<User> Load()
        {
            try
            {
                return JsonConvert.DeserializeObject<ObservableCollection<User>>(File.ReadAllText(Config.UsersPathName)) ?? [];
            }
            catch
            {
                return allUsers ?? [];
            }
        }
    }
}
