using AssignmentRectangleCorner.Data;
using Microsoft.EntityFrameworkCore;
using AssignmentRectangleCorner.Model;

namespace AssignmentRectangleCorner.Repository.EFCore
{
    public class TestCaseDbContext : DbContext
    {
        public TestCaseDbContext(DbContextOptions options) : base(options) { }

        public DbSet<TestCase> TestCases { get; set; }
        public DbSet<AssignmentRectangleCorner.Model.TestCaseModel> TestCaseModel { get; set; } = default!;
    }
}
