using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Atamas_OOP_Project.Interfaces;
using System.IO;
using System.Text.Json;

namespace Atamas_OOP_Project.Services
{
    public class LocalDataManager : IDataManager
    {
        public string JsonFilePath { get; set; }

        public void Save(object data)
        {
            var json = JsonSerializer.Serialize(data);
            File.WriteAllText(JsonFilePath, json);
        }

        public object Load()
        {
            var json = File.ReadAllText(JsonFilePath);
            return JsonSerializer.Deserialize<object>(json); // потрібно уточнити тип
        }
    }
}
