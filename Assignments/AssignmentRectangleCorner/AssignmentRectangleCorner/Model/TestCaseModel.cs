using System.ComponentModel.DataAnnotations;

namespace AssignmentRectangleCorner.Model
{
    public class TestCaseModel
    {
        [Key]
        public int testcaseid { get; set; }
        public int Row { get; set; }
        public int Column { get; set; }
        public string Testcase { get; set; } = string.Empty;
        public float Answer { get; set; } = -404;
    }
}
