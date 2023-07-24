using System.ComponentModel.DataAnnotations;

namespace TaskMasterAPI.Models
{
    public class TaskModel
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public string? Description { get; set; }
        public bool IsComplete { get; set; } = false;

        // Navigation property
        public string? UserId { get; set; }
        public UserModel? User { get; set; }

    }
}
