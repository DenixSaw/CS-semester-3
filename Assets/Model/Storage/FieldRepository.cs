#nullable enable
using System.Collections.Generic;
using System.IO;
using Model.Entities;
using Newtonsoft.Json;

namespace Model.Storage {
public abstract class FieldRepository : IRepository<List<IBrick?>> {
    public static List<IBrick?> Get() {
        if (!File.Exists("./field.json")) {
            File.WriteAllText("./field.json", "");
        }
        var json = File.ReadAllText("./field.json");
        var field = JsonConvert.DeserializeObject<List<IBrick?>>(json, new JsonSerializerSettings {
            TypeNameHandling = TypeNameHandling.All
        });
        return field ?? new List<IBrick?>();
    }

    public static void Set(List<IBrick?> newItem) {
        var json = JsonConvert.SerializeObject(newItem, new JsonSerializerSettings {
            TypeNameHandling = TypeNameHandling.All
        });
        File.WriteAllText("./field.json", json);
    }
}
}
