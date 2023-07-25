using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskMasterAPI.Models
{
    public class TaskModel
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public required string Title { get; set; }
        public string? Description { get; set; }
        public bool IsComplete { get; set; }

        // Navigation property
        public string? UserId { get; set; }
        public UserModel? User { get; set; }

    }
}
