using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AssignmentRectangleCorner.Migrations
{
    /// <inheritdoc />
    public partial class Migration01 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TestCaseModel",
                columns: table => new
                {
                    testcaseid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Row = table.Column<int>(type: "int", nullable: false),
                    Column = table.Column<int>(type: "int", nullable: false),
                    TestCase = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Answer = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestCaseModel", x => x.testcaseid);
                });

            migrationBuilder.CreateTable(
                name: "TestCases",
                columns: table => new
                {
                    testcaseid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    testcase = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    answer = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestCases", x => x.testcaseid);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TestCaseModel");

            migrationBuilder.DropTable(
                name: "TestCases");
        }
    }
}
