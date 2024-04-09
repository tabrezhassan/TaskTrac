using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskTrac.DAL.Models;

namespace TaskTrak.PL.Controllers
{
    public class TasksController : Controller
    {
        public IActionResult Tasks()
        {
            return View();
        }

        public IActionResult CreateTask()
        {
            return View();
        }

        public IActionResult UpdateTask(int id)
        {
            var task = new Tasks
            {
                Id = id
            };

            return View(task);
        }

        public IActionResult DeleteTask(int id)
        {
            var task = new Tasks
            {
                Id = id
            };

            return View(task);
        }
    }
}
