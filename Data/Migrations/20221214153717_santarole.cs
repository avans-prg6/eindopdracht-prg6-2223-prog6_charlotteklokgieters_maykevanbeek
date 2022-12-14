using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SantasWishlist_Data.Migrations
{
    public partial class santarole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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
                value: "11cd77c7-ccff-42e8-92e1-39972b96a7d8");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "8f4a8874-aa05-4559-a3a8-e89bfd79e4a0");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "ec47a3cc-b70e-479c-87ed-ae1a8a289a91", 0, "8c727ea2-6136-47b7-ab7d-0d979053028e", null, false, false, null, null, "SANTA", "AQAAAAEAACcQAAAAEO0o04fxvtlJ5ZtmJR0Jl3EML/2fKozxFGLD1sdZiR0gFGVqB0qmEDL1MUj7CoYrbw==", null, false, "2da2631d-82f6-4999-93c1-48cd6d3c56e1", false, "Santa" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "1", "ec47a3cc-b70e-479c-87ed-ae1a8a289a91" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "1", "ec47a3cc-b70e-479c-87ed-ae1a8a289a91" });

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "ec47a3cc-b70e-479c-87ed-ae1a8a289a91");

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
    }
}
