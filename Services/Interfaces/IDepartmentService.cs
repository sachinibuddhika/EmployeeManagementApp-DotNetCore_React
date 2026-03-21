using EmployeeManagementAPI.Models;

namespace EmployeeManagementAPI.Services.Interfaces
{
    public interface IDepartmentService
    {

        List<Department> GetAllDepartments();
        Department GetDepartmentById(int id);
        void CreateDepartment(Department department);
        void UpdateDepartment(Department department);
        void DeleteDepartment(int id);
    }
}
