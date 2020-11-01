using Microsoft.EntityFrameworkCore.Migrations;

namespace DecathlonDynamoErpApp.Migrations
{
    public partial class UserProfileSeeded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "UserProfiles",
                columns: new[] { "Id", "ApplicationUserId", "City", "Nationality", "PhoneNumber", "PhotoName" },
                values: new object[] { 1, "8ab6ee62-f36c-48b6-ae27-dbb22cbfb507", null, null, null, "No File" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UserProfiles",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
