using Microsoft.EntityFrameworkCore;
using TaskManager.Models.Entities;

namespace TaskManager.Infra;

public class TaskManagerDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Models.Entities.Task> Tasks { get; set; }
    public DbSet<Category> Categories { get; set; }
    public TaskManagerDbContext(DbContextOptions<TaskManagerDbContext> options) : base(options) { }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        foreach(var entityType in modelBuilder.Model.GetEntityTypes())
        {
            if(typeof(BaseEntity).IsAssignableFrom(entityType.ClrType))
            {
                modelBuilder.Entity(entityType.ClrType).Property(nameof(BaseEntity.CreatedAt))
                .HasColumnType("timestamp without time zone");

                modelBuilder.Entity(entityType.ClrType).Property(nameof(BaseEntity.UpdatedAt))
                    .HasColumnType("timestamp without time zone");
            }
        }
    }
}