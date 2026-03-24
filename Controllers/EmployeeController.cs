using EmployeeManagementAPI.Data;
using EmployeeManagementAPI.Models;
using EmployeeManagementAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _service;

        public EmployeeController(IEmployeeService service) {
            _service = service;
        }

        [Authorize(Roles = "HR")]
        [HttpGet]
        public IActionResult GetAllEmployees()
        {
            var employees = _service.GetAllEmployees();
            return Ok(employees);
        }

        [HttpGet("{id}")]
        public IActionResult GetEmployeeById(int id)
        {       
                var employee = _service.GetEmployeeById(id);
            return Ok(new
            {
                success = true,
                data = employee
            });
        }

        [HttpGet("email/{email}")]
        public IActionResult GetEmployeeByEmail(string email)
        {
            var employee = _service.GetEmployeeByEmail(email);
            return Ok(new
            {
                success = true,
                data = employee
            });
        }

        [Authorize(Roles = "HR")]
        [HttpPost]
        public IActionResult CreateEmployee(Employee employee)
        {
            _service.CreateEmployee(employee);
            return Ok(new
            {
                success = true,
                message = "Employee created successfully"
            });
        }

 
        [HttpPut]
        public IActionResult UpdateEmployee([FromBody] Employee employee)
        {
            _service.UpdateEmployee(employee);
            return Ok(new
            {
                success = true,
                message = "Employee updated successfully"
            });
        }

        [Authorize(Roles = "HR")]
        [HttpDelete("{id}")]
        public IActionResult DeleteEmployee(int id)
        {
            _service.DeleteEmployee(id);
            return Ok(new
            {
                success = true,
                message = "Employee deleted successfully"
            });
        }

        [Authorize(Roles = "HR")]
        [HttpPost("{employeeId}/assign/{departmentId}")]
        public IActionResult AssignEmployeeToDepartment(int employeeId, int departmentId)
        {
            _service.AssignEmployeeToDepartment(employeeId, departmentId);
            return Ok(new
            {
                success = true,
                message = $"Employee {employeeId} assigned to Department {departmentId}"
            });
        }

        [HttpGet("test")]
        public IActionResult Test()
        {
            return Ok(new { Message = "JWT is valid!", Email = User.Identity.Name });
        }

    }
}
