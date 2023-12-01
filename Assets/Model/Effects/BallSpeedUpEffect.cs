namespace Model.Effects {
public class BallSpeedUpEffect : IEffect {
    public void Apply(Scene scene) {
        scene.Ball.Speed += 1;
    }
}
}