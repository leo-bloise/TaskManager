using MediatR;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Api.Controllers;
using TaskManager.Api.Controllers.DTOs.Input;

namespace TaskManager.Api
{
    public class UsersController : ApiController
    {
        public UsersController(IMediator mediator) : base(mediator) { }
        [HttpPost]
        public CreateUserDTO Create([FromBody] CreateUserDTO payload)
        {
            return payload;
        }
    }
}