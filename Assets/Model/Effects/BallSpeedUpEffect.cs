using System.Threading.Tasks;
using UnityEngine;

namespace Model.Effects {
public  class BallSpeedUpEffect : IEffect {
    public void Apply(Game game) {
        var script = game.Ball.GetComponent<BallScript>();
        var force = script.ballRigidbody.velocity;
        script.ballRigidbody.AddForce(force * 2, ForceMode2D.Force);
        Task.Run(async () => {
            await Task.Delay(5000);
            script.ballRigidbody.AddForce(force / 2, ForceMode2D.Force);
        });
    }
}
}