using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Model.Effects;
using Model.Entities;

namespace Model {
public class Scene {
    [CanBeNull] public List<Brick> Field { get; set; }
    public Player Player { get; }
    public Ball Ball { get; } = new();

    private void ApplyEffect(object sender, EventArgs e) => (sender as IEffect)?.Apply(this);
    
    public Scene() {
        Player = new Player();
        Player.EffectRecieved += ApplyEffect;
    }
}
}
