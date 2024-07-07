using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Dtos.Dtos;
using WebApi.Entity.DataModels;
using WebApi.Repository.Interface;

namespace WebApi.Service.Interface
{
    public interface IUserService 
    {
        Task<UserDto?> GetUser(int id);
    }
}
