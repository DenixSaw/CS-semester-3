
using System;

namespace Model.Effects {
public interface IEffect {
    public void Apply(Game scene) => throw new NotImplementedException();
}
}