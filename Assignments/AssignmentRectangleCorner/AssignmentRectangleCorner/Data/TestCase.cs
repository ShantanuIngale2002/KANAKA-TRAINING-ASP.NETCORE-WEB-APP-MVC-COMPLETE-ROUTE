using System.ComponentModel.DataAnnotations;

namespace AssignmentRectangleCorner.Data
{
    public class TestCase
    {
        [Key]
        public int testcaseid { get; set; }
        public string testcase { get; set; } = string.Empty;
        public float answer { get; set; } = -1;
    }
}
