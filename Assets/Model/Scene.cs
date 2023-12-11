using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Model.Effects;
using Model.Entities;

namespace Model {
public class Scene : IScene {
    private static readonly Random Random = new();
    
    public IList<IBrick> Field { get; set; }
    public IPlayer Player { get; }
    public IBall Ball { get; }

    private static string GetColor(int lineIdx) {
        return lineIdx switch {
            0 => "#9d9d9d",
            1 => "#ff0000",
            2 => "#0070ff",
            3 => "#ff00ff",
            4 => "#00ff00",
            _ => GetColor(Random.Next(0, 5))
        };
    }

    private IEffect GetEffect() {
        return Random.NextDouble() > 0.3 ? null : AllEffects.Entities[Random.Next(0, AllEffects.Entities.Count)];
    }
    
    private ObservableCollection<IBrick> GenerateField() {
        ObservableCollection<IBrick> field = new();
        for (var i = 0; i < 5 * 8; i++) {
            field.Add(new Brick(i < 5 ? 2 : 1, GetColor(i / 5), GetEffect()));
        }
        return field;
    }
    
    private void ApplyEffect(object sender, EventArgs e) => (sender as IEffect)?.Apply(this);
    
    public Scene() {
        Player = new Player();
        Player.EffectRecieved += ApplyEffect;
        Ball = new Ball(1, 1);
        Field = GenerateField();
    }
}
}
