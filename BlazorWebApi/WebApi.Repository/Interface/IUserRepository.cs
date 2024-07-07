

using WebApi.Entity.DataModels;

namespace WebApi.Repository.Interface
{
    public interface IUserRepository
    {
        IQueryable<User> GetUsers();
    }
}
