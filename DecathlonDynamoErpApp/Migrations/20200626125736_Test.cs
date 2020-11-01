using Microsoft.EntityFrameworkCore.Migrations;

namespace DecathlonDynamoErpApp.Migrations
{
    public partial class Test : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_LineIncentives_LineId",
                table: "LineIncentives");

            migrationBuilder.CreateIndex(
                name: "IX_LineIncentives_LineId",
                table: "LineIncentives",
                column: "LineId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_LineIncentives_LineId",
                table: "LineIncentives");

            migrationBuilder.CreateIndex(
                name: "IX_LineIncentives_LineId",
                table: "LineIncentives",
                column: "LineId",
                unique: true);
        }
    }
}
