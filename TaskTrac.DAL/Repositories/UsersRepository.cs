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

        //public async Task<Users> CreateUser(Users users)
        //{
        //    try
        //    {
        //        if (users != null)
        //        {
        //            var newuser = _appDbContext.Add<Users>(users);
        //            await _appDbContext.SaveChangesAsync();
        //            return newuser.Entity;
        //        }
        //        else
        //        {
        //            return new Users();
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }

        //    //_appDbContext.Users.Add(users);

        //    //await _appDbContext.SaveChangesAsync();
        //}

        public async Task<Users> GetByUserName(string username)
        {
            return await _appDbContext.Users.FirstOrDefaultAsync(u => u.UserName == username);
        }

        public async Task<Users> GetUserById(int id)
        {
            return await _appDbContext.Users.FindAsync(id);
        }

        public async Task<Users> CreateUser(Users users)
        {
            try
            {
                if (users != null)
                {
                    var newuser = _appDbContext.Add<Users>(users);
                    await _appDbContext.SaveChangesAsync();
                    return newuser.Entity;
                }
                else
                {
                    return new Users();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
