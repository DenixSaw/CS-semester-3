using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Model.Effects;
using UnityEngine;

namespace Model.Entities {
public interface IBrick {
    public int Health { get; set; }

    public Color Color { get; set; }
    
    [CanBeNull] public IEffect Effect { get; set; }
}

public interface IPlayer {
    public float Speed { get; set; }
    public float Width { get; set; }
}

public interface IBall {
    public float Speed { get; set; }
    public float Radius { get; set; }
}

public interface IGame {
    public IPlayer Player { get; set; }
    public IBall Ball { get; set; }

    public List<IBrick> Field { get; set; }
}
}
