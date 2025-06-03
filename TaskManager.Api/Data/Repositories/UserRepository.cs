using TaskManager.Data;
using TaskManager.Data.Repositories;
using TaskManager.Domain;

namespace TaskManager.Api.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private TaskManagerContext _taskManagerContext;
        public UserRepository(TaskManagerContext taskManagerContext)
        {
            _taskManagerContext = taskManagerContext;
        }
        public User Create(User entity)
        {
            _taskManagerContext.Add(entity);
            _taskManagerContext.SaveChanges();
            return entity;
        }
        public bool ExistsByUsernameOrEmail(string username, string email)
        {
            return _taskManagerContext.Users.Any(user => user.Username.Equals(username) || user.Email.Equals(email));
        }
    }
}