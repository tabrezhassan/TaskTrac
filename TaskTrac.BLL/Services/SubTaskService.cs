using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using TaskTrac.BLL.Interfaces;
using TaskTrac.DAL.Interfaces;
using TaskTrac.DAL.Models;

namespace TaskTrac.BLL.Services
{
    public class SubTaskService : ISubTaskService
    {
        private readonly ISubTaskRepository _subTaskRepository;

        public SubTaskService(ISubTaskRepository subTaskRepository)
        {
            _subTaskRepository = subTaskRepository;
        }

        public async Task CreateSubTask(SubTasks subTasks)
        {
            await _subTaskRepository.CreateSubTask(subTasks);
        }

        public async Task DeleteSubTask(int id)
        {
            await _subTaskRepository.DeleteSubTask(id);
        }

        public async Task<List<SubTasks>> GetAllForTask(int id)
        {
            return await _subTaskRepository.GetAllForTask(id);
        }

        public async Task<SubTasks> GetSubTaskById(int id)
        {
            return await _subTaskRepository.GetTaskById(id);
        }

        public async Task UpdateSubTask(SubTasks subTasks)
        {
            await _subTaskRepository.UpdateSubTask(subTasks);
        }
    }
}
