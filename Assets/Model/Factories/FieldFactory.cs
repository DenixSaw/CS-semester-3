using System.Collections.Generic;
using Model.Entities;
using System;

namespace Model.Factories {
public class FieldFactory : IFieldFactory {
    private readonly IConfig _config;
    private readonly IEffectFactory _effectFactory;
    private readonly Random _random = new();

    private string GetColor(int lineIdx) {
        return lineIdx switch {
            0 => "#9d9d9d",
            1 => "#ff0000",
            2 => "#0070ff",
            3 => "#ff00ff",
            4 => "#00ff00",
            _ => GetColor(_random.Next(0, 5))
        };
    }

    public IList<IBrick> GetField() {
        IList<IBrick> field = new List<IBrick>();
        for (var i = 0; i < _config.NumbOfLines * _config.NumOfBricks; i++) {
            field.Add(new Brick(i < _config.NumOfBricks ? 2 : 1, GetColor(i / _config.NumOfBricks), _effectFactory.GetEffect()));
        }

        return field;
    }

    public FieldFactory(IConfig config, IEffectFactory effectFactory) {
        _config = config;
        _effectFactory = effectFactory;
    }
}
}