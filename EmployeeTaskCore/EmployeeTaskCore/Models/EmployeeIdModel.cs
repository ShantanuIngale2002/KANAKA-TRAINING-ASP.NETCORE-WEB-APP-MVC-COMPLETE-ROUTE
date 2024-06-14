using System.ComponentModel.DataAnnotations;

namespace CoreTaskEmployee.Models
{
    public class EmployeeIdModel
    {
        [Key]
        public int EmployeeId { get; set; }

        [Required]
        public int empID { get; set; }
    }
}
