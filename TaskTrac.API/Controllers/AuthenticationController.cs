using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TaskTrac.BLL.DTO;
using TaskTrac.BLL.Interfaces;
using TaskTrac.DAL.Models;

namespace TaskTrac.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly UserManager<Users> _userManager;

        public AuthenticationController(IUserService userService, UserManager<Users> userManager)
        {
            _userService = userService;
            _userManager = userManager;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser(RegisterUserDTO registerUserDTO)
        {
            var newUser = new Users
            {
                UserName = registerUserDTO.UserName,
                Email = registerUserDTO.Email,
            };

            var result = await _userService.CreateUser(newUser);

            if(!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            return Ok();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginUserDTO loginUserDTO)
        {
            var user = _userManager.FindByNameAsync(loginUserDTO.UserName);

            if(user == null)
            {
                return BadRequest("Invalid username or password");
            }

            var isValidPassword = await _userManager.CheckPasswordAsync(user, loginUserDTO.passwordHash);
        }
    }
}
