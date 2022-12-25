using Microsoft.AspNetCore.Mvc;

namespace AHY.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        [HttpPost]
        public IActionResult Login()
        {
            return Created("", JWTTokenGenerator.GenerateToken());
        }
    }
}
