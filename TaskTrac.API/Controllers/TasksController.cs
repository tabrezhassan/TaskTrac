using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using TaskTrac.BLL.DTO;
using TaskTrac.BLL.Interfaces;
using TaskTrac.DAL.Models;

namespace TaskTrac.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly ITaskService _taskService;
        private readonly ISubTaskService _subTaskService;
        private readonly UserManager<Users> _userManager;

        public TasksController(ITaskService taskService, ISubTaskService subTaskService, UserManager<Users> userManager)
        {
            _taskService = taskService;
            _subTaskService = subTaskService;
            _userManager = userManager;
        }

        //TASKS ACTIONS

        [HttpGet]
        public async Task<IActionResult> GetAllTasksForUser()
        {
            int userid = Convert.ToInt32(_userManager.GetUserId(User));
            var tasks = await _taskService.GetAllForUser(userid);

            return Ok(tasks);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTaskById(int id)
        {
            var task = await _taskService.GetTaskById(id);

            if (task == null)
            {
                return NotFound("No Task Found");
            }

            return Ok(task);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTask(CreateTaskDTO createTaskDTO)
        {
            int userid = Convert.ToInt32(_userManager.GetUserId(User));

            var task = new Tasks
            {
                Title = createTaskDTO.Title,
                Description = createTaskDTO.Description,
                DueDate = createTaskDTO.DueDate,
                UserId = userid,
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

        [HttpGet("{taskid}/subtasks")]
        public async Task<IActionResult>GetSubTasksForTask(int taskId)
        {
            var subtasks = await _taskService.GetSubTasksForTask(taskId);

            return Ok(subtasks);
        }

        [HttpPost("{taskid/subtasks")]
        public async Task<IActionResult> CreateSubTask(int taskId, CreateSubTaskDTO createSubTaskDTO)
        {
            var subTask = new SubTasks
            {
                Title = createSubTaskDTO.Title,
                Description = createSubTaskDTO.Description,
                DueDate = createSubTaskDTO.DueDate,
            };

            await _taskService.CreateSubTask(subTask);

            return CreatedAtAction(nameof(GetSubTasksForTask), new {taskId},subTask);
        }
    }
}
