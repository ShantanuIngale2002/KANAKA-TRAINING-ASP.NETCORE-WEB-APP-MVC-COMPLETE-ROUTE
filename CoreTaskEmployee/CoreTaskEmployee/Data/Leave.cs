using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoreTaskEmployee.Data
{
    public class Leave
    {

        [Key]
        [Column("emp_id")]
        public int EmployeeId { get; set; }

        [Column("paid_leaves_taken")]
        public int PaidLeavesTaken { get; set; }

        [Column("paid_leaves_remain")]
        public int PaidLeavesRemaining { get; set; }

        [Column("losspay_leaves_taken")]
        public int PaylossLeavesTaken { get; set; }
    }
}
