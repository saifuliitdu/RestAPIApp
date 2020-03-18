using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace RestAPIApp.Models
{
    public class RestAPIContext : DbContext
    {
        public RestAPIContext(DbContextOptions<RestAPIContext> options)
            : base(options)
        {
        }
        
        public DbSet<User> Users { get; set; }
        public DbSet<AssignTask> AssignTasks { get; set; }
        //public DbSet<UserLogin> UserLogins { get; set; }
    }
}
