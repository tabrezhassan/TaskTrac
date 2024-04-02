using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskTrac.DAL.Models;

namespace TaskTrac.DAL.Interfaces
{
    public interface IUserRepository
    {
        Task<Users> GetUserById(int id);
        Task<Users> GetByUserName(string username);
        Task<Users> CreateUser(Users users);
    }
}
