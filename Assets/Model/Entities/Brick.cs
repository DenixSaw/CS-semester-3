
using System;
using JetBrains.Annotations;
using Model.Effects;

namespace Model.Entities {
public class Brick : IBrick {
    private int _health;
    public int Health {
        get => _health;
        set {
            if (value < 0)
                throw new Exception("Некорректное значение HP");
            _health = value;
        }
    }

    public string Color { get; set; }
    
    public IEffect Effect { get; set; }

    public Brick(int health = 1, string color = "#fff", IEffect effect = null) {
        Health = health;
        Color = color;
        Effect = effect;
    }
}
}
