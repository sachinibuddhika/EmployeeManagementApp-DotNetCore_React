using EmployeeManagementAPI.Data;
using EmployeeManagementAPI.Models;
using EmployeeManagementAPI.Services.Interfaces;

namespace EmployeeManagementAPI.Services
{
    public class EmployeeService:IEmployeeService
    {
        private readonly EmployeeRepository _repository;

        public EmployeeService(EmployeeRepository repository)
        {
            _repository = repository;
        }

        public List<Employee> GetAllEmployees()
        {
            return _repository.GetEmployees();
        }

        public Employee GetEmployeeById(int id)
        {
            var employee = _repository.GetEmployeeByID(id);

            if (employee == null)
                throw new Exception("Employee not found");

            return employee;
        }

        public void CreateEmployee(Employee employee)
        {
            _repository.AddEmployee(employee);
        }

        public void UpdateEmployee(Employee employee)
        {
            if (employee.EmployeeId <= 0)
                throw new Exception("Invalid employee ID");

            _repository.UpdateEmployee(employee);
        }

        public void DeleteEmployee(int id)
        {
            _repository.DeleteEmployee(id);
        }
    }
}
