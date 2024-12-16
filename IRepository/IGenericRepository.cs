namespace JournyTask.IRepository
{
    public interface IGenericRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T getById(Guid id);
        T getByName(string name);
        void add(T entity);
        void delete(T entity);
        void update(T entity);
        void save();
    }
}
