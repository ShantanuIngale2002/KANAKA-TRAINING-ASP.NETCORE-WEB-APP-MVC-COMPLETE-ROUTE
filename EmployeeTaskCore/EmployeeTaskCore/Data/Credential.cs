using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EmployeeTaskCore.Data
{
    public class Credential
    {
        [Key]
        public int admin_id { get; set; }

        [Column("emp_id")]
        public int EmployeeId { get; set; }

        [Column("username")]
        public string Username { get; set; } = string.Empty;

        [Column("password")]
        public string Password { get; set; } = string.Empty;
    }
}
