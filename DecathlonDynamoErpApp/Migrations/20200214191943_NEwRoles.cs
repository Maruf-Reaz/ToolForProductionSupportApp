using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DecathlonDynamoErpApp.Migrations
{
    public partial class NEwRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a212b59c-5209-3999-e1f1-bdec785e4lil",
                columns: new[] { "Name", "NormalizedName" },
                values: new object[] { "KSIUserOfLineBalancing", "KSIUSEROFLINEBALANCING" });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e858c58l-3251-9888-c1d1-bdec765e3pip",
                columns: new[] { "Name", "NormalizedName" },
                values: new object[] { "MAFUserOfLineBalancing", "MAFUSEROFLINEBALANCING" });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "g734b69c-9461-6111-g1h1-bdec785e4imi",
                columns: new[] { "Name", "NormalizedName" },
                values: new object[] { "YSSUserOfLineBalancing", "YSSUSEROFLINEBALANCING" });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "v212b59c-6348-6121-m1n1-bdec785e4iki",
                columns: new[] { "Name", "NormalizedName" },
                values: new object[] { "EDISONUserOfLineBalancing", "EDISONUSEROFLINEBALANCING" });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "x212b59c-3269-1666-k1l1-bdec785e4gig",
                columns: new[] { "Name", "NormalizedName" },
                values: new object[] { "APEXUserOfLineBalancing", "APEXUSEROFLINEBALANCING" });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "z323b59c-5209-5344-i1j1-bdec735e4gpg",
                columns: new[] { "Name", "NormalizedName" },
                values: new object[] { "RFLUserOfLineBalancing", "RFLUSEROFLINEBALANCING" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "CreatedOn", "Description", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "v212b59c-6349-6122-m2n2-bdec785e4iki", "ql5a3b1g-5c2g-6513-81e1-4fd511e23i18", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Has Several Permissions", "EDISONUserOfIncentive", "EDISONUSEROFINCENTIVE" },
                    { "x212b59c-3272-1669-k4l4-bdec785e4gig", "lq5a3b1g-6c2g-3257-93e1-4fd255e23i18", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Has Several Permissions", "APEXUserOfSOT", "APEXUSEROFSOT" },
                    { "x212b59c-3271-1668-k3l3-bdec785e4gig", "lq5a3b1g-6c2g-3256-93e1-4fd255e23i18", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Has Several Permissions", "APEXUserOfSkillMatrix", "APEXUSEROFSKILLMATRIX" },
                    { "x212b59c-3270-1667-k2l2-bdec785e4gig", "lq5a3b1g-6c2g-3255-93e1-4fd255e23i18", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Has Several Permissions", "APEXUserOfIncentive", "APEXUSEROFINCENTIVE" },
                    { "z323b59c-5212-5347-i4j4-bdec735e4gpg", "al5a3b0g-7c3p-4520-45e1-4fd377e23i20", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Has Several Permissions", "RFLUserOfSOT", "RFLUSEROFSOT" },
                    { "z323b59c-5211-5346-i3j3-bdec735e4gpg", "al5a3b0g-7c3p-4519-45e1-4fd377e23i20", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Has Several Permissions", "RFLUserOfSkillMatrix", "RFLUSEROFSKILLMATRIX" },
                    { "z323b59c-5210-5345-i2j2-bdec735e4gpg", "al5a3b0g-7c3p-4518-45e1-4fd377e23i20", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Has Several Permissions", "RFLUserOfIncentive", "RFLUSEROFINCENTIVE" },
                    { "g734b69c-9464-6114-g4h4-bdec785e4imi", "ri5a3b0g-8d4j-2187-95e1-4fd352e23i19", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Has Several Permissions", "YSSUserOfSOT", "YSSUSEROFSOT" },
                    { "g734b69c-9463-6113-g3h3-bdec785e4imi", "ri5a3b0g-8d4j-2186-95e1-4fd352e23i19", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Has Several Permissions", "YSSUserOfSkillMatrix", "YSSUSEROFSKILLMATRIX" },
                    { "g734b69c-9462-6112-g2h2-bdec785e4imi", "ri5a3b0g-8d4j-2185-95e1-4fd352e23i19", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Has Several Permissions", "YSSUserOfIncentive", "YSSUSEROFINCENTIVE" },
                    { "a212b59c-5212-4002-e4f4-bdec785e4lil", "eu5a3b0g-9c5g-9129-45e1-4fd366e23i18", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Has Several Permissions", "KSIUserOfSOT", "KSIUSEROFSOT" },
                    { "a212b59c-5211-4001-e3f3-bdec785e4lil", "eu5a3b0g-9c5g-9128-45e1-4fd366e23i18", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Has Several Permissions", "KSIUserOfSkillMatrix", "KSIUSEROFSKILLMATRIX" },
                    { "a212b59c-5210-4000-e2f2-bdec785e4lil", "eu5a3b0g-9c5g-9127-45e1-4fd366e23i18", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Has Several Permissions", "KSIUserOfIncentive", "KSIUSEROFINCENTIVE" },
                    { "e858c58l-3254-9891-c4d4-bdec765e3pip", "ah8b4b9j-3n6o-3494-73e1-4fd800e23g17", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Has Several Permissions", "MAFUserOfSOT", "MAFUSEROFSOT" },
                    { "e858c58l-3253-9890-c3d3-bdec765e3pip", "ah8b4b9j-3n6o-3493-73e1-4fd800e23g17", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Has Several Permissions", "MAFUserOfSkillMatrix", "MAFUSEROFSKILLMATRIX" },
                    { "e858c58l-3252-9889-c2d2-bdec765e3pip", "ah8b4b9j-3n6o-3492-73e1-4fd800e23g17", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Has Several Permissions", "MAFUserOfIncentive", "MAFUSEROFINCENTIVE" },
                    { "v212b59c-6350-6123-m3n3-bdec785e4iki", "ql5a3b1g-5c2g-6514-81e1-4fd511e23i18", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Has Several Permissions", "EDISONUserOfSkillMatrix", "EDISONUSEROFSKILLMATRIX" },
                    { "v212b59c-6351-6124-m4n4-bdec785e4iki", "ql5a3b1g-5c2g-6515-81e1-4fd511e23i18", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Has Several Permissions", "EDISONUserOfSOT", "EDISONUSEROFSOT" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a212b59c-5210-4000-e2f2-bdec785e4lil");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a212b59c-5211-4001-e3f3-bdec785e4lil");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a212b59c-5212-4002-e4f4-bdec785e4lil");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e858c58l-3252-9889-c2d2-bdec765e3pip");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e858c58l-3253-9890-c3d3-bdec765e3pip");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e858c58l-3254-9891-c4d4-bdec765e3pip");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "g734b69c-9462-6112-g2h2-bdec785e4imi");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "g734b69c-9463-6113-g3h3-bdec785e4imi");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "g734b69c-9464-6114-g4h4-bdec785e4imi");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "v212b59c-6349-6122-m2n2-bdec785e4iki");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "v212b59c-6350-6123-m3n3-bdec785e4iki");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "v212b59c-6351-6124-m4n4-bdec785e4iki");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "x212b59c-3270-1667-k2l2-bdec785e4gig");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "x212b59c-3271-1668-k3l3-bdec785e4gig");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "x212b59c-3272-1669-k4l4-bdec785e4gig");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "z323b59c-5210-5345-i2j2-bdec735e4gpg");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "z323b59c-5211-5346-i3j3-bdec735e4gpg");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "z323b59c-5212-5347-i4j4-bdec735e4gpg");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a212b59c-5209-3999-e1f1-bdec785e4lil",
                columns: new[] { "Name", "NormalizedName" },
                values: new object[] { "KSIUser", "KSIUSER" });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e858c58l-3251-9888-c1d1-bdec765e3pip",
                columns: new[] { "Name", "NormalizedName" },
                values: new object[] { "MAFUser", "MAFUSER" });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "g734b69c-9461-6111-g1h1-bdec785e4imi",
                columns: new[] { "Name", "NormalizedName" },
                values: new object[] { "YSSUser", "YSSUSER" });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "v212b59c-6348-6121-m1n1-bdec785e4iki",
                columns: new[] { "Name", "NormalizedName" },
                values: new object[] { "EDISONUser", "EDISONUSER" });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "x212b59c-3269-1666-k1l1-bdec785e4gig",
                columns: new[] { "Name", "NormalizedName" },
                values: new object[] { "APEXUser", "APEXUSER" });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "z323b59c-5209-5344-i1j1-bdec735e4gpg",
                columns: new[] { "Name", "NormalizedName" },
                values: new object[] { "RFLUser", "RFLUSER" });
        }
    }
}
