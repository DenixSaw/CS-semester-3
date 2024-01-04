using System;

namespace Model.Storage {
public interface IRepository<T> {
    static T Get() {
        throw new NotImplementedException();
    }
    
    static void Set(T newItem) {
        throw new NotImplementedException();
    }
}
}