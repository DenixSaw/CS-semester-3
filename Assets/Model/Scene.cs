using System;
using System.Collections.Generic;
using Model.Effects;
using Model.Entities;

namespace Model {
public class Scene : IScene {
    public IList<IBrick> Field { get; set; }
    public IPlayer Player { get; }
    public IBall Ball { get; }

    private void ApplyEffect(object sender, EventArgs e) => (sender as IEffect)?.Apply(this);
    
    public Scene() {
        Player = new Player();
        Player.EffectRecieved += ApplyEffect;
        Ball = new Ball(1, 1);
    }
}
}
