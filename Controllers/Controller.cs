using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace TaskManager.Controllers;

[ApiController]
[Route("[controller]")]
public abstract class Controller : ControllerBase
{
    protected int? GetIdFromUser()
    {
        var user = HttpContext.User;
        if (user == null)
        {
            return null;
        }
        var identity = user.Identity;
        if (identity == null) return null;
        if (!identity.IsAuthenticated)
        {
            return null;
        }
        var userId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (!int.TryParse(userId, out int id))
        {
            return null;
        }
        return id;
    }
}