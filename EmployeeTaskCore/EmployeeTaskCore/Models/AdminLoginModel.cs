using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EmployeeTaskCore.Models
{
    public class AdminLoginModel
    {
        [Key]
        [Column("emp_id")]
        public int EmployeeId { get; set; }

        [Column("username")]
        public string Username { get; set; } = string.Empty;

        [Column("password")]
        public string Password { get; set; } = string.Empty;
    }
}
