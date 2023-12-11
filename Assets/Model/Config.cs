using System.Collections.Generic;
using Model.Effects;

namespace Model {
public class Config : IConfig {
    public IDictionary<string, float> Effects { get; } = new Dictionary<string, float>();
    public int NumOfBricks { get; } = 24;
    public int NumbOfLines { get; } = 3;
    public int BallSpeed { get; } = 1;
    public int BallRadius { get; } = 1;
    
    public Config() {}
    public Config(IDictionary<string, float> effects, int numOfBricks, int numbOfLines, int ballSpeed, int  ballRadius) {
        Effects = effects;
        NumOfBricks = numOfBricks;
        NumbOfLines = numbOfLines;
        BallSpeed = ballSpeed;
        BallRadius = ballRadius;
    }
}
}