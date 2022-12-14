using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SantasWishlist_Data.Migrations
{
    public partial class santa : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "2f7b2762-9e9b-4c62-abd6-825aabdd591f");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "5e9ddebf-3c11-4583-ace2-0d8491b3eb0b");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "2978d67b-9e31-4572-989c-ad33534b31cd", 0, "a0cf8a16-c19d-40df-ab1f-2a045915f3be", null, false, false, null, null, null, null, null, false, "f88e4e6a-37a2-4b27-81f5-c94007c8e9ba", false, "Santa" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2978d67b-9e31-4572-989c-ad33534b31cd");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "e6a2c529-5916-4b4e-9784-b8253f3ec94f");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "83e291af-cedd-4d80-8b09-73943499ca4c");
        }
    }
}
