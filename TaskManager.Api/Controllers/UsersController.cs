using MediatR;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Api.Application.Requests;
using TaskManager.Api.Controllers;
using TaskManager.Api.Controllers.DTOs.Input;
using TaskManager.Api.Controllers.Output;
using TaskManager.Domain;

namespace TaskManager.Api
{
    public class UsersController : ApiController
    {
        public UsersController(IMediator mediator) : base(mediator) { }
        [HttpPost]
        public async Task<UserCreatedDTO> Create([FromBody] CreateUserDTO payload)
        {
            User user = await Dispatch(new CreateUserRequest(
                payload.Username,
                payload.Password,
                payload.Email
            ));
            return new UserCreatedDTO(
                user.Id,
                user.Username,
                user.Email,
                user.CreatedAt,
                user.UpdatedAt
            );
        }
    }
}