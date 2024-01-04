
using System;
using Model.Effects;
using Model.Storage;
using Newtonsoft.Json;
using UnityEngine;

namespace Model.Entities {
public class Brick : IBrick {
    public int Health { get; set; }

    [JsonProperty("Color")]
    [JsonConverter(typeof(ColorConverter))]
    public Color Color { get; set; }
    
    public IEffect Effect { get; set; }

    public Brick(int health = 1, Color color = default, IEffect effect = null) {
        Health = health;
        Color = color;
        Effect = effect;
    }
}
}
