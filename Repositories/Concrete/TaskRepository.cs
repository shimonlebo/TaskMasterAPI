using Microsoft.EntityFrameworkCore;
using TaskMasterAPI.Data;
using TaskMasterAPI.Models;
using TaskMasterAPI.Repositories.Interfaces;

namespace TaskMasterAPI.Repositories.Concrete
{
    public class TaskRepository : ITaskRepository
    {
        private readonly TaskDbContext _context;

        public TaskRepository(TaskDbContext context)
        {
            _context = context;
        }

        public async Task CreateTask(TaskModel task)
        {
            await _context.Tasks.AddAsync(task);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<TaskModel>> GetTasks()
        {
            return await _context.Tasks.ToListAsync();
        }

        public async Task<TaskModel> GetTaskById(int id)
        {
            var task = await _context.Tasks.FindAsync(id);

            if (task is null) throw new KeyNotFoundException($"Task with id {id} does not exist.");

            return task;
        }

        public async Task UpdateTask(TaskModel task)
        {
            _context.Tasks.Update(task);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteTaskById(int id)
        { 
            var task = await _context.Tasks.FindAsync(id);
            if (task != null)
            {
                _context.Tasks.Remove(task);
                await _context.SaveChangesAsync();
            }        
        }
    }
}
