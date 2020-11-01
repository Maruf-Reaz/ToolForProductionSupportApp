using Microsoft.EntityFrameworkCore.Migrations;

namespace DecathlonDynamoErpApp.Migrations
{
    public partial class FactoryInMachineAndOperation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FactoryId",
                table: "Operations",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FactoryId",
                table: "Machines",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Operations_FactoryId",
                table: "Operations",
                column: "FactoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Machines_FactoryId",
                table: "Machines",
                column: "FactoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Machines_Factories_FactoryId",
                table: "Machines",
                column: "FactoryId",
                principalTable: "Factories",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Operations_Factories_FactoryId",
                table: "Operations",
                column: "FactoryId",
                principalTable: "Factories",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Machines_Factories_FactoryId",
                table: "Machines");

            migrationBuilder.DropForeignKey(
                name: "FK_Operations_Factories_FactoryId",
                table: "Operations");

            migrationBuilder.DropIndex(
                name: "IX_Operations_FactoryId",
                table: "Operations");

            migrationBuilder.DropIndex(
                name: "IX_Machines_FactoryId",
                table: "Machines");

            migrationBuilder.DropColumn(
                name: "FactoryId",
                table: "Operations");

            migrationBuilder.DropColumn(
                name: "FactoryId",
                table: "Machines");
        }
    }
}
