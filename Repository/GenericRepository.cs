using JournyTask.IRepository;
using JournyTask.Models;
using Microsoft.EntityFrameworkCore;

namespace JournyTask.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private FCarePlus3Context _context;
        private DbSet<T> dbSet;
        public GenericRepository(FCarePlus3Context context)
        {
            _context = context;
            dbSet = _context.Set<T>();
        }
        public void add(T entity)
        {
            dbSet.Add(entity);
        }

        public void delete(T entity)
        {
            dbSet.Remove(entity);
        }

        public IEnumerable<T> GetAll()
        {
            return dbSet.ToList();
        }

        public T getById(Guid id)
        {
            return dbSet.Find(id);
        }
        public T getByName(string name)
        {
            return dbSet.FirstOrDefault(e => EF.Property<string>(e, "NameEn") == name);
        }

        public void save()
        {
            _context.SaveChanges();
        }

        public void update(T entity)
        {
            dbSet.Update(entity);
        }
    }
}
