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
       private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Users> GetByUserName(string username)
        {
            return await _userRepository.GetByUserName(username);
        }

        public async Task<Users> GetUserById(int id)
        {
           return await _userRepository.GetUserById(id);
        }

        public async Task<Users> CreateUser(Users users)
        {
            try
            {
                if(users != null)
                {
                    return await _userRepository.CreateUser(users);
                }
                else
                {
                    throw new ArgumentNullException(nameof(users));
                }
            }
            catch(Exception)
            {
                throw;
            }
        }
    }
}
