using System.Threading.Tasks;

namespace Model.Effects {
public class PlayerSpeedUpEffect : IEffect {
    public void Apply(Game game) {
        var script = game.Player.GetComponent<PlayerScript>();
        script.Player.Speed *= 2;
        Task.Run(async () => {
            await Task.Delay(5000);
            script.Player.Speed /= 2;
        });

    }
}
}