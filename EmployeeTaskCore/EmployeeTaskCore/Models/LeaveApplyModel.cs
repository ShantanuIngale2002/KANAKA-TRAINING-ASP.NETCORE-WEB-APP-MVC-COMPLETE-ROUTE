using System.ComponentModel.DataAnnotations;

namespace EmployeeTaskCore.Models
{
    public class LeaveApplyModel
    {
        [Key]
        public int EmployeeId { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }

        [Required]
        public string Reason { get; set; } = string.Empty;

        public string AppliedTo { get; set; } = "Manager";
    }
}
