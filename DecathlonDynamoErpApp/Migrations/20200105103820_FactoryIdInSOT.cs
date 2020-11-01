using Microsoft.EntityFrameworkCore.Migrations;

namespace DecathlonDynamoErpApp.Migrations
{
    public partial class FactoryIdInSOT : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FactoryId",
                table: "StandardOperationTimes",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_StandardOperationTimes_FactoryId",
                table: "StandardOperationTimes",
                column: "FactoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_StandardOperationTimes_Factories_FactoryId",
                table: "StandardOperationTimes",
                column: "FactoryId",
                principalTable: "Factories",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StandardOperationTimes_Factories_FactoryId",
                table: "StandardOperationTimes");

            migrationBuilder.DropIndex(
                name: "IX_StandardOperationTimes_FactoryId",
                table: "StandardOperationTimes");

            migrationBuilder.DropColumn(
                name: "FactoryId",
                table: "StandardOperationTimes");
        }
    }
}
