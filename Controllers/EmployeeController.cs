using EmployeeManagementAPI.Data;
using EmployeeManagementAPI.Models;
using EmployeeManagementAPI.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _service;

        public EmployeeController(IEmployeeService service) {
            _service = service;
        }

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
                return Ok(employee);           
        }

        [HttpPost]
        public IActionResult CreateEmployee(Employee employee)
        {
            _service.CreateEmployee(employee);
            return Ok("Employee created successfully");
        }


        [HttpPut]
        public IActionResult UpdateEmployee(Employee employee)
        {
            _service.UpdateEmployee(employee);
            return Ok("Employee updated successfully");
        }


        [HttpDelete("{id}")]
        public IActionResult DeleteEmployee(int id)
        {
            _service.DeleteEmployee(id);
            return Ok("Employee deleted successfully");
        }
    }
}
