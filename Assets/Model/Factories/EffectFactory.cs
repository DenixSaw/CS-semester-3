using Model.Effects;
using Random = System.Random;

namespace Model.Factories {
public class EffectFactory : IEffectFactory {
    private readonly Random _random = new();
    private object Config;
    public IEffect GetEffect() {
        if (_random.Next(100) > 20) return null;
        return null;
    }

    public EffectFactory(IConfig _config) {
        Config = _config;
    }
}
}