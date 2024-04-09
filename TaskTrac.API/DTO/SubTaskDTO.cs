using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskTrac.API.DTO
{
    public class SubTaskDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public int TaskId { get; set; }
    }


    public class CreateSubTaskDTO
    {
        [Required(ErrorMessage = "Title for Task is required")]
        [StringLength(100)]
        public string Title { get; set; }

        [StringLength(150)]
        public string? Description { get; set; }

        [Required(ErrorMessage = "Due Date for Task is required")]
        public DateTime DueDate { get; set; }
    }

    public class UpdateSubTaskDTO
    {
        [Required(ErrorMessage = "Title for Task is required")]
        [StringLength(100)]
        public string Title { get; set; }

        [StringLength(150)]
        public string? Description { get; set; }

        [Required(ErrorMessage = "Due Date for Task is required")]
        public DateTime DueDate { get; set; }
    }

}
