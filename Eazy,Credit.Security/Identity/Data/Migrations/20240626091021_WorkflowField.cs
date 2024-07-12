using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Eazy.Credit.Security.Identity.Data.Migrations
{
    /// <inheritdoc />
    public partial class WorkflowField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EmployeeID",
                schema: "sec",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmployeeID",
                schema: "sec",
                table: "Users");
        }
    }
}
