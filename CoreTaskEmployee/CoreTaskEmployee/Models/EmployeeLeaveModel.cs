using System.ComponentModel.DataAnnotations;

namespace CoreTaskEmployee.Models
{
    public class EmployeeLeaveModel
    {
        [Key]
        public int EmployeeId { get; set; }

        [Required]
        public int EmployeePaidLeavesTaken { get; set; } = 0;

        //public int EmployeePaidLeavesRemain { get; set; } = 0;

        [Required]
        public int EmployeePaylossLeavesTaken { get; set; } = 0;
    }
}
