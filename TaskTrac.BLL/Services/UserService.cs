using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskTrac.BLL.Interfaces;
using TaskTrac.DAL.Interfaces;
using TaskTrac.DAL.Models;

namespace TaskTrac.BLL.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<Users> _usersManager;
        private readonly IUserRepository _userRepository;

        public UserService(UserManager<Users> userManager,IUserRepository userRepository)
        {
            _userRepository = userRepository;
            _usersManager = userManager;
        }

        public async Task<IdentityResult> CreateUser(Users user)
        {
            var newUser = new Users
            {
                UserName = user.UserName,
                Email = user.Email,
                PasswordHash = user.PasswordHash,
            };

            var result = await _usersManager.CreateAsync(newUser);
            return result;

            //return await _userRepository.CreateUser(user);
        }

        public async Task<Users> GetByUserName(string username)
        {
            return await _userRepository.GetByUserName(username);
        }

        public async Task<Users> GetUserById(int id)
        {
           return await _userRepository.GetUserById(id);
        }
    }
}
