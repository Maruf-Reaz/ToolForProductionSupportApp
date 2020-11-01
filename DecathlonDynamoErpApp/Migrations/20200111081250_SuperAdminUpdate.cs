using Microsoft.EntityFrameworkCore.Migrations;

namespace DecathlonDynamoErpApp.Migrations
{
    public partial class SuperAdminUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "UserId", "RoleId" },
                keyValues: new object[] { "133a8219-f819-45a8-b9b5-e7c4ddad8b7c", "m347b57d-8617-9224-g0h0-bdec765e3lml" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "UserId", "RoleId" },
                keyValues: new object[] { "1891b070-42ce-40ad-9c4e-5a47f81dc0ad", "m121b57c-9025-9223-e0f0-bdec765e3bgb" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "UserId", "RoleId" },
                keyValues: new object[] { "7846b041-f643-45a8-b9b5-e7c4ddad8b7c", "m354b57g-4013-9227-m0n0-bdec765e3oeo" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "UserId", "RoleId" },
                keyValues: new object[] { "8231b097-f123-45a8-b9b5-e7c4ddad8b7c", "m915b57e-2431-9225-i0j0-bdec765e3pgp" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "UserId", "RoleId" },
                keyValues: new object[] { "8fg9ww95-f792-45a8-b9b5-e7c4ddad8b7c", "m173b57f-8519-9226-k0l0-bdec765e3rir" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "UserId", "RoleId" },
                keyValues: new object[] { "8lh7oo83-f918-45a8-b9b5-e7c4ddad8b7c", "w585b57b-7456-8222-c0d0-bdec765e3awa" });

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "133a8219-f819-45a8-b9b5-e7c4ddad8b7c");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1891b070-42ce-40ad-9c4e-5a47f81dc0ad");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7846b041-f643-45a8-b9b5-e7c4ddad8b7c");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8231b097-f123-45a8-b9b5-e7c4ddad8b7c");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8fg9ww95-f792-45a8-b9b5-e7c4ddad8b7c");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8lh7oo83-f918-45a8-b9b5-e7c4ddad8b7c");

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
                    { "8ab6ee62-f36c-48b6-ae27-dbb22cbfb507", "m354b57g-4013-9227-m0n0-bdec765e3oeo" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FactoryId", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "1891b070-42ce-40ad-9c4e-5a47f81dc0ad", 0, "504dcd7c-cfdc-49ec-a455-7397cbfcccdc", "monir.hossain@decathlon.com", false, 1, false, null, "MONIR.HOSSAIN@DECATHLON.COM", "SUPERADMINKSI", "AQAAAAEAACcQAAAAEEO6XuvdZ4p/fzatnYaxaH4psRTnOEp3N+Ez6FFxAh1S76HPtt/YScM8RIguweFeoQ==", null, false, "MHNEMETEQV4TNCQ3YRP7WC5X74GNV7HI", false, "SuperAdminKSI" },
                    { "133a8219-f819-45a8-b9b5-e7c4ddad8b7c", 0, "3bee968a-9516-4e3c-9fad-b6d7234d4550", "monir.hossain@decathlon.com", false, 2, false, null, "MONIR.HOSSAIN@DECATHLON.COM", "SUPERADMINYSS", "AQAAAAEAACcQAAAAEEO6XuvdZ4p/fzatnYaxaH4psRTnOEp3N+Ez6FFxAh1S76HPtt/YScM8RIguweFeoQ==", null, false, "WSZPQ45KPNK6QHM5RTUM6OXJAV46LW4P", false, "SuperAdminYSS" },
                    { "8lh7oo83-f918-45a8-b9b5-e7c4ddad8b7c", 0, "3lqq352a-7534-4e3c-9fbc-b6d7234d4660", "monir.hossain@decathlon.com", false, 3, false, null, "MONIR.HOSSAIN@DECATHLON.COM", "SUPERADMINMAF", "AQAAAAEAACcQAAAAEEO6XuvdZ4p/fzatnYaxaH4psRTnOEp3N+Ez6FFxAh1S76HPtt/YScM8RIguweFeoQ==", null, false, "FRPOV99KPNK6QHM5IUUM6OXJAV32LW4Q", false, "SuperAdminMAF" },
                    { "8231b097-f123-45a8-b9b5-e7c4ddad8b7c", 0, "3ioo142a-2145-4e3c-9fmb-b6d7234d4770", "monir.hossain@decathlon.com", false, 4, false, null, "MONIR.HOSSAIN@DECATHLON.COM", "SUPERADMINRFL", "AQAAAAEAACcQAAAAEEO6XuvdZ4p/fzatnYaxaH4psRTnOEp3N+Ez6FFxAh1S76HPtt/YScM8RIguweFeoQ==", null, false, "LKTIC77KPNK6Q54FLRUM6OXJAV14LW4W", false, "SuperAdminRFL" },
                    { "8fg9ww95-f792-45a8-b9b5-e7c4ddad8b7c", 0, "3fjj357a-8632-4e3c-9flk-b6d7234d4880", "monir.hossain@decathlon.com", false, 5, false, null, "MONIR.HOSSAIN@DECATHLON.COM", "SUPERADMINAPEX", "AQAAAAEAACcQAAAAEEO6XuvdZ4p/fzatnYaxaH4psRTnOEp3N+Ez6FFxAh1S76HPtt/YScM8RIguweFeoQ==", null, false, "VJRSO58KPNK5RCP5JRUM6OXJAV49LW4E", false, "SuperAdminAPEX" },
                    { "7846b041-f643-45a8-b9b5-e7c4ddad8b7c", 0, "3zaz461a-2136-4e3c-9fpo-b6d7234d4990", "monir.hossain@decathlon.com", false, 6, false, null, "MONIR.HOSSAIN@DECATHLON.COM", "SUPERADMINEDISON", "AQAAAAEAACcQAAAAEEO6XuvdZ4p/fzatnYaxaH4psRTnOEp3N+Ez6FFxAh1S76HPtt/YScM8RIguweFeoQ==", null, false, "JWERP12KPNK6QHM5JRUM6OXJAV85LW4R", false, "SuperAdminEDISON" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "UserId", "RoleId" },
                values: new object[,]
                {
                    { "1891b070-42ce-40ad-9c4e-5a47f81dc0ad", "m121b57c-9025-9223-e0f0-bdec765e3bgb" },
                    { "133a8219-f819-45a8-b9b5-e7c4ddad8b7c", "m347b57d-8617-9224-g0h0-bdec765e3lml" },
                    { "8lh7oo83-f918-45a8-b9b5-e7c4ddad8b7c", "w585b57b-7456-8222-c0d0-bdec765e3awa" },
                    { "8231b097-f123-45a8-b9b5-e7c4ddad8b7c", "m915b57e-2431-9225-i0j0-bdec765e3pgp" },
                    { "8fg9ww95-f792-45a8-b9b5-e7c4ddad8b7c", "m173b57f-8519-9226-k0l0-bdec765e3rir" },
                    { "7846b041-f643-45a8-b9b5-e7c4ddad8b7c", "m354b57g-4013-9227-m0n0-bdec765e3oeo" }
                });
        }
    }
}
