using System.Collections.Generic;

namespace Model {
public interface IConfig {
    public IDictionary<string, float> Effects { get; }
    public int NumOfBricks { get; }
    public int NumbOfLines { get; }
    public int BallSpeed { get; }
    public int BallRadius { get; }
}
}