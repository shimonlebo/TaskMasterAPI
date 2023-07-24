using Microsoft.EntityFrameworkCore;

namespace TaskMasterAPI.Data
{
    public class TaskDbContext : DbContext
    {
        public TaskDbContext(DbContextOptions<TaskDbContext> options) : base(options)
        {
        }

        public DbSet<Models.Task> Tasks { get; set; }
        public DbSet<Models.User> Users { get; set; }
    }
}
