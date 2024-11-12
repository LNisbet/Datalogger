﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
