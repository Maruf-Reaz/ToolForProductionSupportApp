using Microsoft.EntityFrameworkCore.Migrations;

namespace DecathlonDynamoErpApp.Migrations
{
    public partial class PLBUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ParticularLineBalancings_LineId",
                table: "ParticularLineBalancings");

            migrationBuilder.DeleteData(
                table: "CssClasses",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "CssClasses",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.CreateIndex(
                name: "IX_ParticularLineBalancings_LineId",
                table: "ParticularLineBalancings",
                column: "LineId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ParticularLineBalancings_LineId",
                table: "ParticularLineBalancings");

            migrationBuilder.InsertData(
                table: "CssClasses",
                columns: new[] { "Id", "IsActive", "Name" },
                values: new object[] { 4, false, "Light Background" });

            migrationBuilder.InsertData(
                table: "CssClasses",
                columns: new[] { "Id", "IsActive", "Name" },
                values: new object[] { 5, false, "Dark Mode" });

            migrationBuilder.CreateIndex(
                name: "IX_ParticularLineBalancings_LineId",
                table: "ParticularLineBalancings",
                column: "LineId");
        }
    }
}
