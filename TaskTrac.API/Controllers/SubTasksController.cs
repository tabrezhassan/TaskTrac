using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskTrac.API.DTO;
using TaskTrac.BLL.Interfaces;

namespace TaskTrac.API.Controllers
{
    [Route("api/[controller]")]
    //[Authorize]
    [ApiController]
    public class SubTasksController : ControllerBase
    {
        private readonly ISubTaskService _subTaskService;

        public SubTasksController(ISubTaskService subTaskService)
        {
            _subTaskService = subTaskService;
        }

        [HttpGet]
        public async Task<IActionResult> GetSubTaskById(int id)
        {
            var subtask = await _subTaskService.GetSubTaskById(id);

            if (subtask == null)
            {
                return NotFound("SubTask not found");
            }

            return Ok(subtask);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSubTask(int id,UpdateSubTaskDTO updateSubTaskDTO)
        {
            var subtask = await _subTaskService.GetSubTaskById(id);

            if (subtask == null)
            {
                return NotFound("SubTask not found");
            }

            subtask.Title = updateSubTaskDTO.Title;
            subtask.Description = updateSubTaskDTO.Description;
            subtask.DueDate = updateSubTaskDTO.DueDate;

            await _subTaskService.UpdateSubTask(subtask);

            return Ok(subtask);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSubTask(int id)
        {
            var subtask = await _subTaskService.GetSubTaskById(id);

            if (subtask == null)
            {
                return NotFound("SubTask not found");
            }

            await _subTaskService.DeleteSubTask(id);

            return Ok(subtask);
        }
    }
}
