#nullable enable
using System.Collections.Generic;
using Model.Entities;
using UnityEngine;
using Random = System.Random;

namespace Model.Factories {
public class FieldFactory : IFieldFactory {
    private readonly IConfig _config;
    private readonly IEffectFactory _effectFactory;
    private readonly Random _random = new();

    private Color GetColor(int lineIdx) {
        return lineIdx switch {
            0 => new Color(157, 157, 157, 1),
            1 => new Color(255, 0, 0, 1),
            2 => new Color(0, 112, 255, 1),
            3 => new Color(0, 255, 0, 1),
            4 => new Color(200, 200, 0, 1),
            _ => GetColor(_random.Next(0, 5))
        };
    }

    public List<IBrick?> GetField() {
        var field = new List<IBrick?>();
        for (var i = 0; i < _config.NumOfLines * _config.NumOfBricks; i++) {
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