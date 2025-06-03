using Microsoft.AspNetCore.Mvc;
using MediatR;

namespace TaskManager.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public abstract class ApiController : ControllerBase
    {
        private IMediator _mediator;
        public ApiController(IMediator mediator)
        {
            _mediator = mediator;
        }
        protected void Dispatch(IRequest request)
        {
            _mediator.Send(request);
        }
    }
}