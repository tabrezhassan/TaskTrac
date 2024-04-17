using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using TaskTrac.API.DTO;
using TaskTrac.BLL.Interfaces;
using TaskTrac.DAL.Models;

namespace TaskTrac.API.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly ITaskService _taskService;
        private readonly ISubTaskService _subTaskService;
        private readonly UserManager<Users> _userManager;

        public TasksController(ITaskService taskService, ISubTaskService subTaskService,UserManager<Users> userManager)
        {
            _taskService = taskService;
            _subTaskService = subTaskService;
            _userManager = userManager;
        }

        //TASKS ACTIONS

        [HttpGet]
        public async Task<IActionResult> GetAllTasksForUser()
        {
            try
            {
                string userId = _userManager.GetUserId(User);
                var tasks = await _taskService.GetAllForUser(userId);

                if (tasks == null || tasks.Count == 0)
                {
                    return NotFound($"No tasks found for user");
                }
                return Ok(tasks);
            }
            catch (Exception ex)
            {
                // Log the exception for further analysis
                // You can use your preferred logging framework here
                Console.WriteLine($"Exception occurred in GetAllTasksForUser: {ex}");

                // Return an error response
                return StatusCode(500, "An error occurred while processing the request.");
            }
            //string userId = _userManager.GetUserId(User);
            //var tasks = await _taskService.GetAllForUser(userId);

            //return Ok(tasks);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTaskById(int id)
        {
            var task = await _taskService.GetTaskById(id);

            if (task == null)
            {
                return NotFound($"No tasks found for user with ID {id}");
            }

            return Ok(task);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTask(CreateTaskDTO createTaskDTO)
        {
            string userId = _userManager.GetUserId(User);

            var task = new Tasks
            {
                Title = createTaskDTO.Title,
                Description = createTaskDTO.Description,
                DueDate = createTaskDTO.DueDate,
                UserId = userId,
            };

            await _taskService.CreateTask(task);

            return CreatedAtAction(nameof(GetTaskById), new { id = task.Id }, task);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTask(int id, UpdateTaskDTO updateTaskDTO)
        {
            var task = await _taskService.GetTaskById(id);

            if (task == null)
            {
                return NotFound("Task Not Found");
            }

            task.Title = updateTaskDTO.Title;
            task.Description = updateTaskDTO.Description;
            task.DueDate = updateTaskDTO.DueDate;

            await _taskService.UpdateTask(task);

            return Ok(task);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            var task = await _taskService.GetTaskById(id);

            if (task == null)
            {
                return NotFound("Task Not Found");
            }

            await _taskService.DeleteTask(id);

            return Ok(task);
        }

        //SUBTASKS ACTIONS

        [HttpGet("subtasks")]
        public async Task<IActionResult>GetSubTasksForTask(int taskId)
        {
            var subtasks = await _taskService.GetSubTasksForTask(taskId);

            if (subtasks == null)
            {
                return NotFound($"No sub tasks found for Task with ID {taskId}");
            }

            return Ok(subtasks);
        }

        [HttpPost("subtasks")]
        public async Task<IActionResult> CreateSubTask(int taskId, CreateSubTaskDTO createSubTaskDTO)
        {
            var subTask = new SubTasks
            {
                Title = createSubTaskDTO.Title,
                Description = createSubTaskDTO.Description,
                DueDate = createSubTaskDTO.DueDate,
                TaskId = taskId
            };

            await _taskService.CreateSubTask(subTask);

            //return Ok(subTask);
            return CreatedAtAction(nameof(GetSubTasksForTask), new {taskId = taskId},subTask);
        }
    }
}
