using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoreTaskEmployee.Data
{
    public class EmployeeProjectMap
    {
        //public int map_id { get; set; }

        [Key]
        [Column("emp_id")]
        public int EmployeeId { get; set; }

        [Column("project_id")]
        public int ProjectId { get; set; }
    }
}
