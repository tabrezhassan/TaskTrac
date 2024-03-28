using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using TaskTrac.DAL.Data;
using TaskTrac.DAL.Interfaces;
using TaskTrac.DAL.Models;

namespace TaskTrac.DAL.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly AppDbContext _appDbContext;

        public TaskRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task CreateTask(Tasks tasks)
        {
            _appDbContext.Tasks.Add(tasks);

            await _appDbContext.SaveChangesAsync();
        }

        public async Task<List<Tasks>> GetAllForUser(int Id)
        {
            return await _appDbContext.Tasks
                .Where(t => t.UserId == Id).Include(t => t.SubTasks).ToListAsync();
        }

        public async Task<Tasks> GetTaskById(int Id)
        {
            return await _appDbContext.Tasks
                 .Include(t => t.SubTasks).FirstOrDefaultAsync(t => t.Id == Id);
        }

        async Task ITaskRepository.DeleteTask(int Id)
        {
            var task = await _appDbContext.Tasks.FindAsync(Id);

            if(task != null)
            {
                _appDbContext.Tasks.Remove(task);

                await _appDbContext.SaveChangesAsync();
            }
        }

        async Task ITaskRepository.UpdateTask(Tasks tasks)
        {
            _appDbContext.Tasks.Update(tasks);

            await _appDbContext.SaveChangesAsync();
        }
    }
}
