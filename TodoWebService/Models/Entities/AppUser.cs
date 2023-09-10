using Microsoft.AspNetCore.Identity;

namespace TodoWebService.Models.Entities
{
    public class AppUser : IdentityUser
    {
        public string RefreshToken { get; set; } = string.Empty;
        public DateTime TokenCreated { get; set; }
        public DateTime TokenExpires { get; set; }

        public virtual ICollection<TodoItem> TodoItems { get; set; } = new List<TodoItem>();
    }
}
