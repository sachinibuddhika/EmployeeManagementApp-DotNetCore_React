using EmployeeManagementAPI.Models;

namespace EmployeeManagementAPI.Services.Interfaces
{
    public interface IEmployeeService
    {
        List<Employee> GetAllEmployees();
        Employee GetEmployeeById(int id);
        Employee GetEmployeeByEmail(string email);
        void CreateEmployee(Employee employee);
        void UpdateEmployee(Employee employee);
        void DeleteEmployee(int id);
        void AssignEmployeeToDepartment(int employeeId, int departmentId);
  
    }
}
