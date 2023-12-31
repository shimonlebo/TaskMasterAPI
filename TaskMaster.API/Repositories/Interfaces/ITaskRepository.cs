﻿using TaskMaster.API.Models;

namespace TaskMaster.API.Repositories.Interfaces
{
    public interface ITaskRepository
    {
        Task<IEnumerable<TaskModel>> GetTasks();
        Task<TaskModel> GetTaskById(int id);
        Task CreateTask(TaskModel task);
        Task UpdateTask(TaskModel task);
        Task DeleteTaskById(int id);
    }
}
