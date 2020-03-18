using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestAPIApp.Models;
using RestAPIApp.Services;

namespace RestAPIApp.Controllers
{
    [Produces("application/json")]
    //[Route("api/AssignTasks")]
    public class AssignTasksController : Controller
    {
        private readonly ITaskService _taskService;
        public AssignTasksController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        // GET: api/AssignTasks
        [Route("api/AssignTasks")]
        [HttpGet]
        public async Task<IEnumerable<AssignTaskVM>> GetAssignTasks()
        {
            return await _taskService.GetTasks();
        }

        // GET: api/AssignTasks
        [Route("api/AssignTasks/GetAssignTasksByUserId/{id}")]
        [HttpGet]
        public async Task<IEnumerable<AssignTaskVM>> GetAssignTasksByUserId([FromRoute] int id)
        {
            return await _taskService.GetTasksByUserId(id);
        }

        // GET: api/AssignTasks/5
        [Route("api/AssignTasks/GetAssignTask/{id}")]
        [HttpGet]
        public async Task<IActionResult> GetAssignTask([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var assignTask = await _taskService.GetTask(id);

            if (assignTask == null)
            {
                return NotFound();
            }
            var assignTaskVM = new AssignTaskVM
            {
                TaskId = assignTask.TaskId,
                Name = assignTask.Name,
                Description = assignTask.Description,
                StartDate = assignTask.StartDate,
                EndDate = assignTask.EndDate,
                UserId = assignTask.AssignTo.UserId
            };

            return Ok(assignTaskVM);
        }

        // PUT: api/AssignTasks/5
        [Route("api/AssignTasks/PutAssignTask/{id}")]
        [HttpPut]
        public async Task<IActionResult> PutAssignTask([FromRoute] int id, [FromBody] AssignTaskVM assignTaskVM)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != assignTaskVM.TaskId)
            {
                return BadRequest();
            }

            try
            {
                await _taskService.EditTask(assignTaskVM);
            }
            catch (DbUpdateConcurrencyException)
            {
                    throw;
            }

            return NoContent();
        }

        // POST: api/AssignTasks
        [Route("api/AssignTasks/PostAssignTask")]
        [HttpPost]
        public async Task<IActionResult> PostAssignTask([FromBody] AssignTaskVM assignTaskVM)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
           
            await _taskService.AddTask(assignTaskVM);

            return CreatedAtAction("GetAssignTask", new { id = assignTaskVM.TaskId }, assignTaskVM);
        }

        // DELETE: api/AssignTasks/5
        [Route("api/AssignTasks/DeleteAssignTask/{id}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteAssignTask([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _taskService.DeleteTask(id);

            return Ok();
        }
    }
}