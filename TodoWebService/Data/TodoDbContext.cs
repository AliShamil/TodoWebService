using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TodoWebService.Models.Entities;

namespace TodoWebService.Data
{
    public class TodoDbContext : IdentityDbContext<AppUser>
    {
        public TodoDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
            base.OnConfiguring(optionsBuilder);
        }

        public DbSet<TodoItem> TodoItems => Set<TodoItem>();
    }
}
