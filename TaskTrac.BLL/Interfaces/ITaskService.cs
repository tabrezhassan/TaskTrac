using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskTrac.DAL.Models;

namespace TaskTrac.BLL.Interfaces
{
    public interface ITaskService
    {
        Task<List<Tasks>> GetAllForUser(int id);
        Task<Tasks> GetTaskById(int id);
        Task CreateTask(Tasks tasks);
        Task UpdateTask(Tasks tasks);
        Task DeleteTask(int id);

        Task<List<SubTasks>> GetSubTasksForTask(int id);
        Task CreateSubTask(SubTasks subTasks);
        Task UpdateSubTask(SubTasks subTasks);  
        Task DeleteSubTask(int id);
        
    }
}
