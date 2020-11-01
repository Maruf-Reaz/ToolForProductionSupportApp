using Microsoft.EntityFrameworkCore.Migrations;

namespace DecathlonDynamoErpApp.Migrations
{
    public partial class NeuroStormSuperAdminSeeded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FactoryId", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "9cd7ff85-f36c-69b6-ea27-dbb23cbfb508", 0, "62d21881-0b3b-55ab-aa4d-61220ace1e2e", "info@neurostormsoft.com", false, 1, false, null, "INFO@NEUROSTORMSOFT.COM", "NEUROSTORM", "AQAAAAEAACcQAAAAEDNQpE8hqBcgCek1F6WkCX1siCTa4B6dSKD+ZjbxScznzTuQffacsPK9nKL7gK+3DQ==", null, false, "6UEMS5CNA2GYLO2URB5GDOQQI2NI7FIL", false, "NeuroStorm" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "UserId", "RoleId" },
                values: new object[,]
                {
                    { "9cd7ff85-f36c-69b6-ea27-dbb23cbfb508", "f686b56a-6135-4221-a0b0-bdec547e3waw" },
                    { "9cd7ff85-f36c-69b6-ea27-dbb23cbfb508", "m121b57c-9025-9223-e0f0-bdec765e3bgb" },
                    { "9cd7ff85-f36c-69b6-ea27-dbb23cbfb508", "m347b57d-8617-9224-g0h0-bdec765e3lml" },
                    { "9cd7ff85-f36c-69b6-ea27-dbb23cbfb508", "w585b57b-7456-8222-c0d0-bdec765e3awa" },
                    { "9cd7ff85-f36c-69b6-ea27-dbb23cbfb508", "m915b57e-2431-9225-i0j0-bdec765e3pgp" },
                    { "9cd7ff85-f36c-69b6-ea27-dbb23cbfb508", "m173b57f-8519-9226-k0l0-bdec765e3rir" },
                    { "9cd7ff85-f36c-69b6-ea27-dbb23cbfb508", "m354b57g-4013-9227-m0n0-bdec765e3oeo" }
                });

            migrationBuilder.InsertData(
                table: "UserProfiles",
                columns: new[] { "Id", "ApplicationUserId", "City", "Nationality", "PhoneNumber", "PhotoName" },
                values: new object[] { 2, "9cd7ff85-f36c-69b6-ea27-dbb23cbfb508", null, null, null, "No File" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "UserId", "RoleId" },
                keyValues: new object[] { "9cd7ff85-f36c-69b6-ea27-dbb23cbfb508", "f686b56a-6135-4221-a0b0-bdec547e3waw" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "UserId", "RoleId" },
                keyValues: new object[] { "9cd7ff85-f36c-69b6-ea27-dbb23cbfb508", "m121b57c-9025-9223-e0f0-bdec765e3bgb" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "UserId", "RoleId" },
                keyValues: new object[] { "9cd7ff85-f36c-69b6-ea27-dbb23cbfb508", "m173b57f-8519-9226-k0l0-bdec765e3rir" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "UserId", "RoleId" },
                keyValues: new object[] { "9cd7ff85-f36c-69b6-ea27-dbb23cbfb508", "m347b57d-8617-9224-g0h0-bdec765e3lml" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "UserId", "RoleId" },
                keyValues: new object[] { "9cd7ff85-f36c-69b6-ea27-dbb23cbfb508", "m354b57g-4013-9227-m0n0-bdec765e3oeo" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "UserId", "RoleId" },
                keyValues: new object[] { "9cd7ff85-f36c-69b6-ea27-dbb23cbfb508", "m915b57e-2431-9225-i0j0-bdec765e3pgp" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "UserId", "RoleId" },
                keyValues: new object[] { "9cd7ff85-f36c-69b6-ea27-dbb23cbfb508", "w585b57b-7456-8222-c0d0-bdec765e3awa" });

            migrationBuilder.DeleteData(
                table: "UserProfiles",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "9cd7ff85-f36c-69b6-ea27-dbb23cbfb508");
        }
    }
}
