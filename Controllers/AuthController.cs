using EmployeeManagementAPI.Models;
using EmployeeManagementAPI.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace EmployeeManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IEmployeeService _employeeService;

        public AuthController(IAuthService authService,IEmployeeService employeeService)
        {
            _authService = authService;
            _employeeService = employeeService;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] Login request)
        {

            var user = _employeeService.GetEmployeeByEmail(request.Email);
            Console.WriteLine(user);
            if (user == null) 
            {
                return Unauthorized(new { success = false, error = "Invalid email or password" });
            }

            var token = _authService.Authenticate(request.Email, request.Password);

            if (token == null)
            {
                return Unauthorized(new { success = false, error = "Invalid email or password" });
            }

            return Ok(new
            {
                success = true,
                token = token,
                user = new
                {
                    id=user.EmployeeId,
                    fname=user.FirstName,
                    lname=user.LastName,
                    email = user.Email,
                    role = user.Role
                }

            });
        }


    }
}
