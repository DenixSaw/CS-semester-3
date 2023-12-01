using System;
using Model.Effects;

namespace Model.Entities {
public class Player {
    public event EventHandler EffectRecieved;

    protected void OnEffectRecieved(IEffect effect) {
        EffectRecieved?.Invoke(effect, EventArgs.Empty);
    }
    
    public int Speed { get; set; } = 1;
    public int Width { get; set; } = 10;
    public int Height { get; set; } = 2;

}
}