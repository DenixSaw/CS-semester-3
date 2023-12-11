using System.Collections.Generic;
using Model.Entities;

namespace Model.Factories {
public interface IFieldFactory {
    IList<IBrick> GetField();
}
}