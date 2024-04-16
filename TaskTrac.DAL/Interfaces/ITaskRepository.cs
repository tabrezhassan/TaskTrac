using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskTrac.DAL.Models;

namespace TaskTrac.DAL.Interfaces
{
    public interface ITaskRepository
    {
        Task<List<Tasks>> GetAllForUser(string Id);
        Task<Tasks> GetTaskById(int Id);
        Task CreateTask(Tasks tasks);
        Task UpdateTask(Tasks tasks);
        Task DeleteTask(int Id);

    }
}
