using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SQLight_Database
{
    internal class SaveConfig
    {
        internal async static void Save<T>(List<T> values) 
        {
            await using FileStream createStream = File.Create(@"config.json");
            await JsonSerializer.SerializeAsync(createStream, values);
        }

        internal static List<T> Load<T>()
        {
            await using FileStream createStream = File.Create(@"config.json");
            await JsonSerializer.SerializeAsync(createStream, values);
        }
    }
}
