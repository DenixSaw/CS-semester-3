using System.Collections.Generic;

namespace Model {
public interface IConfig {
    public IDictionary<string, float> Effects { get; }
    public int NumOfBricks { get; }
    public int NumOfLines { get; }
    public float BallSpeed { get; }
    public float BallRadius { get; }
    public float PlayerWidth { get; }
    public float PlayerSpeed { get; }
}
}