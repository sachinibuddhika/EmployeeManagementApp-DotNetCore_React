using EmployeeManagementAPI.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public IActionResult Login(string email, string password)
        {
            var token = _authService.Authenticate(email, password);
            if (token == null)
            {
                return Unauthorized("Invalid email or password");
            }
            return Ok(new {Token=token});
        }


    }
}
