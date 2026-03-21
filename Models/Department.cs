using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeManagementAPI.Models
{
    public class Department
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DepartmentId { get; set; }

        [Required,MaxLength(10)]
        public string DepartmentCode { get; set; }=string.Empty;

        [Required, MaxLength(50)]
        public string DepartmentName { get; set; } = string.Empty;
        [Required]
        public DateTime CreatedDate { get; set; }
        [Required]
        public DateTime ModifiedDate { get; set; }

        public List<Employee> Employees { get; set; } = new List<Employee>();

    }
}
