namespace Model.Effects {
public class PlayerSpeedUpEffect : IEffect {
    public void Apply(Scene scene) {
        scene.Player.Speed += 1;
    }
}
}