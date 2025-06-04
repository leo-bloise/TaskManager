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
        protected Task<T> Dispatch<T>(IRequest<T> request)
        {
            return _mediator.Send(request);
        }
    }
}