using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookLambProject.Migrations
{
    public partial class usersecondmig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_SUser",
                table: "SUser");

            migrationBuilder.RenameTable(
                name: "SUser",
                newName: "User");

            migrationBuilder.AddPrimaryKey(
                name: "PK_User",
                table: "User",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_User",
                table: "User");

            migrationBuilder.RenameTable(
                name: "User",
                newName: "SUser");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SUser",
                table: "SUser",
                column: "Id");
        }
    }
}
