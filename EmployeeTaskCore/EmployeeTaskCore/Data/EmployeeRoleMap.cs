using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoreTaskEmployee.Data
{
    public class EmployeeRoleMap
    {
        //public int map_id { get; set; }

        [Key]
        [Column("emp_id")]
        public int EmployeeId { get; set; }

        [Column("role_id")]
        public int RoleId { get; set; }
    }
}
