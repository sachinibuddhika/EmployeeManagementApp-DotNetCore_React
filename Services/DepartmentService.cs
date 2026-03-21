using EmployeeManagementAPI.Data;
using EmployeeManagementAPI.Models;
using EmployeeManagementAPI.Services.Interfaces;

namespace EmployeeManagementAPI.Services
{
    public class DepartmentService:IDepartmentService
    {
        private readonly DepartmentRepository _repository;

        public DepartmentService(DepartmentRepository repository)
        {
            _repository = repository;
        }

        public List<Department> GetAllDepartments()
        {
            return _repository.GetDepartments();
        }

        public Department GetDepartmentById(int id)
        {
            var department = _repository.GetDepartmentByID(id);

            if (department == null)
                throw new Exception("Department not found");

            return department;
        }

        public void CreateDepartment(Department department)
        {
            _repository.AddDepartment(department);
        }

        public void UpdateDepartment(Department department)
        {
            if (department.DepartmentId <= 0)
                throw new Exception("Invalid department ID");

            _repository.UpdateDepartment(department);
        }

        public void DeleteDepartment(int id)
        {
            _repository.DeleteDepartment(id);
        }
    }
}
