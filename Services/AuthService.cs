using EmployeeManagementAPI.Data;
using EmployeeManagementAPI.Models;
using EmployeeManagementAPI.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EmployeeManagementAPI.Services
{
    public class AuthService:IAuthService
    {
        private readonly EmployeeRepository _employeeRepo;
        private readonly IConfiguration _config;

        public AuthService(EmployeeRepository employeeRepo, IConfiguration config)
        {
            _employeeRepo = employeeRepo;
            _config = config;
        }

        public string Authenticate(string email, string password)
        {
            var user= _employeeRepo.GetEmployeeByEmail(email);

            if(user==null || !BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
            {
                return null;
            }
            return GenerateJwtToken(user);
        }

        private string GenerateJwtToken(Employee user)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.Email),
                new Claim(ClaimTypes.Role, user.Role),
                 new Claim("EmployeeId", user.EmployeeId.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(double.Parse(_config["Jwt:ExpiryHours"])),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
