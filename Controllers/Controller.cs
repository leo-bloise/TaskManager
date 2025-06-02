using Microsoft.AspNetCore.Mvc;

namespace TaskManager
{
    [ApiController]
    [Route("[controller]")]
    public abstract class Controller : ControllerBase { }
}