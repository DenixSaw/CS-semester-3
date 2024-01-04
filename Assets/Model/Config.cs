using System.Collections.Generic;   

namespace Model {
public class Config : IConfig {
    public IDictionary<string, float> Effects { get; } 
    public int NumOfBricks { get; }
    public int NumOfLines { get; } 
    public float BallSpeed { get; }
    public float BallRadius { get; }
    public float PlayerWidth { get; }
    public float PlayerSpeed { get; }
    
    public Config(IDictionary<string, float> effects, int numOfBricks, int numOfLines, float ballSpeed, float  ballRadius,
        float playerWidth, float playerSpeed) {
        Effects = effects;
        NumOfBricks = numOfBricks;
        NumOfLines = numOfLines;
        BallSpeed = ballSpeed;
        BallRadius = ballRadius;
        PlayerWidth = playerWidth;
        PlayerSpeed = playerSpeed;
    }
}
}