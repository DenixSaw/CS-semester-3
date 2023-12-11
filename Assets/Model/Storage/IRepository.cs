namespace Model.Storage {
public interface IRepository<out TR, in TU, in TD> {
    public TR Read();
    public bool Update(TU item);
    public bool Delete(TD key);
}
}