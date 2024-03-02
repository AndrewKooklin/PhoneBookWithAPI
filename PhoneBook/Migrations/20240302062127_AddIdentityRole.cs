using Microsoft.EntityFrameworkCore.Migrations;

namespace PhoneBook.Migrations
{
    public partial class AddIdentityRole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "c38e4f0a-51d7-42b8-a90c-297990cab76c", "600b4457-e84c-4fc6-a91b-e3b551d56eca", "Counter", "COUNTER" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c38e4f0a-51d7-42b8-a90c-297990cab76c");
        }
    }
}
