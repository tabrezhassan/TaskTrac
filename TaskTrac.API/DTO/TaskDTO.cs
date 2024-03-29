using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskTrac.DAL.Models;

namespace TaskTrac.BLL.DTO
{
    public class TaskDTO
    {
        public int Id { get; set; }

        public string? Title { get; set; }

        public string? Description { get; set; }

        public DateTime DueDate { get; set; }

        public int UserId { get; set; }
        public Users Users { get; set; }

        public List<SubTasks>? SubTasks { get; set; }
    }

    public class CreateTaskDTO
    {
       
        [Required(ErrorMessage = "Title for Task is required")]
        [StringLength(100)]
        public string? Title { get; set; }

        [StringLength(150)]
        public string? Description { get; set; }

        [Required(ErrorMessage = "Due Date for Task is required")]
        public DateTime DueDate { get; set; }
    }

    public class UpdateTaskDTO
    {

        [Required(ErrorMessage = "Title for Task is required")]
        [StringLength(100)]
        public string? Title { get; set; }

        [StringLength(150)]
        public string? Description { get; set; }

        [Required(ErrorMessage = "Due Date for Task is required")]
        public DateTime DueDate { get; set; }
    }
}
