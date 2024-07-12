using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Eazy.Credit.Security.Identity.Data.Migrations
{
    /// <inheritdoc />
    public partial class Workflow : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "sec");

            migrationBuilder.CreateTable(
                name: "UserRoles",
                schema: "sec",
                columns: table => new
                {
                    UserRoleID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => x.UserRoleID);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                schema: "sec",
                columns: table => new
                {
                    LoginID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OtherName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShortName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsAdmin = table.Column<bool>(type: "bit", nullable: false),
                    LogInRetries = table.Column<int>(type: "int", nullable: false),
                    UserPhoto = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    LastPasswordChangedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AuthCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasswordExpiration = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PasswordReset = table.Column<bool>(type: "bit", nullable: false),
                    LastLoginDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AddedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Disabled = table.Column<bool>(type: "bit", nullable: false),
                    DisableReason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EnableDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SuccessfulLoginsToday = table.Column<int>(type: "int", nullable: true),
                    PasswordResetCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasswordChangeCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.LoginID);
                });

            migrationBuilder.CreateTable(
                name: "AppRoleClaim",
                schema: "sec",
                columns: table => new
                {
                    RoleClaimID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppRoleClaim", x => x.RoleClaimID);
                    table.ForeignKey(
                        name: "FK_AppRoleClaim_UserRoles_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "sec",
                        principalTable: "UserRoles",
                        principalColumn: "UserRoleID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AppUserClaim",
                schema: "sec",
                columns: table => new
                {
                    UserClaimID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LoginID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUserClaim", x => x.UserClaimID);
                    table.ForeignKey(
                        name: "FK_AppUserClaim_Users_LoginID",
                        column: x => x.LoginID,
                        principalSchema: "sec",
                        principalTable: "Users",
                        principalColumn: "LoginID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AppUserLogin",
                schema: "sec",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUserLogin", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AppUserLogin_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "sec",
                        principalTable: "Users",
                        principalColumn: "LoginID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AppUserToken",
                schema: "sec",
                columns: table => new
                {
                    LoginID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUserToken", x => new { x.LoginID, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AppUserToken_Users_LoginID",
                        column: x => x.LoginID,
                        principalSchema: "sec",
                        principalTable: "Users",
                        principalColumn: "LoginID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRoleUsers",
                schema: "sec",
                columns: table => new
                {
                    LoginID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserRoleID = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoleUsers", x => new { x.UserRoleID, x.LoginID });
                    table.ForeignKey(
                        name: "FK_UserRoleUsers_UserRoles_UserRoleID",
                        column: x => x.UserRoleID,
                        principalSchema: "sec",
                        principalTable: "UserRoles",
                        principalColumn: "UserRoleID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRoleUsers_Users_LoginID",
                        column: x => x.LoginID,
                        principalSchema: "sec",
                        principalTable: "Users",
                        principalColumn: "LoginID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppRoleClaim_RoleId",
                schema: "sec",
                table: "AppRoleClaim",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_AppUserClaim_LoginID",
                schema: "sec",
                table: "AppUserClaim",
                column: "LoginID");

            migrationBuilder.CreateIndex(
                name: "IX_AppUserLogin_UserId",
                schema: "sec",
                table: "AppUserLogin",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                schema: "sec",
                table: "UserRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoleUsers_LoginID",
                schema: "sec",
                table: "UserRoleUsers",
                column: "LoginID");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                schema: "sec",
                table: "Users",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                schema: "sec",
                table: "Users",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppRoleClaim",
                schema: "sec");

            migrationBuilder.DropTable(
                name: "AppUserClaim",
                schema: "sec");

            migrationBuilder.DropTable(
                name: "AppUserLogin",
                schema: "sec");

            migrationBuilder.DropTable(
                name: "AppUserToken",
                schema: "sec");

            migrationBuilder.DropTable(
                name: "UserRoleUsers",
                schema: "sec");

            migrationBuilder.DropTable(
                name: "UserRoles",
                schema: "sec");

            migrationBuilder.DropTable(
                name: "Users",
                schema: "sec");
        }
    }
}
