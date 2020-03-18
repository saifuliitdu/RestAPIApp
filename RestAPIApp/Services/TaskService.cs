using Microsoft.EntityFrameworkCore;
using RestAPIApp.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestAPIApp.Services
{
    public interface ITaskService
    {
        Task<AssignTaskVM> AddTask(AssignTaskVM task);
        Task<bool> DeleteTask(int TaskId);
        Task<bool> EditTask(AssignTaskVM task);
        Task<AssignTask> GetTask(int TaskId);
        Task<IEnumerable<AssignTaskVM>> GetTasks();
        Task<IEnumerable<AssignTaskVM>> GetTasksByUserId(int UserId);
    }
    public class TaskService : ITaskService
    {
        RestAPIContext _db;
        public TaskService(RestAPIContext db)
        {
            _db = db;
        }
        public async Task<AssignTaskVM> AddTask(AssignTaskVM task)
        {
            var user = _db.Users.Find(task.UserId);
            AssignTask assignTask = new AssignTask
            {
                TaskId = 0,
                Name = task.Name,
                Description = task.Description,
                StartDate = task.StartDate,
                EndDate = task.EndDate,
                AssignTo = user
            };
            _db.AssignTasks.Add(assignTask);
            await _db.SaveChangesAsync();
            task.TaskId = assignTask.TaskId;
            return task;
        }

        public async Task<bool> DeleteTask(int TaskId)
        {
            var task = _db.AssignTasks.Find(TaskId);
            _db.AssignTasks.Remove(task);
            return await _db.SaveChangesAsync() > 0;
        }

        public async Task<bool> EditTask(AssignTaskVM task)
        {
            var user = _db.Users.Find(task.UserId);
            AssignTask assignTask = new AssignTask
            {
                TaskId = task.TaskId,
                Name = task.Name,
                Description = task.Description,
                StartDate = task.StartDate,
                EndDate = task.EndDate,
                AssignTo = user
            };
            _db.Entry(assignTask).State = EntityState.Modified;
            return await _db.SaveChangesAsync() > 0;
        }

        public async Task<AssignTask> GetTask(int TaskId)
        {
            var task = await _db.AssignTasks.Include(x => x.AssignTo).SingleOrDefaultAsync(x=>x.TaskId == TaskId);
            return task;
        }

        public async Task<IEnumerable<AssignTaskVM>> GetTasks()
        {
            var list = await _db.AssignTasks.Include(x => x.AssignTo).ToListAsync();

            var list2 = new List<AssignTaskVM>();
            list.ForEach(x =>
            {
                var assignToUserId = x.AssignTo != null ? x.AssignTo.UserId : 0;
                var assignToUserName = x.AssignTo != null ? x.AssignTo.FirstName + ' ' + x.AssignTo.LastName : "";
                var o = new AssignTaskVM
                {
                    TaskId = x.TaskId,
                    Name = x.Name,
                    Description = x.Description,
                    StartDate = x.StartDate,
                    EndDate = x.EndDate,
                    UserId = assignToUserId,
                    AssignedUserName = assignToUserName
                };
                list2.Add(o);
            });

            return list2;
        }

        public async Task<IEnumerable<AssignTaskVM>> GetTasksByUserId(int UserId)
        {
            var user = await _db.Users.Include(x => x.AssignTasks).FirstOrDefaultAsync(x => x.UserId == UserId);
            var list = user.AssignTasks.ToList();

            var list2 = new List<AssignTaskVM>();
            list.ForEach(x =>
            {
                var assignToUserId = x.AssignTo != null ? x.AssignTo.UserId : 0;
                var assignToUserName = x.AssignTo != null ? x.AssignTo.FirstName + ' ' + x.AssignTo.LastName : "";
                var o = new AssignTaskVM
                {
                    TaskId = x.TaskId,
                    Name = x.Name,
                    Description = x.Description,
                    StartDate = x.StartDate,
                    EndDate = x.EndDate,
                    UserId = assignToUserId,
                    AssignedUserName = assignToUserName
                };
                list2.Add(o);
            });

            return list2;
        }
    }
}
