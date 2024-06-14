using System.ComponentModel.DataAnnotations;

namespace EmployeeTaskCore.Models
{
    public class EmployeeCredentialModel
    {
        [Key]
        public int EmployeeId { get; set; }

        [Required]
        public string Username { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;
    }
}
