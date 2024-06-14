using System.ComponentModel.DataAnnotations;

namespace EmployeeTaskCore.Models
{
    public class EmployeeProjectModel
    {
        [Key]
        [Required]
        public int ProjectId { get; set; }

        [Required]
        public string ProjectName { get; set; } = string.Empty;

        [Required]
        public string StartDate { get; set; } = string.Empty;

        [Required]
        public string DueDate { get; set; } = string.Empty;

        [Required]
        public int Resources { get; set; } = 0;
    }
}
