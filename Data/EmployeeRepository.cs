using EmployeeManagementAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Security.Cryptography.X509Certificates;

namespace EmployeeManagementAPI.Data
{
    public class EmployeeRepository
    {
        private readonly DatabaseHelper _dbHelper;

        public EmployeeRepository(DatabaseHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }

        public List<Employee> GetEmployees()
        {
            List<Employee> employees = new List<Employee>();

            using SqlConnection connection = _dbHelper.GetConnection();
            using SqlCommand cmd = new("spGetAllEmployees", connection);

            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            connection.Open();

            using SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                employees.Add(new Employee
                {
                    EmployeeId = (int)reader["EmployeeId"],
                    FirstName = (string)reader["FirstName"],
                    LastName = (string)reader["LastName"],
                    Email = (string)reader["Email"],
                    DOB = DateOnly.FromDateTime((DateTime)reader["DOB"]),
                    Age = (int)reader["Age"],
                    Salary = (decimal)reader["Salary"],
                    DepartmentID = (int)reader["DepartmentID"],
                    PasswordHash= (string)reader["Password"],
                    Role= (string)reader["Role"],
                    ContactNo = (string)reader["ContactNo"],
                    CreatedDate = (DateTime)reader["CreatedDate"],
                    ModifiedDate = (DateTime)reader["ModifiedDate"]
                });
            }

            return employees;
        }


        public void AddEmployee(Employee employee)
        {
            using SqlConnection connection = _dbHelper.GetConnection();
            using SqlCommand cmd = new("spInsertEmployee", connection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(employee.PasswordHash);

            cmd.Parameters.AddWithValue("@FirstName", employee.FirstName);
            cmd.Parameters.AddWithValue("@LastName", employee.LastName);
            cmd.Parameters.AddWithValue("@Email", employee.Email);
            cmd.Parameters.AddWithValue("@DOB", employee.DOB.ToDateTime(new TimeOnly(0, 0)));
            cmd.Parameters.AddWithValue("@Age", employee.Age);
            cmd.Parameters.AddWithValue("@Salary", employee.Salary);
            cmd.Parameters.AddWithValue("@DepartmentID", employee.DepartmentID);
            cmd.Parameters.AddWithValue("@Password", hashedPassword);
            cmd.Parameters.AddWithValue("@Role", employee.Role);
            cmd.Parameters.AddWithValue("@ContactNo", employee.ContactNo);

            connection.Open();
            cmd.ExecuteNonQuery();
        }


        public void UpdateEmployee(Employee employee)
        {
            using SqlConnection connection = _dbHelper.GetConnection();
            using SqlCommand cmd = new("spUpdateEmployee", connection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(employee.PasswordHash);

            cmd.Parameters.AddWithValue("@ID", employee.EmployeeId);
            cmd.Parameters.AddWithValue("@FirstName", employee.FirstName);
            cmd.Parameters.AddWithValue("@LastName", employee.LastName);
            cmd.Parameters.AddWithValue("@Email", employee.Email);
            cmd.Parameters.AddWithValue("@DOB", employee.DOB.ToDateTime(new TimeOnly(0, 0)));
            cmd.Parameters.AddWithValue("@Age", employee.Age);
            cmd.Parameters.AddWithValue("@Salary", employee.Salary);
            cmd.Parameters.AddWithValue("@DepartmentID", employee.DepartmentID);
            cmd.Parameters.AddWithValue("@Password", hashedPassword);
            cmd.Parameters.AddWithValue("@Role", employee.Role);
            cmd.Parameters.AddWithValue("@ContactNo", employee.ContactNo);
            connection.Open();
            cmd.ExecuteNonQuery();
        }

        public void DeleteEmployee(int Id)
        {
            using SqlConnection connection = _dbHelper.GetConnection();
            using SqlCommand cmd = new("spDeleteEmployee", connection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@ID",Id);

            connection.Open();
            cmd.ExecuteNonQuery();
        }

        public Employee GetEmployeeByID(int Id)
        {
            Employee employee = null;

            using SqlConnection connection = _dbHelper.GetConnection();
            using SqlCommand cmd = new("spGetEmployeeById", connection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@ID", Id);
            
            connection.Open();
            using SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                employee = new Employee
                {
                    EmployeeId = (int)reader["EmployeeId"],
                    FirstName = (string)reader["FirstName"],
                    LastName = (string)reader["LastName"],
                    Email = (string)reader["Email"],
                    DOB = DateOnly.FromDateTime((DateTime)reader["DOB"]),
                    Age = (int)reader["Age"],
                    Salary = (decimal)reader["Salary"],
                    DepartmentID = (int)reader["DepartmentID"],
                    PasswordHash = (string)reader["Password"],
                    Role = (string)reader["Role"],
                    ContactNo= (string)reader["ContactNo"],
                    CreatedDate = (DateTime)reader["CreatedDate"],
                    ModifiedDate = (DateTime)reader["ModifiedDate"]
                };
            }

            return employee;
        }


        public Employee GetEmployeeByEmail(string email)
        {
            Employee employee = null;
            using SqlConnection connection = _dbHelper.GetConnection();
            using SqlCommand cmd = new("spGetEmployeeByEmail", connection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Email", email);

            connection.Open();
            using SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                employee = new Employee
                {
                    EmployeeId = (int)reader["EmployeeId"],
                    FirstName = (string)reader["FirstName"],
                    LastName = (string)reader["LastName"],
                    Email = (string)reader["Email"],
                    DOB = DateOnly.FromDateTime((DateTime)reader["DOB"]),
                    Age = (int)reader["Age"],
                    Salary = (decimal)reader["Salary"],
                    DepartmentID = (int)reader["DepartmentID"],
                    PasswordHash = (string)reader["Password"],
                    Role = (string)reader["Role"],
                    ContactNo = (string)reader["ContactNo"],
                    CreatedDate = (DateTime)reader["CreatedDate"],
                    ModifiedDate = (DateTime)reader["ModifiedDate"]
                };
            }

            return employee;
        }

        public void AssignEmployeeToDepartment(int employeeId, int departmentId)
        {
            using SqlConnection connection = _dbHelper.GetConnection();
            using SqlCommand cmd = new SqlCommand("spAssignEmployeeToDepartment", connection);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@EmployeeId", employeeId);
            cmd.Parameters.AddWithValue("@DepartmentId", departmentId);

            connection.Open();
            cmd.ExecuteNonQuery();
        }
    }
}
