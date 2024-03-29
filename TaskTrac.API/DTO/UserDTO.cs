using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskTrac.BLL.DTO
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string  UserName { get; set; }
        public string Email { get; set; }
        public string passwordHash { get; set; }
    }

    public class RegisterUserDTO
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "The {0} must be at least {2} and a max of {1} characters long", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string passwordHash { get; set; }
    }

    public class LoginUserDTO
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "The {0} must be at least {2} and a max of {1} characters long", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string passwordHash { get; set; }
    }
}
