using TaskMasterAPI.Models;

namespace TaskMasterAPI.Repositories.Interfaces
{
    public interface ITaskRepository
    {
        Task<IEnumerable<TaskModel>> GetTasks();
        Task<TaskModel> GetTaskById(int id);
        Task CreateTask(TaskModel task);
        Task UpdateTask(int id, TaskModel task);
        Task DeleteTaskById(int id);
    }
}
