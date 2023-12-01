namespace Model.Effects {
public class PlayerSpeedUpEffect {
    public void Apply(Scene scene) {
        scene.Player.Speed += 1;
    }
}
}