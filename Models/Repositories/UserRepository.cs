using Microsoft.EntityFrameworkCore;
using TaskManager.Infra;
using TaskManager.Models.Entities;

namespace TaskManager.Models.Repositories;

public class UserRepository : IUserRepository
{
    private TaskManagerDbContext _taskManagerDbContext;
    public UserRepository(TaskManagerDbContext taskManagerDbContext)
    {
        _taskManagerDbContext = taskManagerDbContext;
    }
    public User Create(User user)
    {
        _taskManagerDbContext.Add(user);
        _taskManagerDbContext.SaveChanges();
        return user;
    }
    public bool ExistsByEmail(string email)
    {
        return _taskManagerDbContext.Users.Any(u => u.Email == email);
    }
    public bool ExistsByUsername(string username)
    {
        return _taskManagerDbContext.Users.Any(u => u.Username == username);
    }
    public User? FindById(long id)
    {
        return _taskManagerDbContext.Users.AsNoTracking().FirstOrDefault(u => u.Id == id);
    }
    public User? FindByUsername(string username)
    {
        return _taskManagerDbContext.Users.AsNoTracking().FirstOrDefault(u => u.Username == username);
    }
}