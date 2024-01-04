using System.Linq;
using Model.Effects;
using UnityEngine;
using Random = System.Random;


namespace Model.Factories {
public class EffectFactory : IEffectFactory {
    private readonly Random _random = new();
    private readonly IConfig _config;
   
    public IEffect GetEffect() {
        
        if (_random.Next(100) > 20) return null;
        var effectChance = _random.Next(100);
        var sortedDict = from entry in _config.Effects orderby entry.Value descending select entry;
        
        foreach (var effect in sortedDict){
            if (effectChance > effect.Value) {
                return effect.Key switch {
                    "BallSpeedUpEffect" => new BallSpeedUpEffect(),
                    "PlayerSpeedUpEffect" => new PlayerSpeedUpEffect(),
                    _ => null
                };
            }
        }
        return null;
        
    }

    public EffectFactory(IConfig config) {
        _config = config;
    }
}
}