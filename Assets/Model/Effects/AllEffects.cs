using System.Collections.Generic;

namespace Model.Effects {
internal static class AllEffects {
    public static readonly List<IEffect> Entities = new() { new BallSpeedUpEffect(), new PlayerSpeedUpEffect() };
}
}