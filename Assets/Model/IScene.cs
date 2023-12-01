using System.Collections.Generic;
using JetBrains.Annotations;
using Model.Entities;

namespace Model {
public interface IScene {
    [CanBeNull] public IList<IBrick> Field { get; set; }
    public IPlayer Player { get; }
    public IBall Ball { get; } 
}
}