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
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;
        private  ISubTaskRepository _subTaskRepository;

        public TaskService(ITaskRepository taskRepository,ISubTaskRepository subTaskRepository)
        {
            _taskRepository = taskRepository;
            _subTaskRepository = subTaskRepository;
        }

        //public void SetSubTaskService(ISubTaskService subTaskService)
        //{
        //    _subTaskService = subTaskService;
        //}

        public async Task CreateSubTask(SubTasks subTasks)
        {
            await _subTaskRepository.CreateSubTask(subTasks);
        }

        public async Task CreateTask(Tasks tasks)
        {
            await _taskRepository.CreateTask(tasks);
        }

        public async Task DeleteSubTask(int id)
        {
            await _subTaskRepository.DeleteSubTask(id);
        }

        public async Task DeleteTask(int id)
        {
            await _taskRepository.DeleteTask(id);
        }

        public async Task<List<Tasks>> GetAllForUser(string id)
        {
            return await _taskRepository.GetAllForUser(id);
        }

        public async Task<List<SubTasks>> GetSubTasksForTask(int id)
        {
            return await _subTaskRepository.GetAllForTask(id);
        }

        public async Task<Tasks> GetTaskById(int id)
        {
            return await _taskRepository.GetTaskById(id);
        }

        public async Task UpdateSubTask(SubTasks subTasks)
        {
            await _subTaskRepository.UpdateSubTask(subTasks);
        }

        public async Task UpdateTask(Tasks tasks)
        {
            await _taskRepository.UpdateTask(tasks);
        }

        
    }
}
