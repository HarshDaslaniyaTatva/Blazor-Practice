

namespace WebApi.Repository.Interface
{
    public interface IGenericRepository<T> where T : class
    {
       
        void Insert(T entity);
        void Update(T entity);
        void Remove(T entity);
        void Save();
        int GetCount();
        
    }
}
