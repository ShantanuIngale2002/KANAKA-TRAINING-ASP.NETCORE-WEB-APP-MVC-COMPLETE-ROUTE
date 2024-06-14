using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EmployeeTaskCore.Data
{
    public class LeaveHistory
    {
        [Key]
        [Column("emp_id")]
        public int EmployeeId { get; set; }

        [Column("start_date")]
        public string StartDate { get; set; } = string.Empty;

        [Column("end_date")]
        public string EndDate { get; set; } = string.Empty;

        [Column("reason")]
        public string Reason { get; set; } = string.Empty;

        [Column("type")]
        public string Type { get; set; } = string.Empty;
    }
}
