using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TaskTrac.BLL.DTO;
using TaskTrac.BLL.Interfaces;
using TaskTrac.DAL.Models;

namespace TaskTrac.PL.Controllers
{
    public class TasksController : Controller
    {
        private readonly ITaskService _taskService;

        public TasksController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        public IActionResult Index()
        {
           return View();
        }
    }
}
