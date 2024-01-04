using System;
using Model.Effects;

namespace Model.Entities {
public class Player : IPlayer {
    
    public float Speed { get; set; }
    public float Width { get; set; }

    public Player(float speed, float width) {
        Speed = speed;
        Width = width;
    }
}
}