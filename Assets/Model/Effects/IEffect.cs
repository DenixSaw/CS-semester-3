
using System;

namespace Model.Effects {
public interface IEffect {
    public void Apply(Scene scene) => throw new NotImplementedException();
}
}