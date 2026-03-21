using EmployeeManagementAPI.Models;
using Microsoft.Data.SqlClient;

namespace EmployeeManagementAPI.Data
{
    public class DepartmentRepository
    {
        private readonly DatabaseHelper _dbHelper;

        public DepartmentRepository(DatabaseHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }

        public List<Department> GetDepartments()
        {
            List<Department> departments = new List<Department>();

            using SqlConnection connection = _dbHelper.GetConnection();
            using SqlCommand cmd = new("spGetAllDepartments", connection);

            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            connection.Open();

            using SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                departments.Add(new Department
                {
                    DepartmentId = (int)reader["DepartmentID"],
                    DepartmentCode = (string)reader["DepartmentCode"],
                    DepartmentName = (string)reader["DepartmentName"],
                    CreatedDate = (DateTime)reader["CreatedDate"],
                    ModifiedDate = (DateTime)reader["ModifiedDate"]
                });
            }

            return departments;
        }


        public void AddDepartment(Department department)
        {
            using SqlConnection connection = _dbHelper.GetConnection();
            using SqlCommand cmd = new("spInsertDepartment", connection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@DepartmentCode", department.DepartmentCode);
            cmd.Parameters.AddWithValue("@DepartmentName", department.DepartmentName);      

            connection.Open();
            cmd.ExecuteNonQuery();
        }


        public void UpdateDepartment(Department department)
        {
            using SqlConnection connection = _dbHelper.GetConnection();
            using SqlCommand cmd = new("spUpdateDepartment", connection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@ID", department.DepartmentId);
            cmd.Parameters.AddWithValue("@DepartmentCode", department.DepartmentCode);
            cmd.Parameters.AddWithValue("@DepartmentName", department.DepartmentName);

            connection.Open();
            cmd.ExecuteNonQuery();
        }

        public void DeleteDepartment(int Id)
        {
            using SqlConnection connection = _dbHelper.GetConnection();
            using SqlCommand cmd = new("spDeleteDepartment", connection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@ID", Id);

            connection.Open();
            cmd.ExecuteNonQuery();
        }

        public Department GetDepartmentByID(int Id)
        {
            Department department = null;

            using SqlConnection connection = _dbHelper.GetConnection();
            using SqlCommand cmd = new("spGetDepartmentById", connection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@ID", Id);

            connection.Open();
            using SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                department = new Department
                {
                    DepartmentId = (int)reader["DepartmentID"],
                    DepartmentCode = (string)reader["DepartmentCode"],
                    DepartmentName = (string)reader["DepartmentName"],
                    CreatedDate = (DateTime)reader["CreatedDate"],
                    ModifiedDate = (DateTime)reader["ModifiedDate"]
                };
            }

            return department;
        }
    }
}
