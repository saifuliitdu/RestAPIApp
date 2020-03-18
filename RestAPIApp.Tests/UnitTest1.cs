using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using RestAPIApp.Models;
using System.Linq;

namespace Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            var options = new DbContextOptionsBuilder<RestAPIContext>()
                .UseInMemoryDatabase("testdb")
                .Options;

            var db = new RestAPIContext(options);
            User u1 = new User { UserId = 1, FirstName = "A", LastName = "B" };
            User u2 = new User { UserId = 2, FirstName = "C", LastName = "D" };
            db.Users.Add(u1);
            db.Users.Add(u2);

            AssignTask t1 = new AssignTask { Name = "T1", AssignTo = u1 };
            AssignTask t2 = new AssignTask { Name = "T2", AssignTo = u1 };
            AssignTask t3 = new AssignTask { Name = "T3", AssignTo = u1 };
            AssignTask t4 = new AssignTask { Name = "T4", AssignTo = u2 };
            AssignTask t5 = new AssignTask { Name = "T5", AssignTo = u2 };

            db.AssignTasks.Add(t1);
            db.AssignTasks.Add(t2);
            db.AssignTasks.Add(t3);
            db.AssignTasks.Add(t4);
            db.AssignTasks.Add(t5);

            db.SaveChanges();
            var users = db.Users;
            var u1AssignedTasks = db.Users.Include(x => x.AssignTasks).FirstOrDefault(x => x.UserId == 1);
            var u2AssignedTasks = db.Users.Include(x => x.AssignTasks).FirstOrDefault(x => x.UserId == 2);

            Assert.AreEqual(users.Count(), 2);
            Assert.AreEqual(u1AssignedTasks.AssignTasks.Count(), 3);
            Assert.AreEqual(u2AssignedTasks.AssignTasks.Count(), 2);
        }
    }
}