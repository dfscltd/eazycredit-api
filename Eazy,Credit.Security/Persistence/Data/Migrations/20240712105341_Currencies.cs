using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Eazy.Credit.Security.Persistence.Data.Migrations
{
    /// <inheritdoc />
    public partial class Currencies : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TransStatus",
                schema: "dbo",
                table: "TransHeaderStages",
                newName: "TransCode");

            migrationBuilder.RenameColumn(
                name: "TransID",
                schema: "dbo",
                table: "TransHeaderStages",
                newName: "CreditID");

            migrationBuilder.RenameColumn(
                name: "ActionMessage",
                schema: "dbo",
                table: "ActionTakenStatus",
                newName: "ActionStatus");

            migrationBuilder.AddColumn<string>(
                name: "ActionCode",
                schema: "dbo",
                table: "TransHeaderStages",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "PrmCurrencies",
                schema: "dbo",
                columns: table => new
                {
                    CurrCode = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CurrDesc = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrmCurrencies", x => x.CurrCode);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PrmCurrencies",
                schema: "dbo");

            migrationBuilder.DropColumn(
                name: "ActionCode",
                schema: "dbo",
                table: "TransHeaderStages");

            migrationBuilder.RenameColumn(
                name: "TransCode",
                schema: "dbo",
                table: "TransHeaderStages",
                newName: "TransStatus");

            migrationBuilder.RenameColumn(
                name: "CreditID",
                schema: "dbo",
                table: "TransHeaderStages",
                newName: "TransID");

            migrationBuilder.RenameColumn(
                name: "ActionStatus",
                schema: "dbo",
                table: "ActionTakenStatus",
                newName: "ActionMessage");
        }
    }
}
