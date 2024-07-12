using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Eazy.Credit.Security.Persistence.Data.Migrations
{
    /// <inheritdoc />
    public partial class TransHeaderStage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_CreditTran",
                schema: "dbo",
                table: "CreditTran");

            migrationBuilder.RenameTable(
                name: "CreditTran",
                schema: "dbo",
                newName: "CreditTrans",
                newSchema: "dbo");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CreditTrans",
                schema: "dbo",
                table: "CreditTrans",
                column: "TransId");

            migrationBuilder.CreateTable(
                name: "ActionTakenStatus",
                schema: "dbo",
                columns: table => new
                {
                    ActionCode = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ActionMessage = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActionTakenStatus", x => x.ActionCode);
                });

            migrationBuilder.CreateTable(
                name: "CreditSchedulesLinesIntTemp",
                schema: "dbo",
                columns: table => new
                {
                    Sequence = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TransID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InterestDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ValueDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Days = table.Column<int>(type: "int", nullable: false),
                    Rate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Interest = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CreditSchedulesLinesIntTemp", x => x.Sequence);
                });

            migrationBuilder.CreateTable(
                name: "TransHeaderStages",
                schema: "dbo",
                columns: table => new
                {
                    TransID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Workflow = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Workflowlevel = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TransDesc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TransStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TransComment = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AddedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransHeaderStages", x => new { x.TransID, x.Workflow, x.Workflowlevel });
                });

            migrationBuilder.CreateTable(
                name: "TransHeaderStatus",
                schema: "dbo",
                columns: table => new
                {
                    StatusCode = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    StatusMessage = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransHeaderStatus", x => x.StatusCode);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActionTakenStatus",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "CreditSchedulesLinesIntTemp",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "TransHeaderStages",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "TransHeaderStatus",
                schema: "dbo");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CreditTrans",
                schema: "dbo",
                table: "CreditTrans");

            migrationBuilder.RenameTable(
                name: "CreditTrans",
                schema: "dbo",
                newName: "CreditTran",
                newSchema: "dbo");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CreditTran",
                schema: "dbo",
                table: "CreditTran",
                column: "TransId");
        }
    }
}
