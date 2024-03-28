using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskTrac.DAL.Data;
using TaskTrac.DAL.Interfaces;
using TaskTrac.DAL.Models;

namespace TaskTrac.DAL.Repositories
{
    public class UsersRepository : IUserRepository
    {
        private readonly AppDbContext _appDbContext;

        public UsersRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task CreateUser(Users users)
        {
            _appDbContext.Users.Add(users);

            await _appDbContext.SaveChangesAsync();
        }

        public async Task<Users> GetByUserName(string username)
        {
            return await _appDbContext.Users.FirstOrDefaultAsync(u => u.UserName == username);
        }

        public async Task<Users> GetUserById(int id)
        {
            return await _appDbContext.Users.FindAsync(id);
        }
    }
}
