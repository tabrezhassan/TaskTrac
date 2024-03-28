using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.InteropServices;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using TaskTrac.DAL.Data;
using TaskTrac.DAL.Interfaces;
using TaskTrac.DAL.Models;

namespace TaskTrac.DAL.Repositories
{
    public class SubTaskRepository : ISubTaskRepository
    {

        private readonly AppDbContext _appDbContext;

        public SubTaskRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task CreateSubTask(SubTasks subTasks)
        {
            _appDbContext.SubTasks.Add(subTasks);

            await _appDbContext.SaveChangesAsync();
        }

        public async Task DeleteSubTask(int id)
        {
            var subtask = await _appDbContext.SubTasks.FindAsync(id);

            if (subtask != null)
            {
                _appDbContext.SubTasks.Remove(subtask);

                await _appDbContext.SaveChangesAsync();
            }
        }

        public async Task<List<SubTasks>> GetAllForTask(int id)
        {
            return await _appDbContext.SubTasks.Where(s => s.TaskId == id).ToListAsync();
        }

        public async Task<SubTasks> GetTaskById(int id)
        {
            return await _appDbContext.SubTasks.FindAsync(id);
        }

        public async Task UpdateSubTask(SubTasks subTasks)
        {
            _appDbContext.SubTasks.Update(subTasks);

            await _appDbContext.SaveChangesAsync();
        }
    }
}
