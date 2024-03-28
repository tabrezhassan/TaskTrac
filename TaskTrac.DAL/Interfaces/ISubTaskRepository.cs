using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskTrac.DAL.Models;

namespace TaskTrac.DAL.Interfaces
{
    public interface ISubTaskRepository
    {
        Task<List<SubTasks>> GetAllForTask(int id);
        Task<SubTasks> GetTaskById(int id);
        Task CreateSubTask(SubTasks subTasks);
        Task UpdateSubTask(SubTasks subTasks);
        Task DeleteSubTask(int id);
    }
}
