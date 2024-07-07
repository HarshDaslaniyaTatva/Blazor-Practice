
using Microsoft.EntityFrameworkCore;
using WebApi.Entity.DataContext;
using WebApi.Repository.Interface;

namespace WebApi.Repository.Implementation
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;
        private DbSet<T> entities;
        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;
            entities = _context.Set<T>();
        }
        
       
        public int GetCount()
        {
            return entities.Count();
        }
        public void Insert(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Add(entity);
        }
        public void Remove(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Remove(entity);
        }
        public void Save()
        {
            _context.SaveChanges();
        }
        public void Update(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Update(entity);
        }
    }
}
