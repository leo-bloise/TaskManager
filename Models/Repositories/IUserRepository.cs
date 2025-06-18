using TaskManager.Models.Entities;

namespace TaskManager.Models.Repositories;

public interface IUserRepository
{
    public bool ExistsByUsername(string username);
    public bool ExistsByEmail(string email);
    public User? FindByUsername(string username);
    public User Create(User user);
    public User? FindById(long id);
}