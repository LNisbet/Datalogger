using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SQLight_Database.Config;
using SQLight_Database.Database.Interfaces;
using SQLight_Database.Models;
using SQLight_Database.Tables.Interfaces;

namespace SQLight_Database.Tables
{
    public class UsersTable : IUsersTable
    {
        private readonly UserConfig config;

        private ObservableCollection<User>? allUsers;
        public ObservableCollection<User> AllUsers => allUsers ??= Load();

        public ObservableCollection<string> AllUserNames => new(AllUsers.Select(users => users.Name).Distinct());

        public UsersTable(UserConfig userConfig)
        {
            config = userConfig;
        }

        public User? SelectUserByName(string? name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return null;
            return AllUsers.FirstOrDefault(u => u.Name == name);
        }

        public void Add(User user)
        {
            allUsers ??= [];
            if (SelectUserByName(user.Name) == null)
            {
                allUsers.Add(user);
                Save();
            }
        }

        public void Remove(User user)
        {
            var _user = SelectUserByName(user.Name);
            if (allUsers != null && _user != null)
            {
                allUsers.Remove(_user);
                Save();
            }
        }

        public void Modify(User user)
        {
            var _user = SelectUserByName(user.Name);
            if (allUsers != null && _user != null)
            {
                allUsers.Remove(_user);
            }
            Add(user);
            Save();
        }

        internal void Save()
        {
            File.WriteAllText(config.UsersPathName, JsonConvert.SerializeObject(allUsers));
            allUsers = null;
        }

        private ObservableCollection<User> Load()
        {
            try
            {
                return JsonConvert.DeserializeObject<ObservableCollection<User>>(File.ReadAllText(config.UsersPathName)) ?? [];
            }
            catch
            {
                return allUsers ?? [];
            }
        }
    }
}
