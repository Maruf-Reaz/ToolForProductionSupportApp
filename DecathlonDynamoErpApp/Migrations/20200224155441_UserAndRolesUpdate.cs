using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DecathlonDynamoErpApp.Migrations
{
    public partial class UserAndRolesUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "UserId", "RoleId" },
                keyValues: new object[] { "8ab6ee62-f36c-48b6-ae27-dbb22cbfb507", "m121b57c-9025-9223-e0f0-bdec765e3bgb" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "UserId", "RoleId" },
                keyValues: new object[] { "8ab6ee62-f36c-48b6-ae27-dbb22cbfb507", "m173b57f-8519-9226-k0l0-bdec765e3rir" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "UserId", "RoleId" },
                keyValues: new object[] { "8ab6ee62-f36c-48b6-ae27-dbb22cbfb507", "m347b57d-8617-9224-g0h0-bdec765e3lml" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "UserId", "RoleId" },
                keyValues: new object[] { "8ab6ee62-f36c-48b6-ae27-dbb22cbfb507", "m354b57g-4013-9227-m0n0-bdec765e3oeo" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "UserId", "RoleId" },
                keyValues: new object[] { "8ab6ee62-f36c-48b6-ae27-dbb22cbfb507", "m915b57e-2431-9225-i0j0-bdec765e3pgp" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "UserId", "RoleId" },
                keyValues: new object[] { "8ab6ee62-f36c-48b6-ae27-dbb22cbfb507", "w585b57b-7456-8222-c0d0-bdec765e3awa" });

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

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "CreatedOn", "Description", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "e858c58l-3255-9892-c5d5-bdec765e3pip", "aar8b4b9j-3n6o-3495-73e1-4fd157e23g17", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Has Several Permissions", "MAFUserOfArchive", "MAFUSEROFARCHIVE" },
                    { "a212b59c-5213-4003-e5f5-bdec785e4lil", "aar5a3b0g-9c5g-9130-99e1-4fd155e23i18", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Has Several Permissions", "KSIUserOfArchive", "KSIUSEROFARCHIVE" },
                    { "g734b69c-9465-6115-g5h5-bdec785e4imi", "aar5a3b0g-8d4j-2188-95e1-4fd156e23i19", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Has Several Permissions", "YSSUserOfArchive", "YSSUSEROFARCHIVE" },
                    { "z323b59c-5213-5348-i5j5-bdec735e4gpg", "aar5a3b0g-7c3p-4521-45e1-4fd158e23i20", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Has Several Permissions", "RFLUserOfArchive", "RFLUSEROFARCHIVE" },
                    { "x212b59c-3273-1670-k5l5-bdec785e4gig", "aar5a3b1g-6c2g-3258-93e1-4fd159e23i18", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Has Several Permissions", "APEXUserOfArchive", "APEXUSEROFARCHIVE" },
                    { "v212b59c-6352-6125-m5n5-bdec785e4iki", "aar5a3b1g-5c2g-6516-81e1-4fd160e23i18", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Has Several Permissions", "EDISONUserOfArchive", "EDISONUSEROFARCHIVE" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a212b59c-5213-4003-e5f5-bdec785e4lil");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e858c58l-3255-9892-c5d5-bdec765e3pip");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "g734b69c-9465-6115-g5h5-bdec785e4imi");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "v212b59c-6352-6125-m5n5-bdec785e4iki");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "x212b59c-3273-1670-k5l5-bdec785e4gig");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "z323b59c-5213-5348-i5j5-bdec735e4gpg");

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "UserId", "RoleId" },
                values: new object[,]
                {
                    { "8ab6ee62-f36c-48b6-ae27-dbb22cbfb507", "m121b57c-9025-9223-e0f0-bdec765e3bgb" },
                    { "8ab6ee62-f36c-48b6-ae27-dbb22cbfb507", "m347b57d-8617-9224-g0h0-bdec765e3lml" },
                    { "8ab6ee62-f36c-48b6-ae27-dbb22cbfb507", "w585b57b-7456-8222-c0d0-bdec765e3awa" },
                    { "8ab6ee62-f36c-48b6-ae27-dbb22cbfb507", "m915b57e-2431-9225-i0j0-bdec765e3pgp" },
                    { "8ab6ee62-f36c-48b6-ae27-dbb22cbfb507", "m173b57f-8519-9226-k0l0-bdec765e3rir" },
                    { "8ab6ee62-f36c-48b6-ae27-dbb22cbfb507", "m354b57g-4013-9227-m0n0-bdec765e3oeo" },
                    { "9cd7ff85-f36c-69b6-ea27-dbb23cbfb508", "m121b57c-9025-9223-e0f0-bdec765e3bgb" },
                    { "9cd7ff85-f36c-69b6-ea27-dbb23cbfb508", "m347b57d-8617-9224-g0h0-bdec765e3lml" },
                    { "9cd7ff85-f36c-69b6-ea27-dbb23cbfb508", "w585b57b-7456-8222-c0d0-bdec765e3awa" },
                    { "9cd7ff85-f36c-69b6-ea27-dbb23cbfb508", "m915b57e-2431-9225-i0j0-bdec765e3pgp" },
                    { "9cd7ff85-f36c-69b6-ea27-dbb23cbfb508", "m173b57f-8519-9226-k0l0-bdec765e3rir" },
                    { "9cd7ff85-f36c-69b6-ea27-dbb23cbfb508", "m354b57g-4013-9227-m0n0-bdec765e3oeo" }
                });
        }
    }
}
