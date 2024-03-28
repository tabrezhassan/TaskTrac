using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskTrac.DAL.Models
{
    public class SubTasks
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="Title for Task is required")]
        [StringLength(100)]
        public string? Title { get; set; }

        public string? Description  { get; set; }

        [Required(ErrorMessage = "Due Date for Task is required")]
        public DateTime DueDate { get; set; }
        public int TaskId { get; set; }
        public Tasks? Tasks { get; set; }

    }
}
