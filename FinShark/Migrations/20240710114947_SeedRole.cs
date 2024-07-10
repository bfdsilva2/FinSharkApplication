using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinShark.Migrations
{
    public partial class SeedRole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "286cacd0-df42-4032-b625-e6d7e8ee7a26", "91a8ad49-6055-4357-8b5c-ddd51ef04aca", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "90d14292-3acf-4bfb-a417-152130cd2f25", "ac31226b-4c59-46e2-8fff-c5f162d7ae02", "User", "USER" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "286cacd0-df42-4032-b625-e6d7e8ee7a26");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "90d14292-3acf-4bfb-a417-152130cd2f25");
        }
    }
}
