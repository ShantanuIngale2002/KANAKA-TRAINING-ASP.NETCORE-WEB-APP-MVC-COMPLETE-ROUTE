using System.ComponentModel.DataAnnotations;

namespace EmployeeTaskCore.Models
{
    public class EmployeeLeaveHistoryModel
    {
        [Key]
        public int EmployeeId { get; set; }

        [Required]
        public string StartDate { get; set; } = string.Empty;

        [Required]
        public string EndDate { get; set; } = string.Empty;

        [Required]
        public string Reason { get; set; } = string.Empty;

        [Required]
        public string Type { get; set; } = string.Empty;
    }
}
