using Microsoft.AspNetCore.Identity;

namespace TaskMaster.API.Models
{
    public class UserModel : IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }

        // Navigation property
        public ICollection<TaskModel>? Tasks { get; set; }
    }
}
