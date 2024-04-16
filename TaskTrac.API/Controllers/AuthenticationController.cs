using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using TaskTrac.BLL.Interfaces;
using TaskTrac.DAL.Models;
using System.Text;
using System.Runtime.CompilerServices;
using TaskTrac.API.Interfaces;
using TaskTrac.API.DTO;
using System.Data;

namespace TaskTrac.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly UserManager<Users> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly IJwtService _jwtService;

        public AuthenticationController(UserManager<Users> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration, IJwtService jwtService)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _jwtService = jwtService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser(RegisterUserDTO registerUserDTO)
        {
            var userExists = await _userManager.FindByEmailAsync(registerUserDTO.Email);
            if (userExists != null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            Users user = new Users()
            {
                UserName = registerUserDTO.Email,
                Email = registerUserDTO.Email,
                Password = registerUserDTO.Password,
            };

            var result = await _userManager.CreateAsync(user, registerUserDTO.Password);

            if (!result.Succeeded)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return Ok();

        }

        [HttpPost("login")]
        //[Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserDTO loginUserDTO)
        {
            var user = await _userManager.FindByNameAsync(loginUserDTO.Email);

            if (user != null && await _userManager.CheckPasswordAsync(user, loginUserDTO.Password))
            {
                var userRoles = await _userManager.GetRolesAsync(user);

                var token = await _jwtService.GenerateJwtToken(user, userRoles);
                
                return Ok(new
                {
                    token,
                    expiration = DateTime.Now.AddHours(5)
                });
            }

            return Unauthorized();

        }


    }
}

