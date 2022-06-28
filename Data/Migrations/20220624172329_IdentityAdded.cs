using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Data.Migrations
{
    public partial class IdentityAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2934d2ae-3501-4e85-97db-214e8135928b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ddd28b22-b400-49b6-b9ae-139eaf515902");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "183e1784-0d8f-4809-b104-f3a71c45c5c3", "e778ff98-55fc-49d3-a15e-f5e6a12c09e2", "Member", "MEMBER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "e4589a33-91cf-4a08-b261-2b4fc264fdb2", "ee868854-7919-4d6a-8f41-8eb7affbf37d", "Admin", "ADMIN" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "183e1784-0d8f-4809-b104-f3a71c45c5c3");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e4589a33-91cf-4a08-b261-2b4fc264fdb2");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "2934d2ae-3501-4e85-97db-214e8135928b", "026e3eb5-2363-45da-a098-2c540dfc0246", "Member", "MEMBER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "ddd28b22-b400-49b6-b9ae-139eaf515902", "14beb4ff-aaa5-4748-bb85-ce3f2701b26d", "Admin", "ADMIN" });
        }
    }
}
