using RestAPIApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestAPIApp.Services
{
    public interface IUserService
    {
        Task<User> GetUser(int UserId);
        Task<IEnumerable<User>> GetUsers();
        Task<bool> AddUser(User user);
        Task<bool> EditUser(User user);
        Task<bool> DeleteUser(int UserId);
    }
}
