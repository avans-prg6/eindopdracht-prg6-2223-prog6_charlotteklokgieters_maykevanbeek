using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SantasWishlist_Data.Migrations
{
    public partial class santa2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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
                value: "7c24ab53-dc0e-4cba-a165-727546006c40");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "2b74e0e0-878f-46b6-9d41-a3369ba27f16");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "9e7e0c37-e0ff-4772-855a-6b5b918c6cee", 0, "9a81ed8f-be99-4358-9e09-614f85d5f47c", null, false, false, null, null, "SANTA", "AQAAAAEAACcQAAAAEOcjEKakCftwFXZSQXWmCInx6SoC+yqnVWB7gZO7x4fqCyvPZGMbGhg1Hn39Y15LWA==", null, false, "62d7ce2c-2baa-4d28-be9c-0417a4a8073b", false, "Santa" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "9e7e0c37-e0ff-4772-855a-6b5b918c6cee");

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
    }
}
