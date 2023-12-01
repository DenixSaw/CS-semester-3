using System;
using JetBrains.Annotations;
using Model.Effects;

namespace Model.Entities {
public interface IBrick {
    public int Health { get; set; }

    public string Color { get; set; }
    
    [CanBeNull] public IEffect Effect { get; set; }
}

public interface IPlayer {
    public event EventHandler EffectRecieved;
    
    public int Speed { get; set; }
    public int Width { get; set; }
    public int Height { get; set; }
}

public interface IBall {
    public int Speed { get; set; }
    public int Radius { get; set; }
}

}
