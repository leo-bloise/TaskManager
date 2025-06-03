using MediatR;
using TaskManager.Domain;

namespace TaskManager.Api.Application.Requests
{
    public record CreateUserRequest(
        string Username,
        string Password,
        string Email       
    ) : IRequest<User> {}
}