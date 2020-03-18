using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestAPIApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RestAPIApp.Services
{
    public class UserService : IUserService
    {
        RestAPIContext db;
        public UserService(RestAPIContext db)
        {
            this.db = db;
        }

        public async Task<bool> AddUser(User user)
        {
            db.Users.Add(user);
            return await db.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteUser(int UserId)
        {
            var user = db.Users.Find(UserId);
            db.Users.Remove(user);
            return await db.SaveChangesAsync() > 0;
        }

        public async Task<bool> EditUser(User user)
        {
            db.Entry(user).State = EntityState.Modified;
            return await db.SaveChangesAsync() > 0;
            var dbUser = db.Users.Find(user.UserId);
            dbUser.FirstName = user.FirstName;
            dbUser.LastName = user.LastName;
            dbUser.Email = user.Email;
            dbUser.Password = user.Password;
            dbUser.RetypePassword = user.RetypePassword;
            dbUser.Address = user.Address;

            //return NoContent();
        }

        public async Task<User> GetUser(int UserId)
        {
            var user = await db.Users.FirstOrDefaultAsync(x => x.UserId == UserId);
            return user;
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            return await db.Users.ToListAsync();
        }

    }
}
