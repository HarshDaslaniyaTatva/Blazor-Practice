

using WebApi.Entity.DataContext;
using WebApi.Entity.DataModels;
using WebApi.Repository.Interface;

namespace WebApi.Repository.Implementation
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private readonly ApplicationDbContext _context;
        public UserRepository (ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public IQueryable<User> GetUsers()
        {
            return _context.Users;
        }
    }
}
