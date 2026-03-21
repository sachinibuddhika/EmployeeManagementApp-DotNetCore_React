using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeManagementAPI.Models
{
    public class Employee
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EmployeeId { get; set; }

        [Required, MaxLength(50)]
        public string FirstName { get; set; } = string.Empty;

        [Required, MaxLength(50)]
        public string LastName { get; set; } = string.Empty;

        [Required, MaxLength(50)]
        [RegularExpression(@"^\S+@\S+\.\S+$", ErrorMessage = "Email must be in a valid format")]
        public string Email { get; set; } = string.Empty;

        [Required]
        public DateOnly DOB { get; set; }

        [Required]
        public int Age { get; set; }

        [Required]
        public decimal Salary { get; set; }

        [ForeignKey("Department")]
        public int DepartmentID { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        [Required]
        public DateTime ModifiedDate { get; set; }

        [Required]
        public string PasswordHash { get; set; } = string.Empty;

        [Required]
        public string Role { get; set; } = string.Empty;

        [Required]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Contact number must be exactly 10 digits")]
        public string ContactNo { get; set; } = string.Empty;

    }
}
