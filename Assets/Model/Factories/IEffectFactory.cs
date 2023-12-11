using JetBrains.Annotations;
using Model.Effects;

namespace Model.Factories {
public interface IEffectFactory {
    [CanBeNull]
    IEffect GetEffect();
}
}