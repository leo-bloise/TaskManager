using Microsoft.EntityFrameworkCore;
using TaskManager.Models.Entities;

namespace TaskManager.Infra;

public class TaskManagerDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public TaskManagerDbContext(DbContextOptions<TaskManagerDbContext> options) : base(options) { }
}