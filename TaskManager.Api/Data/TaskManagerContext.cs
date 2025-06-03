using Microsoft.EntityFrameworkCore;
using TaskManager.Domain;

namespace TaskManager.Data
{
    public class TaskManagerContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public TaskManagerContext(DbContextOptions<TaskManagerContext> options) : base(options) { }
    }
}