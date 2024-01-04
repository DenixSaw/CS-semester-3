#nullable enable
using System.Collections.Generic;
using Model.Entities;

namespace Model.Factories {
public interface IFieldFactory {
    List<IBrick?> GetField();
}
}