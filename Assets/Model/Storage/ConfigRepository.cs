using System.IO;
using Newtonsoft.Json;

namespace Model.Storage {
public abstract class ConfigRepository : IRepository<IConfig> {
    public static IConfig Get() {
        var json = File.ReadAllText("./config.json");
        var config = JsonConvert.DeserializeObject<Config>(json);
        return config;
    }

    public static void Set(IConfig newItem) {
        throw new System.NotImplementedException();
    }
}
}