using MediatR;
using Microsoft.AspNetCore.Identity;
using TaskManager.Api.Application.Requests;
using TaskManager.Data.Repositories;
using TaskManager.Domain;

namespace TaskManager.Api.Application.Handlers
{
    public class CreateUserHandler : IRequestHandler<CreateUserRequest, User>
    {
        private IUserRepository _userRepository;
        private IPasswordHasher<User> _passwordHasher;
        public CreateUserHandler(IUserRepository userRepository, IPasswordHasher<User> passwordHasher)
        {
            _passwordHasher = passwordHasher;
            _userRepository = userRepository;
        }
        public Task<User> Handle(CreateUserRequest request, CancellationToken cancellationToken)
        {
            if (_userRepository.ExistsByUsernameOrEmail(request.Username, request.Email))
            {
                throw new ApplicationException($"Username [{request.Username}] or email [{request.Email}] already taken");
            }
            var user = new User(
                request.Username,
                request.Password,
                request.Email
            );
            user.Password = _passwordHasher.HashPassword(user, user.Password);
            return Task.FromResult(_userRepository.Create(user));
        }
    }
}