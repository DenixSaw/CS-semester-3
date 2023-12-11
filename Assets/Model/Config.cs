using System.Collections.Generic;
using Model.Effects;

namespace Model {
public class Config : IConfig {
    public IDictionary<string, float> Effects { get; }
    public int NumOfBricks { get; }
    public int NumbOfLines { get; }
    public int BallSpeed { get; }
    public int BallRadius { get; }

    public Config(IDictionary<string, float> effects, int numOfBricks, int numbOfLines, int ballSpeed, int  ballRadius) {
        Effects = effects;
        NumOfBricks = numOfBricks;
        NumbOfLines = numbOfLines;
        BallSpeed = ballSpeed;
        BallRadius = ballRadius;
    }
}
}