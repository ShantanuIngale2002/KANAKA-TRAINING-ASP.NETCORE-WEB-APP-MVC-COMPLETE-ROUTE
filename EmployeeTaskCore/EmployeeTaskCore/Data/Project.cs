using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoreTaskEmployee.Data
{
    public class Project
    {
        [Key]
        [Column("project_id")]
        public int ProjectId { get; set; }

        [Column("project_name")]
        public string ProjectName { get; set; } = string.Empty;

        [Column("start_date")]
        public string StartDate { get; set; } = string.Empty;

        [Column("due_date")]
        public string EndDate { get; set; } = string.Empty;

        [Column("resources")]
        public int ResourcesCount { get; set; }
    }
}
