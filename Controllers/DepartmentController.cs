using EmployeeManagementAPI.Models;
using EmployeeManagementAPI.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentService _service;

        public DepartmentController(IDepartmentService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult GetAllDepartments()
        {
            var departments = _service.GetAllDepartments();
            return Ok(departments);
        }

        [HttpGet("{id}")]
        public IActionResult GetDepartmentById(int id)
        {
            var department = _service.GetDepartmentById(id);
            return Ok(department);
        }

        [HttpPost]
        public IActionResult CreateDepartment(Department department)
        {
            _service.CreateDepartment(department);
            return Ok("Department created successfully");
        }


        [HttpPut]
        public IActionResult UpdateDepartment(Department department)
        {
            _service.UpdateDepartment(department);
            return Ok("Department updated successfully");
        }


        [HttpDelete("{id}")]
        public IActionResult DeleteDepartment(int id)
        {
            _service.DeleteDepartment(id);
            return Ok("Department deleted successfully");
        }
    }
}
