using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace Data.Context.Migrations
{
    /// <inheritdoc />
    public partial class InitializeDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Credentials",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Password = table.Column<string>(type: "longtext", nullable: false),
                    Salt = table.Column<string>(type: "longtext", nullable: false),
                    RefreshToken = table.Column<string>(type: "longtext", nullable: true),
                    ExpiresAt = table.Column<string>(type: "longtext", nullable: false),
                    FailedLogins = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "longtext", nullable: true),
                    CreatedAt = table.Column<string>(type: "longtext", nullable: true),
                    UpdatedBy = table.Column<string>(type: "longtext", nullable: true),
                    UpdatedAt = table.Column<string>(type: "longtext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Credentials", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AppUser",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: false),
                    FirstName = table.Column<string>(type: "longtext", nullable: false),
                    Email = table.Column<string>(type: "longtext", nullable: false),
                    DateOfBirth = table.Column<string>(type: "longtext", nullable: false),
                    UserRole = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CredentialsId = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "longtext", nullable: true),
                    CreatedAt = table.Column<string>(type: "longtext", nullable: true),
                    UpdatedBy = table.Column<string>(type: "longtext", nullable: true),
                    UpdatedAt = table.Column<string>(type: "longtext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUser", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppUser_Credentials_CredentialsId",
                        column: x => x.CredentialsId,
                        principalTable: "Credentials",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.InsertData(
                table: "Credentials",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "ExpiresAt", "FailedLogins", "Password", "RefreshToken", "Salt", "UpdatedAt", "UpdatedBy" },
                values: new object[] { 1, "2024-10-31", "System", "", 0, "U3VwZXJTZWNyZXQ2MjhhYzEwYi05NGZlLTQ4MDUtYjE2MS04NTYxOGMzZjVkODc=", "", "628ac10b-94fe-4805-b161-85618c3f5d87", "2024-10-31", "System" });

            migrationBuilder.InsertData(
                table: "AppUser",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "CredentialsId", "DateOfBirth", "Email", "FirstName", "IsActive", "Name", "UpdatedAt", "UpdatedBy", "UserRole" },
                values: new object[] { 1, "2024-10-31", "System", 1, "01.01.2010", "test@app.com", "", true, "", null, null, 1 });

            migrationBuilder.CreateIndex(
                name: "IX_AppUser_CredentialsId",
                table: "AppUser",
                column: "CredentialsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppUser");

            migrationBuilder.DropTable(
                name: "Credentials");
        }
    }
}
