using TaskManager.Domain;

namespace TaskManager.Data.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        bool ExistsByUsernameOrEmail(string username, string email);
    }
}