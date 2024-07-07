
using Microsoft.EntityFrameworkCore;
using WebApi.Dtos.Dtos;
using WebApi.Entity.DataModels;
using WebApi.Repository.Interface;
using WebApi.Service.Interface;

namespace WebApi.Service.Implementation
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<UserDto?> GetUser(int id)
        {
            try
            {
                User? data = await _userRepository.GetUsers().FirstOrDefaultAsync(x => x.Id == id);

                if (data == null)
                {
                    return null;
                }
                UserDto user = new()
                {
                    Id = data.Id,
                    Username = data.Username,
                    Role = data.Role,
                };
                return user;
            }
            catch (Exception)
            {
                throw new Exception("Internal Server Error");
            }
        }


    }
}
