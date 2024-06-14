using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoreTaskEmployee.Data
{
    public class Employee
    {
        [Key]
        [Column("emp_id")]
        public int EmployeeId { get; set; }

        [Column("emp_name")]
        public string EmployeeName { get; set; } = string.Empty;

        [Column("emp_dob")]
        public string EmployeeDOB { get; set; } = string.Empty;

        [Column("emp_doj")]
        public string EmployeeDOJ { get; set; } = string.Empty;

        [Column("emp_city")]
        public string EmployeeCity { get; set; } = string.Empty;

        [Column("emp_state")]
        public string EmployeeState { get; set; } = string.Empty;

        [Column("emp_country")]
        public string EmployeeCountry { get; set; } = string.Empty;

        [Column("emp_bloodgroup")]
        public string EmployeeBloodGroup { get; set; } = string.Empty;

        [Column("emp_contact")]
        public string EmployeeContact { get; set; } = string.Empty;
    }
}
